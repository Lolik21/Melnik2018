using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QueuingSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BambardCount = 100000;
        private string Result = "";
        private int I = 0;
        private Timer timer = null; 
        public MainWindow()
        {
            InitializeComponent();
            OperationProgressBar.Maximum = BambardCount;
            timer = new Timer(50);
            timer.Elapsed += TimerOnElapsed;
        }

        private void TimerOnElapsed(object sender, ElapsedEventArgs e)
        {           
            var rez = "";
            var i = 0;
            lock (this)
            {
                if (Result == "") return;
                rez = Result + " ";
                i = I;
                Result = "";
            }

            Dispatcher.Invoke(() => UpdateTextBox(i, rez));
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                TextBoxOutPut.Clear();
                Result = "";
                I = 0;
                var pi1 = double.Parse(Pi1TextBox.Text, CultureInfo.InvariantCulture);
                var pi2 = double.Parse(Pi2TextBox.Text, CultureInfo.InvariantCulture);
                var system = new QueuingSystemClass
                {
                    Pi1 = pi1,
                    Pi2 = pi2
                };
                Task.Run(() =>
                {
                    for (var i = 0; i < BambardCount; i++)
                    {
                        lock (this)
                        {
                            Result = Result + system.ProcessTick();
                            I = i;                           
                        }
                        timer.Start();
                    }
                    system.CalcStats(out var A, out var P,out var L, BambardCount);
                    Dispatcher.Invoke(() =>
                    {
                        LabelAbsoluteProbability.Content = A.ToString(CultureInfo.InvariantCulture);
                        LabelProbabilityOfRegectiong.Content = P.ToString(CultureInfo.InvariantCulture);
                        LabelQueryLength.Content = L.ToString(CultureInfo.InvariantCulture);
                    });
                });
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            
            
        }

        private void UpdateTextBox(int i, string rez)
        {
            TextBoxOutPut.AppendText(rez);
            OperationProgressBar.Value = i;
        }
    }
}
