using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class GaussianDistributionStrategy : Strategy
    {
        private const int N = 6;
        public override string Name { get; set; } = "Gaussian Distribution Strategy";


        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                var randomValues = GenerateValues(a, r0, m, max);
                mainWindow.GetGaussValues(out double matWait, out double gaussDispersion);
                var gaussValues = GaussianDistribution(randomValues, matWait, gaussDispersion);

                StatisticsHelper.CalculateStatistics(gaussValues, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(gaussValues);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        protected override bool InputFieldsIsValid(MainWindow mainWindow)
        {
            bool isValid = false;          
            mainWindow.GetGaussValues(out double matWait, out double dispersion);
            if (dispersion >= 0)
            {
                isValid = true;
            }

            return isValid && base.InputFieldsIsValid(mainWindow); ;
        }

        private List<double> GaussianDistribution(List<double> randomValues, double matWait, double gaussDispersion)
        {
            
            var result = new List<double>();
            var randomIndexes = new List<int>();
            
            foreach (var randomValue in randomValues)
            {
                for (int i = 0; i < N; i++)
                {
                    randomIndexes.Add(Random.Next(randomValues.Count));
                }
                var newValue = matWait + gaussDispersion * Math.Sqrt(2) * (randomIndexes.Sum((i => randomValues[i])) - 3);
                result.Add(newValue);
                randomIndexes.Clear();
            }

            return result;
        }
    }
}