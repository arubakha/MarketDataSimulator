using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandomItemsGenerator.Business
{
    public class RandomItemsGeneratorAutoResetEvents : RandomItemsGeneratorBase, IRandomItemsGenerator
    {
        private Queue<string> data = new Queue<string>();
        private object monitorLock = new object();
        private AutoResetEvent producerWaitHandle = new AutoResetEvent(false);
        private AutoResetEvent consumerWaitHandle = new AutoResetEvent(false);
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
            Random random = new Random(DateTime.Now.Millisecond);

            while (!stopIndicator)
            {
                producerWaitHandle.WaitOne();

                string randomItem = "Item " + random.Next();
                Thread.Sleep(2);

                lock (monitorLock)
                {
                    data.Enqueue(randomItem);
                    if (data.Count > 2000)
                        data.Dequeue();
                }

                consumerWaitHandle.Set();
            }
        }

        private void ConsumerThreadStarter()
        {
            while (!stopIndicator)
            {
                producerWaitHandle.Set();
                consumerWaitHandle.WaitOne();

                lock (monitorLock)
                {
                    if (data.Count > 0)
                    {
                        string item = data.Dequeue() + " " + Thread.CurrentThread.Name + " " + data.Count.ToString();
                        RaiseConsumerEvent(item, data.Count);
                    }
                }

            }
        }


    }
}
