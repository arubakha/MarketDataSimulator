using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Collections.Concurrent;

namespace RandomItemsGenerator.Business
{
    public class RandomItemsGeneratorBlockingCollection : RandomItemsGeneratorBase, IRandomItemsGenerator
    {
        private BlockingCollection<IItem> data = new BlockingCollection<IItem>(2000);
        private bool stopIndicator = false;

        public void Start()
        {
            stopIndicator = false;
            new Thread(ConsumerThreadStarter).Start();
            new Thread(ProducerThreadStarter).Start();
        }

        public void Stop()
        {
            stopIndicator = true;
        }

        private void ProducerThreadStarter()
        {
            while (!stopIndicator)
            {
                IItem randomItem = ItemFactory.GetItem();
                Thread.Sleep(100);

                data.Add(randomItem);
            }
        }

        private void ConsumerThreadStarter()
        {
            while (!stopIndicator)
            {
                IItem item = data.Take(); // +" " + data.Count.ToString();
                RaiseConsumerEvent(item, data.Count);
            }
        }
    }
}
