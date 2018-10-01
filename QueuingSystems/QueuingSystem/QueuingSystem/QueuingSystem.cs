using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QueuingSystem
{
    public class QueuingSystemClass
    {
        private static Random Random = new Random(DateTime.Now.Millisecond);

        private const int BufferSize = 2;
        private const int TickToNewRequest = 2;

        public double Pi1 { get; set; } = 0.5;
        public double Pi2 { get; set; } = 0.5;

        private int _rejectedRequestsCount = 0;
        private int _processedRequestsCount = 0;

        private int _currentTicksState = 2;
        private int _currentBufferState = 0;
        private int _currentPi1State = 0;
        private int _currentPi2State = 0;
        
        private List<int> bufferLength = new List<int>();
        private List<string> buf = new List<string>();

        public string ProcessTick()
        {
            _currentTicksState--;
            AddTick();
            if (_currentTicksState == 0)
            {
                _currentTicksState = 2;
                AddRequest();
            }

            bufferLength.Add(_currentBufferState);
            var str = $"P{_currentTicksState}{_currentBufferState}{_currentPi1State}{_currentPi2State}";
            buf.Add(str);
            return str + " --> ";
        }

        public void CalcStats(out double A, out double P, out double L, int bambard)
        {
            //var rez = buf.OrderBy(s => s).GroupBy(s => s).Select(grouping =>
            //{
            //    var r = ((double)grouping.Count() / bambard);
            //    return new {grouping.Key, r};
            //}).ToList();
            A = (double)_processedRequestsCount / bambard;
            P = (double)_rejectedRequestsCount / bambard * 2;
            L = (double)bufferLength.Sum() / bambard;
        }

        private void AddTick()
        {
            if (_currentPi1State == 0 && _currentPi2State == 0)
            {
                //TODO:dosmth
                return;
            }

            if (_currentPi1State == 1 && _currentPi2State == 0)
            {
                switch (Process2Probability(Pi1, 1 - Pi1))
                {
                    case 1:
                        return;
                    case 2:
                        _currentPi1State--;
                        _currentPi2State++;
                        if (_currentBufferState > 0)
                        {
                            _currentBufferState--;
                            _currentPi1State++;
                        }
                        break;
                }
                return;
            }

            if (_currentPi1State == 0 && _currentPi2State == 1)
            {
                switch (Process2Probability(Pi2, 1 - Pi2))
                {
                    case 1:
                        return;
                    case 2:
                        _currentPi2State--;
                        _processedRequestsCount++;
                        break;
                }
                return;
            }

            if (_currentPi1State == 1 && _currentPi2State == 1)
            {
                var state1 = Process2Probability(Pi1, 1 - Pi1);
                var state2 = Process2Probability(Pi2, 1 - Pi2);

                if (state1 == 1 && state2 == 1)
                {
                    return;
                }

                if (state1 == 1 && state2 == 2)
                {
                    _currentPi2State--;
                    _processedRequestsCount++;
                    return;
                }

                if (state1 == 2 && state2 == 1)
                {
                    _currentPi1State--;
                    _rejectedRequestsCount++;
                    if (_currentBufferState > 0)
                    {
                        _currentBufferState--;
                        _currentPi1State++;
                    }
                    return;
                }

                if (state1 == 2 && state2 == 2)
                {
                    _currentPi1State--;
                    _processedRequestsCount++;
                    if (_currentBufferState > 0)
                    {
                        _currentBufferState--;
                        _currentPi1State++;
                    }
                    return;
                }
            }
        }

        private void AddRequest()
        {
            if (_currentPi1State == 0)
            {
                _currentPi1State++;
                return;
            }

            if (_currentPi1State == 1 && _currentBufferState == 0)
            {
                _currentBufferState++;
                return;
            }

            if (_currentPi1State == 1 && _currentBufferState == 1)
            {
                _currentBufferState++;
                return;
            }

            _rejectedRequestsCount++;
        }


        private int Process2Probability(double p1, double p2)
        {
            var randomValue = Random.Next(101);
            if (randomValue <= p1 * 100)
            {
                return 1;
            }

            return 2;
        }
    }
}