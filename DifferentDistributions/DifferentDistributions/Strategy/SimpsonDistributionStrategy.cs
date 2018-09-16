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
                var randomValues = GenerateValues(a, r0, m, max).Where(d => d <= 0.5).ToList();
                var simpsonValue = SimpsonDistribution(randomValues).ToList();
                StatisticsHelper.CalculateStatistics(simpsonValue, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(simpsonValue);
                mainViewModel.UpdatePlotModel(model);
            }
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