using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace DifferentDistributions
{
    public class PlotModelGenerator
    {
        public static PlotModel GenerateModel(List<double> values)
        {
            var parameters = CalculateParameters(values, out double interval);
            return BuildModel(values, parameters, interval);
        }

        private static PlotModel BuildModel(List<double> values, IEnumerable<double> parameters, double interval)
        {
            
            var enumerable = parameters as double[] ?? parameters.ToArray();

            CategoryAxis xaxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                GapWidth = 0, 
            };

            xaxis.Labels.AddRange(enumerable.Select(d => (d * values.Count).ToString(CultureInfo.InvariantCulture)));

            LinearAxis yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                Maximum = Math.Round(enumerable.Max(), 3)
            };

            ColumnSeries s1 = new ColumnSeries
            {
                IsStacked = true,
            };

            s1.Items.AddRange(enumerable.Select(d => new ColumnItem(d)));

            var plotModel = new PlotModel
            {
                Background = OxyColors.White,
                Axes = { xaxis, yaxis },
                Series = { s1 },
            };

            return plotModel;
        }

        private static IEnumerable<double> CalculateParameters(List<double> values, out double interval)
        {
            var generatedValues = values;
            var matchPoints = new List<int>();
            var max = generatedValues.Max();
            var min = generatedValues.Min();
            var range = max - min;
            if (range == 0)
            {
                range = 1;
                var equalInterval = range / 20;
                interval = equalInterval;
                double left = 0;
                while (left < 1)
                {
                    var rightBorder = left + equalInterval;
                    matchPoints.Add(generatedValues.Count(value => (value >= left && value < rightBorder)));
                    left = left + equalInterval;
                }
                return matchPoints.Select((i => i / (double)generatedValues.Count));
            }
            var intervalLength = range / 20;
            interval = intervalLength;
            double leftBorder = min;
            while (Math.Round(leftBorder, 13) < Math.Round(max, 13))
            {
                var rightBorder = leftBorder + intervalLength;
                matchPoints.Add(generatedValues.Count(value => (value >= leftBorder && value < rightBorder)));
                leftBorder = leftBorder + intervalLength;
            }

            return matchPoints.Select((i => i / (double)generatedValues.Count));
        }
    }
}