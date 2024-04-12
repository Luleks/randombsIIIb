#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int k = size;
    int A[k][k], B[k][k], C[k][k];
    int my_col[k];

    MPI_Datatype COL_TYPE, RESIZED_COL_TYPE;
    MPI_Type_vector(k, 1, k, MPI_INT, &COL_TYPE);
    MPI_Type_create_resized(COL_TYPE, 0, sizeof(int), &RESIZED_COL_TYPE);
    MPI_Type_commit(&RESIZED_COL_TYPE);

    if (rank == 0) {
        for (int i = 0; i < k; ++i)
            for (int j = 0; j < k; ++j)
                A[i][j] = i * k + j + 1;
        for (int i = 0; i < k; ++i)
            for (int j = 0; j < k; ++j)
                B[i][j] = i * k + j + 1;
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j)
                printf("%d ", A[i][j]);
            printf("\n");
        }
        printf("\n");
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j)
                printf("%d ", B[i][j]);
            printf("\n");
        }
        printf("\n");
    }
    MPI_Bcast(B, k * k, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Scatter(A, 1, RESIZED_COL_TYPE, my_col, k, MPI_INT, 0, MPI_COMM_WORLD);

    int my_res[k][k];

    for (int i = 0; i < k; ++i) {
        for (int j = 0; j < k; ++j) {
            my_res[i][j] = B[rank][j] * my_col[i];
        }
    }

    MPI_Reduce(my_res, C, k * k, MPI_INT, MPI_SUM, size - 1, MPI_COMM_WORLD);
    if (rank == size - 1) {
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j)
                printf("%d ", C[i][j]);
            printf("\n");
        }
    }

    MPI_Type_free(&RESIZED_COL_TYPE);
    MPI_Type_free(&COL_TYPE);
    MPI_Finalize();
    return 0;
}