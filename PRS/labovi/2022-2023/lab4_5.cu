%%cuda
#include <stdio.h>

#define BLOCK_SIZE 32
#define GRID_SIZE 32

__global__ void kernel(int* a, int* b, int* c, int* n) {
    int tid = threadIdx.x + blockIdx.x * blockDim.x;
    
    while (tid < n[0]) {
        c[tid] = a[tid] * b[tid];
        tid += n[0];
    }
}

__host__ void init_and_call(int* a, int* b, int* c, int* n) {
    int* dev_a, *dev_b, *dev_c, *dev_n;
    cudaMalloc((void**)&dev_a, n[0] * sizeof(int));
    cudaMalloc((void**)&dev_b, n[0] * sizeof(int));
    cudaMalloc((void**)&dev_c, n[0] * sizeof(int));
    cudaMalloc((void**)&dev_n, sizeof(int));
    cudaMemcpy(dev_a, a, n[0] * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_b, b, n[0] * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_n, n, sizeof(int), cudaMemcpyHostToDevice);

    kernel<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, dev_c, dev_n);

    cudaMemcpy(c, dev_c, n[0] * sizeof(int), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
    cudaFree(dev_n);
}

int main() {
    int* n = (int*)malloc(sizeof(int));

    n[0] = 10 + rand() % 100;
    int* a, *b, *c;
    a = (int*)malloc(n[0] * sizeof(int));
    b = (int*)malloc(n[0] * sizeof(int));
    c = (int*)malloc(n[0] * sizeof(int));

    for (int i = 0; i < n[0]; ++i) {
        a[i] = i;
        b[i] = i;
    }

    for (int i = 0; i < n[0]; ++i) {
        printf("%3d ", a[i]);
    }
    printf("\n");
    for (int i = 0; i < n[0]; ++i) {
        printf("%3d ", b[i]);
    }
    printf("\n");

    init_and_call(a, b, c, n);

    for (int i = 0; i < n[0]; ++i) {
        printf("%3d ", c[i]);
    }
    printf("\n");
    return 0;
}