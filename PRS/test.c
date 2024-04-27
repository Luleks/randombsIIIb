#include <stdio.h>
#include <stdlib.h>


int main(int argc, char* argv) {
    int n = 3, m = 4;
    int* matrix_arr = (int*)malloc(n * m * sizeof(int));
    int** matrix = (int**)malloc(n * sizeof(int*));
    for (int i = 0; i < n; ++i)
        matrix[i] = &(matrix_arr[i * m]);
    
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            matrix[i][j] = i * m + j;
        }
    }
    
    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            printf("%d ", matrix[i][j]);
        }
        printf("\n");
    }

    for (int i = 0; i < n; ++i) {
        for (int j = 0; j < m; ++j) {
            printf("%ld ", &matrix[i][j]);
        }
        printf("\n");
    }
}