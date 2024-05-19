#include <stdio.h>
#include <stdlib.h>
#include "omp.h"

void initializeVector(int* vec, int n, int ascending) {
    for (int i = 0; i < n; ++i) {
        vec[i] = ascending ? i : 1;
    }
}

void printVector(int* vec, int n) {
    for (int i = 0; i < n; ++i) {
        printf("%4d ", vec[i]);
    }
    printf("\n");
}

int main() {
    int n = 100000000;
    int* a = (int*)malloc(n * sizeof(int)), *b = (int*)malloc(n * sizeof(int)), c = 0;
    initializeVector(a, n, 0);
    initializeVector(b, n, 0);
    double start, end;

    omp_set_num_threads(10);

    start = omp_get_wtime();
    for (int i = 0; i < n; ++i)
        c += a[i] * b[i];
    end = omp_get_wtime();
    printf("result: %d sequential time: %lf\n", c, end - start);

    c = 0;
    start = omp_get_wtime();
    #pragma omp parallel for
    for (int i = 0; i < n; ++i) {
        int temp = a[i] * b[i];
        #pragma omp critical
        c += temp;
    }
    end = omp_get_wtime();
    printf("result: %d non reduction time: %lf\n", c, end - start);

    c = 0;
    start = omp_get_wtime();
    #pragma omp parallel for reduction(+: c)
    for (int i = 0; i < n; ++i) {
        c += a[i] * b[i];
    }
    end = omp_get_wtime();
    printf("result: %d reduction time: %lf\n", c, end - start);

    free(a);
    free(b);

}