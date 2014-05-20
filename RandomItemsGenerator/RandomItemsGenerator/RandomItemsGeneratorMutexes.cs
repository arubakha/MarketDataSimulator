﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace RandomItemsGenerator.Business
{
    public class RandomItemsGeneratorMutexes : RandomItemsGeneratorBase, IRandomItemsGenerator
    {
        private Queue<string> data = new Queue<string>();
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

                using (var mutex = new Mutex(false, "ProducerConsumer"))
                {
                    mutex.WaitOne();

                    data.Enqueue(randomItem);
                    if (data.Count > 2000)
                        data.Dequeue();

                    mutex.ReleaseMutex();
                }
            }
        }

        private void ConsumerThreadStarter()
        {
            while (!stopIndicator)
            {
                using (var mutex = new Mutex(false, "ProducerConsumer"))
                {
                    mutex.WaitOne();

                    if (data.Count > 0)
                    {
                        string item = data.Dequeue() + " " + Thread.CurrentThread.Name + " " + data.Count.ToString();
                        RaiseConsumerEvent(item, data.Count);
                    }

                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
