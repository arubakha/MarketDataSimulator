using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomItemsGenerator.Business;

namespace RandomItemsGenerator
{
    public class RandomItemsGeneratorBase : IConsumerEvent
    {
        public event ConsumerEventDelegate ConsumerEvent;

        public void RaiseConsumerEvent(object data, int queueCount)
        {
            if (ConsumerEvent != null)
            {
                ConsumerEvent(this, new ConsumerEventArgs(data, queueCount));
            }
        }
    }
}
