using System;

namespace MrAnaga
{
    public class RandomNumberGenerator : IRandomNumberGenerator
    {
        Random _random;
        public RandomNumberGenerator()
        {
            _random = new Random();
        }
        public int Next(int minValue, int maxValue)
        {
            return _random.Next(minValue, maxValue);
        }
    }
}
