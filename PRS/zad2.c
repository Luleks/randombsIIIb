#include <stdio.h>
#include "omp.h"

void initializMatrix(int* matrix, int n, int m, int ascending) {
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            matrix[i * m + j] = ascending ? i * m + j : 1; 
        }
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

int main() {
    double start, end;
    int m = 700;
    int n = 700;
    int a[m][n];
    initializMatrix((int*)a, m, n, 0);

    start = omp_get_wtime();
    #pragma omp parallel for
    for (int i = 0; i < m; ++i) {
        for (int j = 2; j < n; ++j) {
            a[i][j] = 2 * a[i][j - 2];
        }
    }
    end = omp_get_wtime();
    printf("Bez menjanja redosleda: %lf\n", end - start);
    // printMatrix((int*)a, m, n);
    initializMatrix((int*)a, m, n, 0);

    start = omp_get_wtime();
    for (int j = 2; j < n; ++j) {
        #pragma omp parallel for
        for (int i = 0; i < m; ++i) {
            a[i][j] = 2 * a[i][j - 2];
        }
    }
    end = omp_get_wtime();
    printf("Sa promenjenim redosleda: %lf\n", end - start);
    // printMatrix((int*)a, m, n);

    return 0;
}