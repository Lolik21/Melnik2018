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
                StatisticsHelper.CalculateStatistics(randomValues, out double expectedValue,
                    out double dispersion, out double squareDeviation);
                mainWindow.UpdateStatistics(expectedValue, dispersion, squareDeviation);
                var model = PlotModelGenerator.GenerateModel(randomValues);
                mainViewModel.UpdatePlotModel(model);
            }
        }
    }
}