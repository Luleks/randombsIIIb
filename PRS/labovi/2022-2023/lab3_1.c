#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <math.h>


int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int side = (int)sqrt(size);

    if ((int)pow(side, 2) != size) {
        printf("MAJOR ERROR\n");
        MPI_Finalize();
        return 0;
    }

    int n_diag_proc = (side % 2) ? 2 * side - 1 : 2 * side;
    int diag_ranks[n_diag_proc];
    int c = 0;
    for (int i = 0; i < size; ++i) {
        if (i % (side + 1) == 0 || i % (side - 1) == 0)
            diag_ranks[c++] = i;
    }

    MPI_Group WORLD_GROUP, DIAG_GROUP;
    MPI_Comm_group(MPI_COMM_WORLD, &WORLD_GROUP);
    MPI_Group_incl(WORLD_GROUP, n_diag_proc, diag_ranks, &DIAG_GROUP);
    MPI_Comm DIAG_COMM;
    MPI_Comm_create(MPI_COMM_WORLD, DIAG_GROUP, &DIAG_COMM);

    int a;
    if (rank == 0)
        a = 55; 
    if (rank % (side + 1) == 0 || rank % (side - 1) == 0) {
        MPI_Bcast(&a, 1, MPI_INT, 0, DIAG_COMM);
        int newrank;
        MPI_Comm_rank(DIAG_COMM, &newrank);
        printf("I was %d now I'm %d and I have %d\n", rank, newrank, a);
    }

    MPI_Finalize();
    return 0;
}