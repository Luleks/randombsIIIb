#include <stdio.h>
#include "omp.h"

int is_prime(int n) {
    if (n <= 1) return 0;
    if (n <= 3) return 1;

    if (n % 2 == 0 || n % 3 == 0) return 0;

    for (int i = 5; i * i <= n; i += 6) {
        if (n % i == 0 || n % (i + 2) == 0) return 0;
    }

    return 1;
}

int main() {
    double start, end;
    int N = 10000000;
    int n_of_primes = 0;

    start = omp_get_wtime();
    for (int i = 1; i < N; ++i) {
        n_of_primes += is_prime(i);
    }
    end = omp_get_wtime();
    printf("Primes: %d Sequential time %lf\n", n_of_primes, end - start);

    n_of_primes = 0;
    start = omp_get_wtime();
    #pragma omp parallel for
    for (int i = 1; i < N; ++i) {
        #pragma omp critical
        n_of_primes += is_prime(i);
    }
    end = omp_get_wtime();
    printf("Primes: %d Parallel no reduction time %lf\n", n_of_primes, end - start);

    n_of_primes = 0;
    start = omp_get_wtime();
    #pragma omp parallel for reduction(+: n_of_primes)
    for (int i = 1; i < N; ++i) {
        n_of_primes += is_prime(i);
    }
    end = omp_get_wtime();
    printf("Primes: %d Parallel with reduction time %lf\n", n_of_primes, end - start);

    return 0;

}