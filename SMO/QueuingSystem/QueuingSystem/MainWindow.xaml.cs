using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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
using Timer = System.Timers.Timer;

namespace QueuingSystem
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const int BombardTime = 10000000;
        private string Result = "";
        private int I = 0;
        private Timer timer = null; 
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonCalculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                
                var peopleIntensity = double.Parse(Pi1TextBox.Text, CultureInfo.InvariantCulture);
                var carIntensity = double.Parse(Pi2TextBox.Text, CultureInfo.InvariantCulture);
                var system = new QueuingSystemClass
                {
                    PeopleIntensity = peopleIntensity,
                    CarIntensity = carIntensity
                };
                Task.Run(() =>
                {
                    Stopwatch SW = new Stopwatch();
                    SW.Start();
                    for (var i = 0; i < BombardTime; i++)
                    {
                        system.ProcessTick();
                    }
                    system.CalcStats(out var peopleQueryLength, out var carQueryLength,out var peopleWaitTime,
                        out var  carWaitTime,out var chanceToLeave, BombardTime);
                    SW.Stop();
                    Dispatcher.Invoke(() =>
                    {
                        LabelPeopleQueryLength.Content = peopleQueryLength.ToString(CultureInfo.InvariantCulture);
                        LabelVehicleQueryLength.Content = carQueryLength.ToString(CultureInfo.InvariantCulture);
                        LabelPeopleWait.Content = peopleWaitTime.ToString(CultureInfo.InvariantCulture);
                        LabelVehicleWait.Content = carWaitTime.ToString(CultureInfo.InvariantCulture);
                        ProbabilityToLeave.Content = chanceToLeave.ToString(CultureInfo.InvariantCulture);
                        TextBlock.Text = SW.Elapsed.ToString();
                    });
                });
                
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }                      
        }
    }
}
