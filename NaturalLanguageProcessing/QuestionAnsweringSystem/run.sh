#!/bin/bash

# Get file name.
file=$1
# Set question type as "ALL".
question=$2
output="${file}-${question}"
# Run qa system on input file (designated by command line argument).
python ".\\code\\qa.py" ".\\tests\\${file}.input" "${question}" > "${output}.response"

# Get all answer files.
#cat tests/${file}/*.answers > ${output}.answers

# Score using perl.
perl scoring/score-answers.pl ${output}.response ${output}.answers > ${output}.score
