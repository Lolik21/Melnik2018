using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class ExponentialDistributionStrategy : Strategy
    {
        public override string Name { get; set; } = "Exponential Distribution Strategy";
        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                var randomValues = GenerateValues(a, r0, m, max);
                mainWindow.GetExpValues(out double lambda);
                var expValues = ExponentialDistribution(randomValues, lambda).ToList();
                StatisticsHelper.CalculateStatistics(expValues, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(expValues);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        private IEnumerable<double> ExponentialDistribution(List<double> randomValues, double lambda)
        {
            foreach (var randomValue in randomValues)
            {
                yield return (-1) * lambda * Math.Log10(randomValue);
            }
        }
    }
}