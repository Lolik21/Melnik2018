using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
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
using static System.Int32;

namespace EvenlyDistributedRandomValues
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _mainViewModel;

        public MainWindow()
        {
            _mainViewModel = new MainViewModel();
            InitializeComponent();
            this.DataContext = _mainViewModel;
        }

        private void btnCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (InputFieldsIsValid())
            {
                GetValues(out var a, out var r0, out var m, out var max);
                GenerateValues(a, r0, m, max);
            }
        }

        private void GenerateValues(int a, double r0, int m, int max)
        {
            double r1;
            var collection = new List<double>();
            for (int i = 0; i < max; i++)
            {
                r1 = (r0 * a) % m;
                collection.Add((double)r1 / m);
                r0 = r1;
            }
            CalculateStatistics(collection);
            _mainViewModel.GeneratedValues = collection;
            ModelGenerator.GenerateModel(_mainViewModel);
        }

        private void CalculateStatistics(List<double> collection)
        {
            var expectedValue = collection.Sum() / collection.Count;
            var dispersion = collection.Sum(number => Math.Pow(number - expectedValue, 2)) / collection.Count;
            var squareDeviation = Math.Sqrt(dispersion);
            lblExpectedValue.Content = expectedValue;
            lblDispersion.Content = dispersion;
            lblSquareDeviation.Content = squareDeviation;
        }

        private void GetValues(out int a, out double r0, out int m, out int max)
        {
             a = Parse(TextBoxA.Text);
             r0 = Parse(TextBoxR0.Text);
             m = Parse(TextBoxM.Text);
             max = Parse(TextBoxMax.Text);
        }

        private bool InputFieldsIsValid()
        {
            try
            {
                GetValues(out var a, out var r0, out var m, out var max);
                if (a < 0 || (r0 < 0) || m < 0 || max < 0)
                {
                    throw new Exception("Values MUST be positive!");
                }

                return true;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return false;
        }
    }
}
