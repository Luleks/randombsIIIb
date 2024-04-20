#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <math.h>

#define n 8

void printMatrix(int* matrix, int rows, int cols) {
    for (int i = 0; i < rows; i++) {
        for (int j = 0; j < cols; j++) {
            printf("%d ", matrix[i * cols + j]);
        }
        printf("\n");
    }
}

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int q = (int)sqrt(size);
    int k = n / q;
    int A[n][n], B[n][n], C[n][n];
    int my_rows[k][n], my_cols[n][k];

    MPI_Datatype ROW_TYPE, RESIZED_ROW_TYPE;
    MPI_Type_vector(k, n, n * q, MPI_INT, &ROW_TYPE);
    MPI_Type_create_resized(ROW_TYPE, 0, n * sizeof(int), &RESIZED_ROW_TYPE);
    MPI_Type_commit(&RESIZED_ROW_TYPE);

    MPI_Datatype COL_TYPE, RESIZED_COL_TYPE;
    MPI_Type_vector(n * k, 1, q, MPI_INT, &COL_TYPE);
    MPI_Type_create_resized(COL_TYPE, 0, sizeof(int), &RESIZED_COL_TYPE);
    MPI_Type_commit(&RESIZED_COL_TYPE);

    if (rank == 0) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                A[i][j] = i * n + j;
                B[i][j] = i * n + j;
            }
        }
    }

    MPI_Comm ROW_COMM, COL_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, rank / q, 0, &ROW_COMM);
    MPI_Comm_split(MPI_COMM_WORLD, rank % q, 0, &COL_COMM);

    MPI_Scatter(A, 1, RESIZED_ROW_TYPE, my_rows, k * n, MPI_INT, 0, COL_COMM);
    MPI_Bcast(my_rows, k * n, MPI_INT, 0, ROW_COMM);
    MPI_Scatter(B, 1, RESIZED_COL_TYPE, my_cols, k * n, MPI_INT, 0, ROW_COMM);
    MPI_Bcast(my_cols, k * n, MPI_INT, 0, COL_COMM);

    int my_res[k][k];

    for (int i = 0; i < k; ++i) {
        for (int j = 0; j < k; ++j) {
            my_res[i][j] = 0;
            for (int z = 0; z < n; ++z) {
                my_res[i][j] += my_rows[i][z] * my_cols[z][j];
            }
        }
    }

    MPI_Datatype RECV_COL_TYPE, RESIZED_RECV_COL_TYPE;
    MPI_Type_vector(k * k, 1, q, MPI_INT, &RECV_COL_TYPE);
    MPI_Type_create_resized(RECV_COL_TYPE, 0, sizeof(int), &RESIZED_RECV_COL_TYPE);
    MPI_Type_commit(&RESIZED_RECV_COL_TYPE);

    int semi_res[k][n];
    MPI_Gather(my_res, k * k, MPI_INT, semi_res, 1, RESIZED_RECV_COL_TYPE, 0, ROW_COMM);
    MPI_Gather(semi_res, k * n, MPI_INT, C, 1, RESIZED_ROW_TYPE, 0, COL_COMM);

    if (rank == 0) {
        printMatrix((int*)C, n, n);
    }

    MPI_Type_free(&RESIZED_COL_TYPE);
    MPI_Type_free(&RESIZED_RECV_COL_TYPE);
    MPI_Type_free(&RESIZED_ROW_TYPE);
    MPI_Type_free(&COL_TYPE);
    MPI_Type_free(&RECV_COL_TYPE);
    MPI_Type_free(&ROW_TYPE);
    MPI_Finalize();
    return 0;
}