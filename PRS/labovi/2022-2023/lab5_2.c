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

int main() {
    double start, end;
    int m = 700;
    int n = 700;
    int a[m][n];
    initializMatrix((int*)a, m, n, 0);

    // for (int i = 0; i < m; ++i) {
    //     for (int j = 2; j < n; ++j) {
    //         printf("(%d %d) <- (%d %d)\n", i, j, i, j - 2);
    //     }
    // }

    start = omp_get_wtime();
    #pragma omp parallel for
    for (int i = 0; i < m; ++i) {
        for (int j = 2; j < n; ++j) {
            a[i][j] = 2 * a[i][j - 2];
        }
    }
    end = omp_get_wtime();
    // printMatrix((int*)a, m, n);
    printf("Bez menjanja redosleda: %lf\n", end - start);
    initializMatrix((int*)a, m, n, 0);

    // for (int j = 2; j < m; ++j) {
    //     for (int i = 0; i < n; ++i) {
    //         printf("(%d %d) <- (%d %d)\n", i, j, i, j - 2);
    //     }
    // }

    start = omp_get_wtime();
    #pragma omp parallel for
    for (int j = 2; j < m; ++j) {
        for (int i = 0; i < n; ++i) {
            a[i][j] = 2 * a[i][j - 2];
        }
    }
    end = omp_get_wtime();
    // printMatrix((int*)a, m, n);
    printf("Sa menjanjanjem redosleda: %lf\n", end - start);

    return 0;
}