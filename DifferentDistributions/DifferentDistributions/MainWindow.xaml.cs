using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DifferentDistributions
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            _mainViewModel = this.DataContext as MainViewModel;
        }

        public void UpdateStatistics(double expectedValue, double dispersion, double squareDeviation)
        {
            LabelDeviation.Content = squareDeviation.ToString(CultureInfo.InvariantCulture);
            LabelDispersion.Content = dispersion.ToString(CultureInfo.InvariantCulture);
            LabelExpectedValue.Content = expectedValue.ToString(CultureInfo.InvariantCulture);
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                (ComboBoxStrategy.SelectionBoxItem as Strategy.Strategy)?.Calculate(this, _mainViewModel);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }        
        }

        public void GetValues(out int a, out double r0, out int m, out int max)
        {
            a = int.Parse(TextBoxA.Text, CultureInfo.InvariantCulture);
            r0 = int.Parse(TextBoxR0.Text, CultureInfo.InvariantCulture);
            m = int.Parse(TextBoxM.Text, CultureInfo.InvariantCulture);
            max = int.Parse(TextBoxMax.Text, CultureInfo.InvariantCulture);
        }

        public void GetGaussValues(out double matWait, out double dispersion)
        {
            matWait = double.Parse(TextBoxGaussExpected.Text, CultureInfo.InvariantCulture);
            dispersion = double.Parse(TextBoxGaussDispersion.Text, CultureInfo.InvariantCulture);
        }

        public void GetExpValues(out double lambda)
        {
            lambda = double.Parse(TextBoxExpLambda.Text, CultureInfo.InvariantCulture);
        }

        public void GetGammaValues(out double niu, out double lambda)
        {
            niu = int.Parse(TextBoxGammaNiu.Text, CultureInfo.InvariantCulture);
            lambda = double.Parse(TextBoxGammaLambda.Text, CultureInfo.InvariantCulture);
        }

        public void GetTriangleValue(out bool isMin)
        {
            if (ComboBoxTriangle.SelectedIndex == 0)
            {
                isMin = true;
            }
            else
            {
                isMin = false;
            }
        }
    }
}
