#!/bin/sh

if [ "$#" -ne 2 ]; then
    echo "Usage: $0 <C file> <number of processes>"
    exit 1
fi

c_file="$1"
num_processes="$2"

if [ ! -f "$c_file" ]; then
    echo "Error: $c_file not found."
    exit 1
fi

mpicc "$c_file" -o program -lm

mpiexec -np "$num_processes" ./program

rm -f program