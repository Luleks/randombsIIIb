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

struct mystruct {
    int val;
    int rank;
};

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    int k = -1;
    if (rank == 0) {
        while (k % size != 0) {
            printf("k: (divisible by %d): ", size);
            scanf("%d", &k);
        }
    }
    MPI_Bcast(&k, 1, MPI_INT, 0, MPI_COMM_WORLD);
    int A[k][k];

    MPI_Datatype ROW_TYPE, RESIZED_ROW_TYPE;
    MPI_Type_vector(k / 2, k / size, 2 * size, MPI_INT, &ROW_TYPE);
    MPI_Type_create_resized(ROW_TYPE, 0, k / size * sizeof(int), &RESIZED_ROW_TYPE);
    MPI_Type_commit(&RESIZED_ROW_TYPE);

    if (rank == 0) {
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j) {
                A[i][j] = rand() % 40;
            }
        }

        printMatrix((int*)A, k, k);
    }
    int my_recv[k / 2 * k / size];
    MPI_Scatter(&A[1][0], 1, RESIZED_ROW_TYPE, my_recv, k / 2 * k / size, MPI_INT, 0, MPI_COMM_WORLD);

    struct mystruct ms1 = { my_recv[0], rank };
    struct mystruct min;

    for (int i = 1; i < k / 2 * k / size; ++i) {
        if (ms1.val > my_recv[i]) {
            ms1.val = my_recv[i];
        }
    }

    MPI_Reduce(&ms1, &min, 1, MPI_2INT, MPI_MINLOC, 0, MPI_COMM_WORLD);
    if (rank == 0) {
        printf("%d %d\n", min.rank, min.val);
    }

    MPI_Type_free(&RESIZED_ROW_TYPE);
    MPI_Type_free(&ROW_TYPE);
    MPI_Finalize();
    return 0;
}