using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace QueuingSystem
{
    public class QueuingSystemClass
    {
        private const int BufferSize = 2;
        private const int TiksToNewRequest = 2;

        private int _regectedRequestsCount = 0;
        private int _processedRequestsCount = 0;

        private int _currentTicksState = 2;
        private int _currentBufferState = 0;
        private int _currentPi1State = 0;
        private int _currentPi2State = 0;
        
        public void ProcessRequest()
        {

        }
    }
}