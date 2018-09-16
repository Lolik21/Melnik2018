using System;
using System.Collections.Generic;
using System.Windows;

namespace DifferentDistributions.Strategy
{
    public abstract class Strategy 
    {
        public abstract string Name { get; set; }

        public abstract void Calculate(MainWindow mainWindow, MainViewModel mainViewModel);

        protected static readonly Random Random = new Random(DateTime.Now.Millisecond);

        protected List<double> GenerateValues(int a, double r0, int m, int max)
        {
            double r1;
            var collection = new List<double>();
            var r2 = r0;
            for (int i = 0; i < max; i++)
            {
                r1 = (r0 * a) % m;
                collection.Add((double)r1 / m);
                r0 = r1;
            }

            return collection;
        }
        protected virtual bool InputFieldsIsValid(MainWindow mainWindow)
        {
            try
            {
                mainWindow.GetValues(out var a, out var r0, out var m, out var max);
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