#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <math.h>

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int n = (int)sqrt(size);

    if ((int)pow(n, 2) != size) {
        printf("MAJOR ERROR\n");
        MPI_Finalize();
        exit(1);
    }

    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    int ndims = 2;
    int dims[2] = { n, n };
    int periods[2] = { 1, 1 };
    int my_coords[2];
    
    MPI_Comm CARTESIAN_COMM;
    MPI_Cart_create(MPI_COMM_WORLD, ndims, dims, periods, 0, &CARTESIAN_COMM);
    MPI_Cart_coords(CARTESIAN_COMM, rank, ndims, my_coords);

    MPI_Comm GORNJA_DONJA_COMM;
    MPI_Comm_split(MPI_COMM_WORLD, my_coords[0] >= my_coords[1], 0, &GORNJA_DONJA_COMM);

    int suma;
    MPI_Reduce(&rank, &suma, 1, MPI_INT, MPI_SUM, 0, GORNJA_DONJA_COMM);
    int gornjasuma;
    if (rank == 0) {
        MPI_Recv(&gornjasuma, 1, MPI_INT, 1, 88, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        printf("%d %d\n", suma, gornjasuma);
    }
    else if (rank == 1) {
        MPI_Send(&suma, 1, MPI_INT, 0, 88, MPI_COMM_WORLD);
    }


    MPI_Finalize();
    return 0;
}