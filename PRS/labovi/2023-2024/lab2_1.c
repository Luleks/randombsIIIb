#include <stdio.h>
#include <stdlib.h>
#include "mpi.h"

int main(int argc, char* argv[]) {
    MPI_Init(&argc, &argv);
    int rank, size;
    MPI_Comm_rank(MPI_COMM_WORLD, &rank);
    MPI_Comm_size(MPI_COMM_WORLD, &size);
    int k = size;
    int A[k][k];

    MPI_Datatype datatypes[size - 1];
    MPI_Datatype resized_datatypes[size - 1];
    for (int i = 0; i < size - 1; ++i) {
        MPI_Type_vector(size - 1 - i, 1, size - 1, MPI_INT, &datatypes[i]);
        MPI_Type_create_resized(datatypes[i], 0, (size + 1) * (i + 1) * sizeof(int), &resized_datatypes[i]);
        MPI_Type_commit(&resized_datatypes[i]);
    }

    MPI_Datatype RECV_TYPE, RESIZED_RECV_TYPE;
    MPI_Type_vector(size - rank, 1, k, MPI_INT, &RECV_TYPE);
    MPI_Type_create_resized(RECV_TYPE, 0, sizeof(int), &RESIZED_RECV_TYPE);
    MPI_Type_commit(&RESIZED_RECV_TYPE);

    if (rank == 0) {
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j) {
                A[i][j] = i * k + j;
            }
        }

        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j) {
                printf("%d ", A[i][j]);
            }
            printf("\n");
        }
        printf("\n");

        for (int i = 1; i < size; ++i) {
            MPI_Send(&A[0][size - 1 - i], 2, resized_datatypes[i - 1], i, 88, MPI_COMM_WORLD);
        }
    }
    else {
        for (int i = 0; i < k; ++i) {
            for (int j = 0; j < k; ++j) {
                A[i][j] = 0;
            }
        }
        MPI_Recv(&A[0][size - 2], 2, RESIZED_RECV_TYPE, 0, 88, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
        int print_signal = -1;
        if (rank == 1) {
            printf("Process %d has\n", rank);
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j)
                    printf("%d ", A[i][j]);
                printf("\n");
            }
            printf("\n");
            fflush(stdout);
            MPI_Send(&print_signal, 1, MPI_INT, rank + 1, 88, MPI_COMM_WORLD);
        }
        else if (rank != size - 1) {
            MPI_Recv(&print_signal, 1, MPI_INT, rank - 1, 88, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            printf("Process %d has\n", rank);
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j)
                    printf("%d ", A[i][j]);
                printf("\n");
            }
            printf("\n");
            fflush(stdout);
            MPI_Send(&print_signal, 1, MPI_INT, rank + 1, 88, MPI_COMM_WORLD);
        }
        else {
            MPI_Recv(&print_signal, 1, MPI_INT, rank - 1, 88, MPI_COMM_WORLD, MPI_STATUS_IGNORE);
            printf("Process %d has\n", rank);
            for (int i = 0; i < size; ++i) {
                for (int j = 0; j < size; ++j)
                    printf("%d ", A[i][j]);
                printf("\n");
            }
            printf("\n");
            fflush(stdout);
        }
    }


    for (int i = 0; i < size - 1; ++i) {
        MPI_Type_free(&datatypes[i]);
        MPI_Type_free(&resized_datatypes[i]);
    }
    MPI_Type_free(&RESIZED_RECV_TYPE);
    MPI_Type_free(&RECV_TYPE);
    MPI_Finalize();
    return 0;
}