#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int n = -1;
    if (rank == 0) {
        while (n % size != 0) {
            printf("n (divisible by %d)\n", size);
            scanf("%d", &n);
        }
    }
    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    int a[n], b[n], res;
    if (rank == 0) {
        for (int i = 0; i < n; ++i) {
            a[i] = 1;
            b[i] = 1;
        }
    }

    int my_a[n / size], my_b[n / size], my_res = 0;

    MPI_Datatype SEND_TYPE, RESIZED_SEND_TYPE;
    MPI_Type_vector(n / size, 1, size, MPI_INT, &SEND_TYPE);
    MPI_Type_create_resized(SEND_TYPE, 0, sizeof(int), &RESIZED_SEND_TYPE);
    MPI_Type_commit(&RESIZED_SEND_TYPE);
    MPI_Scatter(a, 1, RESIZED_SEND_TYPE, my_a, n / size, MPI_INT, 0, MPI_COMM_WORLD);
    MPI_Scatter(b, 1, RESIZED_SEND_TYPE, my_b, n / size, MPI_INT, 0, MPI_COMM_WORLD);

    for (int i = 0; i < n / size; ++i) {
        my_res += my_a[i] * my_b[i];
    }
    MPI_Reduce(&my_res, &res, 1, MPI_INT, MPI_SUM, 0, MPI_COMM_WORLD);

    if (rank == 0) {
        printf("%d\n", res);
    }



    MPI_Type_free(&SEND_TYPE);
    MPI_Type_free(&RESIZED_SEND_TYPE);
    MPI_Finalize();
    return 0;
}