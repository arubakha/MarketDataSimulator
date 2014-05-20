using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RandomItemsGenerator.Business;

namespace WPFThreading.Business
{
    public class MainWindowModel
    {
        private readonly IRandomItemsGenerator randomItemsGenerator;

        public event ConsumerEventDelegate ConsumerEvent;
        private void RaiseConsumerEvent(object data, int queueCount)
        {
            if (ConsumerEvent != null)
            {
                ConsumerEvent(this, new ConsumerEventArgs(data, queueCount));
            }
        }

        public MainWindowModel()
        {
            //randomItemsGenerator = new RandomItemsGeneratorAutoResetEvents();
            //randomItemsGenerator = new RandomItemsGeneratorMonitors();
            //randomItemsGenerator = new RandomItemsGeneratorMutexes();
            randomItemsGenerator = new RandomItemsGeneratorBlockingCollection();
            randomItemsGenerator.ConsumerEvent += OnConsumerEvent;
        }

        private void OnConsumerEvent(object sender, ConsumerEventArgs e)
        {
            RaiseConsumerEvent(e.Data, e.QueueCount);
        }

        public void StartDataGeneration()
        {
            randomItemsGenerator.Start();
        }

    }
}
