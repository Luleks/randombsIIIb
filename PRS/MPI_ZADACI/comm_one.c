#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <math.h>

#define n 8

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, p;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &p);
    int matrix[n][n];
    int b[n], c[n];
    int q = (int)sqrt(p);
    int k = n / q;

    int my_block[k][k], my_vector[k];

    MPI_Datatype MATRIX_BLOCK_TYPE, RESIZED_MATRIX_BLOCK_TYPE;
    MPI_Type_vector(k, k, n, MPI_INT, &MATRIX_BLOCK_TYPE);
    MPI_Type_commit(&MATRIX_BLOCK_TYPE);

    MPI_Comm FIRST_ROW_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, rank / q, 0, &FIRST_ROW_COMM);

    MPI_Comm COL_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, rank % q, 0, &COL_COMM);

    if (rank == 0) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j) {
                matrix[i][j] = i * n + j;
            }
        }
        for (int i = 0; i < n; ++i) {
            b[i] = 1;
        }
        // Send matrix subblocks to processes
        int c = 1;
        for (int i = 0; i < n; i+=k) {
            for (int j = 0; j < n; j+=k) {
                if (i == 0 && j == 0)
                    continue;
                MPI_Send(&matrix[i][j], 1, MATRIX_BLOCK_TYPE, c++, 88, MPI_COMM_WORLD);
            }
        }
        // Copy first block for yourself
        for (int i = 0; i < k; i++) {
            for (int j = 0; j < k; ++j) {
                my_block[i][j] = matrix[i][j];
            }
        }
    }
    else {
        MPI_Recv(my_block, k * k, MPI_INT, 0, 88, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
    }

    // Distributing vector
    if (rank / q == 0)
        MPI_Bcast(b, n, MPI_INT, 0, FIRST_ROW_COMM);
    MPI_Scatter(b, k, MPI_INT, my_vector, k, MPI_INT, 0, COL_COMM);
    
    int my_res[k], gatthering_res[k];
    for (int i = 0; i < k; ++i) {
        my_res[i] = 0;
        for (int j = 0; j < k; ++j) {
            my_res[i] += my_vector[j] * my_block[i][j];
        }
    }
    MPI_Reduce(my_res, gatthering_res, k, MPI_INT, MPI_SUM, 0, FIRST_ROW_COMM);
    if (rank % q == 0)
        MPI_Gather(gatthering_res, k, MPI_INT, c, k, MPI_INT, 0, COL_COMM);
    
    if (rank == 0) {
        for (int i = 0; i < n; ++i)
            printf("%d ", c[i]);
        printf("\n");
    }


    MPI_Type_free(&MATRIX_BLOCK_TYPE);
    MPI_Finalize();
    return 0;
}