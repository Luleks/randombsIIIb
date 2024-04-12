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
    int periods[2] = { 1, 1 };
    MPI_Comm CARTESIAN_COMM;
    MPI_Cart_create(MPI_COMM_WORLD, ndims, dims, periods, 0, &CARTESIAN_COMM);
    
    int my_coords[2];
    MPI_Cart_coords(CARTESIAN_COMM, rank, ndims, my_coords);

    MPI_Comm COLUMN_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, rank % m, 0, &COLUMN_COMM);
    int mycolrank;
    MPI_Comm_rank(COLUMN_COMM, &mycolrank);

    MPI_Bcast(my_coords, 2, MPI_INT, n - 1, COLUMN_COMM);

    if (mycolrank == 0) {
        printf("%d %d\n", my_coords[0], my_coords[1]);
    }

    MPI_Finalize();
    return 0;
}