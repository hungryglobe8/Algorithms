using System;
using System.Collections.Generic;
using System.Linq;

namespace GetShort
{
    public class PriorityQueue
    {
        private int size;
        private readonly int _capacity;
        private readonly double[] storage;

        public PriorityQueue(int capacity)
        {
            size = 0;
            _capacity = capacity;
            storage = new double[capacity];
        }

        public bool IsEmpty()
        {
            return size == 0;
        }

        public int DeleteMin()
        {
            int minIndex = -1;
            double min = -1;

            // find first nonzero element in storage
            for (int i = 0; i < _capacity; i++)
            {
                if (storage[i] != 0)
                {
                    minIndex = i;
                    min = storage[i];
                    break;
                }
            }

            // loop through rest of storage
            for (int i = minIndex; i < _capacity; i++)
            {
                double val = storage[i];
                if (val != 0 && val < min) 
                {
                    minIndex = i;
                    min = val;
                }
            }

            size--;
            storage[minIndex] = 0;
            return minIndex;
        }

        public void Update(int index, double val)
        {
            if (storage[index] == 0)
                size++;

            storage[index] = val;
        }
    }
}