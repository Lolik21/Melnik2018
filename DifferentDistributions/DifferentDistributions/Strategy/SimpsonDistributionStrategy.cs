using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class SimpsonDistributionStrategy : Strategy
    {
        public override string Name { get; set; } = "Simpson Distribution Strategy";
        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                mainWindow.GetSimpsValues(out double A, out double B);
                var randomValues = GenerateValues(a, r0, m, max, A / 2 ,B / 2);
                var simpsonValue = SimpsonDistribution(randomValues).ToList();
                StatisticsHelper.CalculateStatistics(simpsonValue, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(simpsonValue);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        private List<double> GenerateValues(int a, double r0, int m, int max, double A, double B)
        {
            double r1;
            var collection = new List<double>();
            var r2 = r0;
            for (int i = 0; i < max; i++)
            {
                r1 = (r0 * a) % m;
                var temp = A + (B-A) * (double)r1 / m;
                collection.Add(temp);
                r0 = r1;
            }

            return collection;
        }

        private IEnumerable<double> SimpsonDistribution(List<double> randomValues)
        {
            foreach (var randomValue in randomValues)
            {
                var randomValue1 = randomValues[Random.Next(randomValues.Count)];
                var randomValue2 = randomValues[Random.Next(randomValues.Count)];
                yield return randomValue1 + randomValue2;
            }
        }
    }
}