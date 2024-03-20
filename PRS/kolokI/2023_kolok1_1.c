#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <limits.h>

#define k 4
#define m 6
#define l 4

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    int A[k][m], B[m][l];

    MPI_Datatype COLUMN_TYPE;
    MPI_Datatype RESIZED_COLUMN_TYPE;

    MPI_Type_vector(m  * k/ size, 1, size, MPI_INT, &COLUMN_TYPE);
    MPI_Type_create_resized(COLUMN_TYPE, 0, sizeof(int), &RESIZED_COLUMN_TYPE);
    MPI_Type_commit(&RESIZED_COLUMN_TYPE);

    int my_columns[k][m / size];

    MPI_Datatype ROW_TYPE;
    MPI_Datatype RESIZED_ROW_TYPE;

    MPI_Type_vector(m / size, l, l * size, MPI_INT, &ROW_TYPE);
    MPI_Type_create_resized(ROW_TYPE, 0, l * sizeof(int), &RESIZED_ROW_TYPE);
    MPI_Type_commit(&RESIZED_ROW_TYPE);

    int my_rows[m / size][l];
    

    if (rank == 0) {
        for (int i = 0; i < k; i++)
            for (int j = 0; j < m; j++)
                A[i][j] = i * m + j + 1;
        for (int i = 0; i < m; i++)
            for (int j = 0; j < l; j++)
                B[i][j] = i * l + j + 1;
    }

    MPI_Scatter(A, 1, RESIZED_COLUMN_TYPE, my_columns, k * m / size, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Scatter(B, 1, RESIZED_ROW_TYPE, my_rows, l * m / size, MPI_INT, 0, MPI_COMM_WORLD);
    
    int my_prod[k][l], prod[k][l];
    struct { int min; int rank; } my_min, total_min;
    my_min.rank = rank;
    my_min.min = INT_MAX;

    for (int i = 0; i < k; ++i) {
        for (int j = 0; j < l; ++j) {
            my_prod[i][j] = 0;
            for (int z = 0; z < m / size; ++z) {
                my_prod[i][j] += my_columns[i][z] * my_rows[z][j];
                if (my_columns[i][z] < my_min.min)
                    my_min.min = my_columns[i][z];
                if (my_rows[z][j] < my_min.min)
                    my_min.min = my_rows[z][j];
            }
        }
    }
    MPI_Reduce(&my_min, &total_min, 1, MPI_2INT, MPI_MINLOC, 0, MPI_COMM_WORLD);
    MPI_Bcast(&total_min, 1, MPI_2INT, 0, MPI_COMM_WORLD);

    MPI_Reduce(my_prod, prod, k * l, MPI_INT, MPI_SUM, total_min.rank, MPI_COMM_WORLD);
    if (rank == total_min.rank) {
        printf("Process %d has the min\n", rank);
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < l; ++j)
                printf("%d ", prod[i][j]);
            printf("\n");
        }
    }

    MPI_Type_free(&COLUMN_TYPE);
    MPI_Type_free(&RESIZED_COLUMN_TYPE);
    MPI_Type_free(&ROW_TYPE);
    MPI_Type_free(&RESIZED_ROW_TYPE);
    MPI_Finalize();
}