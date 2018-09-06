using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using EvenlyDistributedRandomValues.Annotations;
using OxyPlot;

namespace EvenlyDistributedRandomValues
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            GeneratedValues = new List<double>();
        }
        public List<double> GeneratedValues { get; set; }
        public PlotModel PlotModel { get; set; }
        
        public void UpdatePlotModel(PlotModel newModel)
        {
            PlotModel = newModel;
            OnPropertyChanged(nameof(PlotModel));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}