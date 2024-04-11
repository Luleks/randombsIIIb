#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

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

    int matrix[(size / 3) + 1][5];
    int my_row[5];
    if (rank == 0) {
        for (int i = 0; i < (size / 3) + 1; ++i) {
            for (int j = 0; j < 5; ++j) {
                matrix[i][j] = rand() % 30;
            }
        }
        printMatrix((int*)matrix, (size / 3) + 1, 5);
    }
    MPI_Comm THREE_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, rank % 3, 0, &THREE_COMM);
    MPI_Scatter(&matrix[0][0], 5, MPI_INT, my_row, 5, MPI_INT, 0, THREE_COMM);

    if (rank % 3 == 0) {
        for (int i = 0; i < 5; ++i)
            printf("%d ", my_row[i]);
        printf("\n");
    }

    MPI_Finalize();
    return 0;
}