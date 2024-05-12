%%cuda
#include <stdio.h>
#include <stdlib.h>

#define NUM_MAX 500.0f
#define BLOCK_SIZE 32
#define GRID_SIZE 32

__global__ void kernel(float* a, float* b, int* n) {
    __shared__ float local_block[BLOCK_SIZE];
    int tid = threadIdx.x + blockDim.x * blockIdx.x;

    if (tid >= n[0] + 2)
        return;

    local_block[threadIdx.x] = a[tid];

    if (tid >= n[0])
        return;

    __syncthreads();

    float s = 0;
    s += local_block[threadIdx.x] * 3;
    s += local_block[threadIdx.x + 1] * 10;
    s += local_block[threadIdx.x + 1] * 7;
    s /= 20.0f;
    b[tid] = s;
}

__host__ void init_and_call(float* a, float* b, int* n) {
    float* dev_a, *dev_b; int *dev_n;
    cudaMalloc((void**)&dev_a, (n[0] + 2) * sizeof(float));
    cudaMalloc((void**)&dev_b, n[0] * sizeof(float));
    cudaMalloc((void**)&dev_n, sizeof(int));

    cudaMemcpy(dev_a, a, (n[0] + 2) * sizeof(float), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_n, n, sizeof(int), cudaMemcpyHostToDevice);

    kernel<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, dev_n);

    cudaMemcpy(b, dev_b, n[0] * sizeof(int), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
    cudaFree(dev_n);
}

int main() {
    int* n = (int*)malloc(sizeof(int));

    n[0] = 10 + rand() % 20;
    printf("%d\n", n[0]);

    float* a, *b;
    a = (float*)malloc((n[0] + 2) * sizeof(int));
    b = (float*)malloc(n[0] * sizeof(int));

    for (int i = 0; i < n[0] + 2; ++i) {
        a[i] = ((float)rand() / RAND_MAX) * NUM_MAX;
    }

    init_and_call(a, b, n);

    for (int i = 0; i < n[0] + 2; ++i) {
        printf("%7.2f ", a[i]);
    }
    printf("\n        ");

    for (int i = 0; i < n[0]; ++i) {
        printf("%7.2f ", b[i]);
    }
    printf("\n");
    
    return 0;
}