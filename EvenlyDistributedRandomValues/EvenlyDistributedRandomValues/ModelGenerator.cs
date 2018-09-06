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
            var parameters = CalculateParameters(model);
            BuildModel(model, parameters);
        }

        private static void BuildModel(MainViewModel model, IEnumerable<double> parameters)
        {
            CategoryAxis xaxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                GapWidth = 0,              
            };
            var enumerable = parameters as double[] ?? parameters.ToArray();

            xaxis.Labels.AddRange(enumerable.Select((d => d.ToString(CultureInfo.InvariantCulture))));

            LinearAxis yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Dot,
                Minimum = 0,
                Maximum = enumerable.Max()
            };

            ColumnSeries s1 = new ColumnSeries
            {
                IsStacked = true,
            };
            s1.Items.AddRange(enumerable.Select(d => new ColumnItem(d)));

            var plotModel = new PlotModel
            {
                Title = "Normal Distribution Of Random Values",
                Background = OxyColors.Gray,
                Axes = { xaxis, yaxis },
                Series = { s1 },
            };
            model.UpdatePlotModel(plotModel);
        }

        private static IEnumerable<double> CalculateParameters(MainViewModel model)
        {
            var generatedValues = model.GeneratedValues;
            var matchPoints = new List<int>();
            var max = generatedValues.Max();
            var min = generatedValues.Min();
            var range  = max - min;
            if (range == 0)
            {
                range = 1;
                var interval = range / 20;
                double left= 0;
                while (left < 1)
                {
                    var rightBorder = left + interval;
                    matchPoints.Add(generatedValues.Count(value => (value >= left && value < rightBorder)));
                    left = left + interval;
                }
                return matchPoints.Select((i => i / (double)generatedValues.Count));
            }
            var intervalLength = range / 20;
            double leftBorder = min;
            while (leftBorder < max)
            {
                var rightBorder = leftBorder + intervalLength;
                matchPoints.Add(generatedValues.Count(value => (value >= leftBorder && value < rightBorder)));
                leftBorder = leftBorder + intervalLength;
            }
            return matchPoints.Select((i => i / (double)generatedValues.Count));   
        }
    }
}