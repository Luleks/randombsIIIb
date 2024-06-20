#include <stdio.h>
#include <stdlib.h>

#define BLOCK_SIZE 256
#define GRID_SIZE 256
int elements_per_reduction = BLOCK_SIZE * GRID_SIZE * 2;

__global__ void get_new_arr(int* a, int* b, int* c, int* n) {
    int tid_a = threadIdx.x + blockIdx.x * blockDim.x;

    while (tid_a < *n * *n) {
        int row = tid_a / *n;
        int col = tid_a % *n;
        int tid_b = col * *n + row;
        
        c[tid_a] = min(a[tid_a], b[tid_b]);
        tid_a += blockDim.x * gridDim.x;
    }    
}

__host__ void get_matrix_c(int* a, int* b, int* c, int* n) {
    int* dev_a, *dev_b, *dev_c, *dev_n;
    cudaMalloc((void**)&dev_a, *n * *n * sizeof(int));
    cudaMalloc((void**)&dev_b, (*n * *n + 1) * sizeof(int));
    cudaMalloc((void**)&dev_c, *n * *n * sizeof(int));
    cudaMalloc((void**)&dev_n, sizeof(int));

    cudaMemcpy(dev_a, a, *n * *n * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_b, b, *n * *n * sizeof(int), cudaMemcpyHostToDevice);
    cudaMemcpy(dev_n, n, sizeof(int), cudaMemcpyHostToDevice);

    get_new_arr<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, dev_c, dev_n);
    cudaMemcpy(c, dev_c, *n * *n * sizeof(int), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
    cudaFree(dev_c);
    cudaFree(dev_n);
}

__global__ void get_avgs(int* c, float* avgs, int n) {
    __shared__ float local_sum[BLOCK_SIZE];
    local_sum[threadIdx.x] = 0.0f;

    int tid = threadIdx.x * n + blockIdx.x;
    int col = blockIdx.x;
    while (col < n) {
        while (tid < n * n) {
            local_sum[threadIdx.x] += (float)c[tid] / n;
            tid += n * blockDim.x;
        }
        __syncthreads();

        for (int s = blockDim.x / 2; s > 0; s >>= 1) {
            if (threadIdx.x < s)
                local_sum[threadIdx.x] += local_sum[threadIdx.x + s];
            __syncthreads();
        }

        if (threadIdx.x == 0) {
            avgs[col] = local_sum[0];
        }     
        col += gridDim.x;
    }
}

__host__ void get_col_avgs(int* c, float* avgs, int n) {
    printf("N=%d\n", n);
    int* dev_a; float *dev_b;
    cudaMalloc((void**)&dev_a, n * n * sizeof(int));
    cudaMalloc((void**)&dev_b, n * sizeof(float));
    cudaMemcpy(dev_a, c, n * n * sizeof(int), cudaMemcpyHostToDevice);

    get_avgs<<<GRID_SIZE, BLOCK_SIZE>>>(dev_a, dev_b, n);
    cudaMemcpy(avgs, dev_b, n * sizeof(float), cudaMemcpyDeviceToHost);

    cudaFree(dev_a);
    cudaFree(dev_b);
}

int main() {
    int N = 5;
    // scanf("%d", &N);
    int* a = (int*)malloc(N * N * sizeof(int));
    int* b = (int*)malloc(N * N * sizeof(int));
    int* c = (int*)malloc(N * N * sizeof(int));

    for (int i = 0; i < N * N; ++i) {
        a[i] = rand() % 50;
        b[i] = rand() % 50;
    }

    get_matrix_c(a, b, c, &N);
    float* avg_c = (float*)malloc(N * sizeof(float));
    get_col_avgs(c, avg_c, N);

    for (int i = 0; i < N; ++i) {
        for (int j = 0; j < N; ++j) {
            printf("%5.2f ", (float)c[i * N + j]);
        }
        printf("\n");
    }
    printf("--------------------------------------\n");
    for (int i = 0; i < N; ++i)
        printf("%5.2f ", avg_c[i]);
    printf("\n");

    free(a);
    free(b);
    free(c);
    free(avg_c);
}