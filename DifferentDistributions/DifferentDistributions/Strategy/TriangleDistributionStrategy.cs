﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace DifferentDistributions.Strategy
{
    public class TriangleDistributionStrategy : Strategy
    {
        public override string Name { get; set; } = "Triangle Distribution Strategy";
        public override void Calculate(MainWindow mainWindow, MainViewModel mainViewModel)
        {
            if (InputFieldsIsValid(mainWindow))
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
                var randomValues = GenerateValues(a, r0, m, max);
                mainWindow.GetTriangleValue(out bool isMin, out double A, out double B);
                var triangleValues = TriangleDistribution(randomValues, isMin, A, B).ToList();
                StatisticsHelper.CalculateStatistics(triangleValues, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(triangleValues);
                mainViewModel.UpdatePlotModel(model);
            }
        }

        private IEnumerable<double> TriangleDistribution(List<double> randomValues, bool isMin, double a, double b)
        {
            foreach (var randomValue in randomValues)
            {
                var randomValue1 = randomValues[Random.Next(randomValues.Count)];
                var randomValue2 = randomValues[Random.Next(randomValues.Count)];
                if (isMin)
                {
                    yield return a + ((b - a) * Math.Min(randomValue1, randomValue2));
                }
                else
                {
                    yield return a + ((b - a) * Math.Max(randomValue1, randomValue2));
                }
            }
        }
    }
}