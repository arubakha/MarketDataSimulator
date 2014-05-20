using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomItemsGenerator.Business
{
    //EventArgs
    public class ConsumerEventArgs : EventArgs
    {
        public object Data { get; private set; }
        public int QueueCount { get; private set; }

        public ConsumerEventArgs(object data, int queueCount)
        {
            Data = data;
            QueueCount = queueCount;
        }
    }

    //Event
    public delegate void ConsumerEventDelegate(object sender, ConsumerEventArgs e);

    //Raise Event Interface
    public interface IConsumerEvent
    { 
        event ConsumerEventDelegate ConsumerEvent;
        void RaiseConsumerEvent(object data, int queueCount);
    }
}
