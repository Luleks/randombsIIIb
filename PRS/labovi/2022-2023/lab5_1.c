#include <stdio.h>
#include "omp.h"

void initializMatrix(int* matrix, int n, int m, int ascending) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            matrix[i * m + j] = ascending ? i * m + j : 1; 
        }
    }
}

void initializeVector(int* vec, int n, int ascending) {
    for (int i = 0; i < n; ++i) {
        vec[i] = ascending ? i : 1;
    }
}

void printMatrix(int* matrix, int n, int m) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            printf("%4d ", matrix[i * m + j]);
        }
        printf("\n");
    }
}

void printVector(int* vec, int n) {
    for (int i = 0; i < n; ++i) {
        printf("%4d ", vec[i]);
    }
    printf("\n");
}

int main(int argc, char* argv[]) {
    int N = 600;
    int A[N][N], B[N][N], C[N][N], D[N][N];
    initializMatrix((int*)A, N, N, 0);
    initializMatrix((int*)B, N, N, 0);
    double start, end;

    omp_set_num_threads(50);

    start = omp_get_wtime();
    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            C[i][j] = 0;
            for (int k = 0; k < N; ++k) {
                C[i][j] += A[i][k] * B[k][j];
            }
        }
    }
    end = omp_get_wtime();
    // printMatrix((int*)C, N, N);
    printf("Sequential time %lf\n", end - start);

    start = omp_get_wtime();
    #pragma omp parallel for // schedule(dynamic)
        for (int i = 0; i < N; ++i) {
            for (int j = 0; j < N; ++j) {
                D[i][j] = 0;
                for (int k = 0; k < N; ++k) {
                    D[i][j] += A[i][k] * B[k][j];
                }
            }
        }
    end = omp_get_wtime();
    // printMatrix((int*)D, N, N);
    printf("Parallel time %lf\n", end - start);
    

    return 0;
}