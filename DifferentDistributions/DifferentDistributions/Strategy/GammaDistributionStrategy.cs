using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class GammaDistributionStrategy : Strategy
    {
        public override string Name { get; set; } = "Gamma Distribution Strategy";
        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                var randomValues = GenerateValues(a, r0, m, max);
                mainWindow.GetGammaValues(out double niu, out double lambda);
                var gammaValues = GammaDistribution(randomValues, niu, lambda).ToList();
                StatisticsHelper.CalculateStatistics(gammaValues, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(gammaValues);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        protected override bool InputFieldsIsValid(MainWindow mainWindow)
        {
            bool IsValid = false;
            mainWindow.GetGammaValues(out double niu, out double lambda);
            if (lambda > 0 && niu > 0)
            {
                IsValid = true;
            }

            return  IsValid && base.InputFieldsIsValid(mainWindow);
        }

        private IEnumerable<double> GammaDistribution(List<double> randomValues, double niu, double lambda)
        {
            foreach (var randomValue in randomValues)
            {
                var randomIndexes = new List<int>();
                for (int i = 0; i < niu; i++)
                {
                    randomIndexes.Add(Random.Next(randomValues.Count));
                }
                yield return (-1) * randomIndexes.Sum(i => Math.Log10(randomValues[i]));
            }
        }
    }
}