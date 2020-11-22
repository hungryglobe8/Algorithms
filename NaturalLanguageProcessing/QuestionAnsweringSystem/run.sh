#!/bin/bash

# Get file name.
file=$1
# Set question type as "ALL".
question="ALL"
output="${file}-${question}"
# Run qa system on input file (designated by command line argument).
python ".\\code\\qa.py" ".\\tests\\${file}.input" > "${output}.response"

# Get answer files.
cat tests/${file}/*.answers > ${output}.answers

# Score using perl.
perl scoring/score-answers.pl ${output}.response ${output}.answers > ${output}.score
