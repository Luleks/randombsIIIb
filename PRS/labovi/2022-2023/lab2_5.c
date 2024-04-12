#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int n = size + 1;
    if (rank == 0) {
        while (n % size != 0 || n % 2 != 0) {
            printf("n (divisible by %d and 2) ", size);
            fflush(stdout);
            scanf("%d", &n);
        }
    }
    MPI_Bcast(&n, 1, MPI_INT, 0, MPI_COMM_WORLD);
    int matrix[n][n];
    if (rank == 0) {
        for (int i = 0; i < n; ++i) {
            for (int j = 0; j < n; ++j)
                matrix[i][j] = i * n + j;
        }
    }

    MPI_Datatype ROW_TYPE, RESIZED_ROW_TYPE;
    MPI_Type_vector(n / 2, n / size, 2 * n, MPI_INT, &ROW_TYPE);
    MPI_Type_create_resized(ROW_TYPE, 0, n / size * sizeof(int), &RESIZED_ROW_TYPE);
    MPI_Type_commit(&RESIZED_ROW_TYPE);

    int my_data[n * n / (2 * size)];
    MPI_Scatter(matrix, 1, RESIZED_ROW_TYPE, my_data, n * n / ( 2 * size), MPI_INT, 0, MPI_COMM_WORLD);

    int my_max = my_data[0], global_max;
    for (int i = 1; i < n * n / (2 * size); ++i) {
        if (my_data[i] > my_max)
            my_max = my_data[i];
    }
    MPI_Reduce(&my_max, &global_max, 1, MPI_INT, MPI_MAX, 0, MPI_COMM_WORLD);
    
    if (rank == 0) {
        printf("%d\n", global_max);
    }

    MPI_Type_free(&RESIZED_ROW_TYPE);
    MPI_Type_free(&ROW_TYPE);
    MPI_Finalize();
    return 0;
}