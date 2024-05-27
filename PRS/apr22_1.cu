/*

UNFINISHED

*/

#include <stdio.h>
#include <stdlib.h>


__global__ void kernel(int* a, int* b, int* n) {
    if (threadIdx.x >= *n || threadIdx.y >= *n)
        return;
    
    int tid

    b[threadIdx.x][threadIdx.y] = a[threadIdx.x][threadIdx.y];
}

__host__ void initAndCall(int* a, int* b, int n) {
    int* dev_a, *dev_b, *dev_n;
    cudaMalloc((void**)&dev_a, n * n * sizeof(int));
    cudaMalloc((void**)&dev_b, n * n * sizeof(int));
    cudaMalloc((void**)&dev_n, sizeof(int));
    cudaMemcpy(dev_a, a, n * n * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_n, n, sizeof(int), cudaMemcpyHostToDevice);

    dim3 blockDim(256, 256);
    dim3 gridDim(256, 256);

    kernel<<<gridDim, blockDim>>>(dev_a, dev_b);

    cudaMemcpy(b, dev_b, n * n * sizeof(int), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
    cudaFree(dev_n);
}

int main() {

}