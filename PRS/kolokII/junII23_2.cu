#include <stdio.h>
#include <stdlib.h>

#define BLOCK_SIZE 4
#define GRID_SIZE 3


__global__ void kernel(int* a, int* b, int* n) {
    __shared__ int local_a[BLOCK_SIZE + 2];

    int tid = threadIdx.x + blockIdx.x * blockDim.x;
    while (tid < *n) {
        local_a[threadIdx.x] = a[tid];

        if (tid >= *n - 2) {
            return;
        }
        if (threadIdx.x < 2) {
            local_a[threadIdx.x + blockDim.x] = a[tid + blockDim.x];
        }
        __syncthreads();

        int temp = (local_a[threadIdx.x] + local_a[threadIdx.x + 1] + local_a[threadIdx.x + 2]) /
        (local_a[threadIdx.x] * local_a[threadIdx.x + 1] * local_a[threadIdx.x + 2]);
        b[tid] = temp;

        tid += BLOCK_SIZE * GRID_SIZE;
    }
}

__host__ void initAndCall(int* a, int* b, int n) {
    int* dev_a, *dev_b, *dev_n;
    cudaMalloc((void**)&dev_a, (n + 2) * sizeof(int));
    cudaMalloc((void**)&dev_b, (n - 2) * sizeof(int));
    cudaMalloc((void**)&dev_n, sizeof(int));

    cudaMemcpy(dev_a, a, n * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_n, &n, sizeof(int), cudaMemcpyHostToDevice);

    kernel<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, dev_n);

    cudaMemcpy(b, dev_b, (n - 2) * sizeof(int), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
    cudaFree(dev_n);    

}

int main() {
    int n = 33;
    
    int* a = (int*)malloc(n * sizeof(int));
    int* b = (int*)malloc((n - 2) * sizeof(int));

    for(int i = 0; i < n; ++i) {
        a[i] = 1;
    }
    for (int i = 0; i < n - 2; ++i) {
        b[i] = 69;
    }

    initAndCall(a, b, n);

    for (int i = 0; i < n - 2; ++i)
      printf("%3d ", i + 1);
    printf("\n");
    for (int i = 0; i < n - 2; ++i)
        printf("%3d ", b[i]);
    printf("\n");

    free(a);
    free(b);
}