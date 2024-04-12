#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

#define m 4
#define n 6
#define k 4

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int A[m][n], B[n][k], C[m][k];

    if (rank == 0) {
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                A[i][j] = i * n + j;
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < k; ++j)
                B[i][j] = i * k + j;
    }
    MPI_Bcast(A, m * n, MPI_INT, 0, MPI_COMM_WORLD);

    MPI_Datatype COL_TYPE, RESIZED_COL_TYPE;
    MPI_Type_vector(n, k / size, k, MPI_INT, &COL_TYPE);
    MPI_Type_create_resized(COL_TYPE, 0, k / size * sizeof(int), &RESIZED_COL_TYPE);
    MPI_Type_commit(&RESIZED_COL_TYPE);

    MPI_Datatype RECV_TYPE, RESIZED_RECV_TYPE;
    MPI_Type_vector(m, k / size, k, MPI_INT, &RECV_TYPE);
    MPI_Type_create_resized(RECV_TYPE, 0, k / size * sizeof(int), &RESIZED_RECV_TYPE);
    MPI_Type_commit(&RESIZED_RECV_TYPE);

    int my_cols[n][k / size], my_res[m][k / size];
    MPI_Scatter(B, 1, RESIZED_COL_TYPE, my_cols, n * k / size, MPI_INT, 0, MPI_COMM_WORLD);

    for (int i = 0; i < m; ++i) {
        for (int j = 0; j < k / size; ++j) {
            my_res[i][j] = 0;
            for (int z = 0; z < n; ++z)
                my_res[i][j] += A[i][z] * my_cols[z][j];
        }
    }

    MPI_Gather(my_res, m * k / size, MPI_INT, C, 1, RESIZED_RECV_TYPE, 0, MPI_COMM_WORLD);
    if (rank == 0) {
        for(int i = 0; i < m; ++i) {
            for (int j = 0; j < k; ++j)
                printf("%d ", C[i][j]);
            printf("\n");
        }
    }

    MPI_Type_free(&RESIZED_COL_TYPE);
    MPI_Type_free(&COL_TYPE);
    MPI_Type_free(&RESIZED_RECV_TYPE);
    MPI_Type_free(&RECV_TYPE);
    MPI_Finalize();
    return 0;
}