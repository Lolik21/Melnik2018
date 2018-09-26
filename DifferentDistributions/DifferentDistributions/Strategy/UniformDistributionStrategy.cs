using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class UniformDistributionStrategy : Strategy
    {
        public override string Name { get; set; } = "Uniform Distribution Strategy";

        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                var randomValues = GenerateValues(a, r0, m, max);
                mainWindow.GetUniformValues(out double A, out double B);
                var uniform = UniformDistribution(randomValues, A, B).ToList();
                StatisticsHelper.CalculateStatistics(uniform, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(uniform);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        private IEnumerable<double> UniformDistribution(List<double> randomValues, double a, double b)
        {
            foreach (var item in randomValues)
            {
                yield return a + ((b - a) * item);
            }
        }
    }
}