using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace EvenlyDistributedRandomValues
{
    public static class ModelGenerator
    {
        public static void GenerateModel(MainViewModel model)
        {
            var parameters = CalculateParameters(model, out double interval);
            BuildModel(model, parameters, interval);
        }

        private static void BuildModel(MainViewModel model, IEnumerable<double> parameters, double interval)
        {
            CategoryAxis xaxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                GapWidth = 0,              
            };
            var enumerable = parameters as double[] ?? parameters.ToArray();
            xaxis.Labels.AddRange(enumerable.Select(d => Math.Round(interval, 3).ToString(CultureInfo.InvariantCulture)));

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
                Title = "Normal Distribution Of Random Values",
                Background = OxyColors.White,
                Axes = { xaxis, yaxis },
                Series = { s1 },
            };
            model.UpdatePlotModel(plotModel);
        }

        private static IEnumerable<double> CalculateParameters(MainViewModel model, out double interval)
        {
            var generatedValues = model.GeneratedValues;
            var matchPoints = new List<int>();
            var max = generatedValues.Max();
            var min = generatedValues.Min();
            var range  = max - min;
            if (range == 0)
            {
                range = 1;
                var equalInterval = range / 20;
                interval = equalInterval;
                double left= 0;
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
            while (Math.Round(leftBorder,13) < Math.Round(max, 13))
            {
                var rightBorder = leftBorder + intervalLength;
                matchPoints.Add(generatedValues.Count(value => (value >= leftBorder && value < rightBorder)));
                leftBorder = leftBorder + intervalLength;
            }
            
            return matchPoints.Select((i => i / (double)generatedValues.Count));   
        }
    }
}