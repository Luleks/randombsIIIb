#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);

    int n = size + 1, m;
    if (rank == 0) {
        while(size % n != 0 || n == 1 || n == size) {
            printf("n: (evenly dividing %d and != 1 and !=%d): ", size, size);
            scanf("%d", &n);
        }
        m = size / n;
    }

    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Bcast(&m, 1, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Comm CARTESIAN_COMM;
    int ndims = 2;
    int dims[2] = { n, m };
    int periods[2] = { 1, 0 };
    MPI_Cart_create(MPI_COMM_WORLD, ndims, dims, periods, 0, &CARTESIAN_COMM);

    int up, down;
    MPI_Cart_shift(CARTESIAN_COMM, 0, 1, &up, &down);
    int sum = up + down, total_sum;

    MPI_Reduce(&sum, &total_sum, 1, MPI_INT, MPI_SUM, 0, MPI_COMM_WORLD);
    if (rank == 0) {
        printf("%d\n", total_sum);
    }

    MPI_Finalize();
    return 0;
}