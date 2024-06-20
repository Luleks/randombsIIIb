#include <stdio.h>
#include <stdlib.h>
#include <limits.h>
#include <cuda_runtime.h>

#define BLOCK_SIZE 256
#define GRID_SIZE 256

__global__ void get_diag_min(int* a, int* b, int n) {
    __shared__ int partial_min[BLOCK_SIZE];
    partial_min[threadIdx.x] = INT_MAX;

    int row = threadIdx.x + blockIdx.x * blockDim.x;
    while (row < n) {
        int diag_idx = row * n + row;
        partial_min[threadIdx.x] = min(a[diag_idx], partial_min[threadIdx.x]);
        row += blockDim.x * gridDim.x;
    }
    __syncthreads();

    for (int s = blockDim.x / 2; s > 0; s >>= 1) {
        if (threadIdx.x < s) {
            partial_min[threadIdx.x] = min(partial_min[threadIdx.x], partial_min[threadIdx.x + s]);
        }
        __syncthreads();
    }

    if (threadIdx.x == 0) {
        b[blockIdx.x] = partial_min[0];
    }
}

__host__ int get_min(int* mat, int n) {
    int* dev_a, *dev_b;
    cudaMalloc((void**)&dev_a, n * n * sizeof(int));
    cudaMalloc((void**)&dev_b, GRID_SIZE * sizeof(int));
    cudaMemcpy(dev_a, mat, n * n * sizeof(int), cudaMemcpyHostToDevice);

    get_diag_min<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, n);

    int* partial_mins = (int*)malloc(GRID_SIZE * sizeof(int));
    cudaMemcpy(partial_mins, dev_b, GRID_SIZE * sizeof(int), cudaMemcpyDeviceToHost);

    int reduced_min = INT_MAX;
    for (int i = 0; i < GRID_SIZE; ++i) {
        reduced_min = min(reduced_min, partial_mins[i]);
    }

    free(partial_mins);
    cudaFree(dev_a);
    cudaFree(dev_b);

    return reduced_min;
}

int main() {
    int N = 720;
    
    int* matrix = (int*)malloc(N * N * sizeof(int));
    int actual_min = INT_MAX;

    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            matrix[i * N + j] = rand();
            if (i == j) actual_min = min(actual_min, matrix[i * N + j]);
        }
    }

    int reduced_min = get_min(matrix, N);

    printf("%d == %d\n", actual_min, reduced_min);

    free(matrix);
    return 0;
}
