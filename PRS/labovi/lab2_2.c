#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"
#include <time.h>

int main(int argc, char* argv[]) {
    srand(time(0));
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int n = size + 1;

    if (rank == 0) {
        while (n % size != 0 || n % 2 != 0) {
            printf("n (divisible by %d and 2): ", size);
            fflush(stdout);
            scanf("%d", &n);
        }
    }

    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    int matrix[n][n];
    if (rank == 0) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j)
                matrix[i][j] = rand() % 125;
        }
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j)
                printf("%d ", matrix[i][j]);
            printf("\n");
        }
        printf("\n");
        fflush(stdout);
    }

    MPI_Datatype CUSTOM_TYPE, CUSTOM_RESIZED_TYPE;
    MPI_Type_vector(n / size * n / 2, 1, 2, MPI_INT, &CUSTOM_TYPE);
    MPI_Type_create_resized(CUSTOM_TYPE, 0, n / size * n * sizeof(int), &CUSTOM_RESIZED_TYPE);
    MPI_Type_commit(&CUSTOM_RESIZED_TYPE);

    int my_values[n / size * n / 2];
    MPI_Scatter(matrix, 1, CUSTOM_RESIZED_TYPE, my_values, n / size * n / 2, MPI_INT, 0, MPI_COMM_WORLD);
    int my_min = my_values[0], global_min;
    for (int i = 1; i < n / size * n / 2; ++i) {
        if (my_values[i] < my_min)
            my_min = my_values[i];
    }
    MPI_Reduce(&my_min, &global_min, 1, MPI_INT, MPI_MIN, 0, MPI_COMM_WORLD);
    if (rank == 0) {
        printf("Global min: %d\n", global_min);
    }

    MPI_Type_free(&CUSTOM_RESIZED_TYPE);
    MPI_Type_free(&CUSTOM_TYPE);
    MPI_Finalize();
    return 0;
}