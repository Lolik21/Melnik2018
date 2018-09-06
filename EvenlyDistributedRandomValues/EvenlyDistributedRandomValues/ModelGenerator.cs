using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace EvenlyDistributedRandomValues
{
    public static class ModelGenerator
    {
        public static void GenerateModel(MainViewModel model)
        {

            CategoryAxis xaxis = new CategoryAxis
            {
                Position = AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                GapWidth = 0
            };

            LinearAxis yaxis = new LinearAxis
            {
                Position = AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Dot
            };

            ColumnSeries s1 = new ColumnSeries
            {
                IsStacked = true,
                Items = { new ColumnItem(0.3), new ColumnItem(0.5), new ColumnItem(0.6), new ColumnItem(0.9) }
            };

            var plotModel = new PlotModel
            {
                Title = "Normal Distribution Of Random Values",
                Background = OxyColors.Gray,
                Axes = { xaxis, yaxis },
                Series = { s1 },  
            };
            model.UpdatePlotModel(plotModel);
        }
    }
}