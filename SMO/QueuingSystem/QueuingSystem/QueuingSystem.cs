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

        private const int CarBufferSize = 11;
        private const int PeopleBufferSize = 21;
         
        public double PeopleIntensity { get; set; } = 12;
        public double CarIntensity { get; set; } = 12;


        private int _currentCarBufferSize = 0;
        private int _currentPeopleBufferSize = 0;

        private int _leavedPeople = 0;
        private int _arrivedPeople = 0;
        
        private List<int> _carQueryLength = new List<int>();
        private List<int> _peopleQueryLength = new List<int>();

        public void ProcessTick()
        {
            AddTick();
            _carQueryLength.Add(_currentCarBufferSize);
            _peopleQueryLength.Add(_currentPeopleBufferSize);
        }

        public void CalcStats(out double peopleQueryLength, out double carQueryLength, out double peopleWaitTime,
            out double varWaitTime, out double chanceToLeave,int BombardTime)
        {
            peopleQueryLength = _peopleQueryLength.Sum() / (double)BombardTime;
            carQueryLength = _carQueryLength.Sum() / (double)BombardTime;
            peopleWaitTime = peopleQueryLength / PeopleIntensity + 1/ PeopleIntensity;
            varWaitTime = carQueryLength / CarIntensity + 1 / CarIntensity;

            chanceToLeave = _leavedPeople / (double)_arrivedPeople;
        }

        private void AddTick()
        {
            var isCarArrived = IsEventHappened(CarIntensity);
            var isPeopleArrived = IsEventHappened(PeopleIntensity);

            if (isCarArrived && isPeopleArrived)
            {
                _arrivedPeople++;
                _leavedPeople++;
                return;
            }

            if (isPeopleArrived)
            {
                if (_currentCarBufferSize > 0)
                {
                    _currentCarBufferSize--;                    
                    _leavedPeople++;
                }
                else
                {
                    if (PeopleBufferSize != _currentPeopleBufferSize)
                    {
                        _currentPeopleBufferSize++;                        
                    }
                }
                _arrivedPeople++;
                return;
            }

            if (isCarArrived)
            {
                if(_currentPeopleBufferSize > 0)
                {
                    _currentPeopleBufferSize--;
                    _leavedPeople++;
                }
                else
                {
                    if (CarBufferSize != _currentCarBufferSize)
                    {
                        _currentCarBufferSize++;
                    }
                }               
            }

        }

        public bool IsEventHappened(double intensity)
        {
            var randomValue = Random.Next(60);
            if (randomValue < intensity)
            {
                return true;
            }
            return false;
        }
    }
}