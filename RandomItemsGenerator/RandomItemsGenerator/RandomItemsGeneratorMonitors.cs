using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandomItemsGenerator.Business
{
    public class RandomItemsGeneratorMonitors : RandomItemsGeneratorBase, IRandomItemsGenerator
    {
        private Queue<string> data = new Queue<string>();
        private object monitorLock = new object();
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
                string randomItem = "Item " + random.Next();
                Thread.Sleep(1);

                lock (monitorLock)
                {
                    data.Enqueue(randomItem);
                    if (data.Count > 2000)
                        data.Dequeue();

                    Monitor.Pulse(monitorLock);
                }
            }
        }

        private void ConsumerThreadStarter()
        {
            while (!stopIndicator)
            {
                lock (monitorLock)
                {
                    while (data.Count == 0)
                        Monitor.Wait(monitorLock);

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
