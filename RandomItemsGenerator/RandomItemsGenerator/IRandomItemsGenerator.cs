using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomItemsGenerator.Business
{
    public interface IRandomItemsGenerator
    {
        void Start();
        event ConsumerEventDelegate ConsumerEvent;
        void Stop();
    }
}
