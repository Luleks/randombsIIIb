#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"


int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int m = size;
    int A[m][m], B[m][m], C[m][m];

    int my_row[m];
    int my_res[m];

    int mins[m];

    if (rank == 0) {
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < m; ++j)
                A[i][j] = i * m + j + 1;
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < m; ++j)
                B[i][j] = i * m + j + 1;
    }
    MPI_Bcast(B, m * m, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Scatter(A, m, MPI_INT, my_row, m, MPI_INT, 0, MPI_COMM_WORLD);

    for (int i = 0; i < m; ++i) {
        my_res[i] = 0;
        for (int j = 0; j < m; ++j) {
            my_res[i] += my_row[j] * B[j][i];
        }
    }
    MPI_Gather(my_res, m, MPI_INT, C, m, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Reduce(my_res, mins, m, MPI_INT, MPI_MIN, 0, MPI_COMM_WORLD);

    if (rank == 0) {
        for (int i = 0; i < m; ++i) {
            for (int j = 0; j < m; ++j)
                printf("%d ", C[i][j]);
            printf("\n");
        }
        for (int i = 0; i < m; ++i) {
            printf("%d ", mins[i]);
        }
        printf("\n");
    }

    MPI_Finalize();
    return 0;
}