#!/bin/bash

# Get file name.
folder=$1
# Loop over question words.
QSTNS="who what when where why how which whom"
for q in $QSTNS
do
  output="${folder}-${q}"
  python ".\\code\\writer.py" ".\\tests\\${folder}" "${q}" | cat > ${output}.answers
  # Generate response files.
  ./run.sh ${folder} ${q}
done
