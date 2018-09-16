using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions
{
    public static class StatisticsHelper
    {
        public static void CalculateStatistics(List<double> collection, out double expectedValue,
            out double dispersion, out double squareDeviation)
        {
            expectedValue = collection.Sum() / collection.Count;
            var ev = expectedValue;
            dispersion = collection.Sum(number => Math.Pow(number - ev, 2)) / collection.Count;
            squareDeviation = Math.Sqrt(dispersion);
        }
    }
}