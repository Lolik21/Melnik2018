using System;
using System.Collections.Generic;
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

namespace QueuingSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BambardCount = 100000;
        public MainWindow()
        {
            InitializeComponent();
            OperationProgressBar.Maximum = BambardCount;
        }

        private async void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            var system = new QueuingSystemClass();
            await Task.Run(() =>
            {
                for (var i = 0; i < BambardCount; i++)
                {
                    system.ProcessRequest();
                    Dispatcher.Invoke(() => OperationProgressBar.Value = i);
                }
            });
            
        }
    }
}
