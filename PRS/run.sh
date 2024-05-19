#!/bin/sh

if [ "$#" -ne 1 ]; then
    echo "Usage: $0 <C file>"
    exit 1
fi

c_file="$1"

if [ ! -f "$c_file" ]; then
    echo "Error: $c_file not found."
    exit 1
fi

gcc -fopenmp -o program "$c_file" -lm

./program

rm -f program