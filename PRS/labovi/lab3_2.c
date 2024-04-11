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
    int ndims = 2;
    int dims[2] = { n, m };
    int periods[2] = { 0, 1 };
    MPI_Comm CARTESIAN_COMM;
    MPI_Cart_create(MPI_COMM_WORLD, ndims, dims, periods, 0, &CARTESIAN_COMM);

    int my_left, my_right;
    MPI_Cart_shift(CARTESIAN_COMM, 1, 2, &my_left, &my_right);

    printf("%d left: %d right: %d\n", rank, my_left, my_right);

    MPI_Finalize();
    return 0;
}