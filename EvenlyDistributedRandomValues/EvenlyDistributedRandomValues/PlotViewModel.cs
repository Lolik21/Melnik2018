using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EvenlyDistributedRandomValues.Annotations;

namespace EvenlyDistributedRandomValues
{
    public class PlotViewModel : INotifyPropertyChanged
    {
        public PlotModel PlotModel { get; set; }

        public void UpdateModel(PlotModel newModel)
        {
            PlotModel = newModel;
            OnPropertyChanged(nameof(PlotModel));
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
