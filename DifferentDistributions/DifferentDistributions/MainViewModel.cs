using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using DifferentDistributions.Strategy;
using OxyPlot;

namespace DifferentDistributions
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public PlotModel PlotModel { get; set; }

        public List<Strategy.Strategy> Strategies { get; set; } = new List<Strategy.Strategy>
        {
            new UniformDistributionStrategy(),
            new GaussianDistributionStrategy(),
            new ExponentialDistributionStrategy(),
            new GammaDistributionStrategy(),
            new TriangleDistributionStrategy(),
            new SimpsonDistributionStrategy()
        };


        public void UpdatePlotModel(PlotModel newModel)
        {
            PlotModel = newModel;
            OnPropertyChanged(nameof(PlotModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}