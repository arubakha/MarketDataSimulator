using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using RandomItemsGenerator.Business;

namespace RandomItemsService
{
    public class RandomItemsService : IRandomItemsService
    {
        private readonly IRandomItemsGenerator itemsGenerator;
        private Queue<IItem> items = new Queue<IItem>(2000);

        public RandomItemsService()
        {
            itemsGenerator = new RandomItemsGeneratorBlockingCollection();
        }

        private void OnConsumerEvent(object sender, ConsumerEventArgs e)
        {
            items.Enqueue((IItem)e.Data);
        }

        public void Start()
        {
            itemsGenerator.ConsumerEvent += OnConsumerEvent;
            itemsGenerator.Start();
        }

        public RandomItem GetNextItem()
        {
            if (items.Count == 0)
            {
                return null; // new RandomItem();
            }
            else
            {
                IItem item = items.Dequeue();
                RandomItem contractItem = new RandomItem() 
                                            { 
                                                ID = item.ID, 
                                                RandomData = item.RandomData, 
                                                Description = item.Description, 
                                                Enabled = item.Enabled 
                                            };
                return contractItem;
            }
        }

        public void Stop()
        {
            itemsGenerator.Stop();
            itemsGenerator.ConsumerEvent -= OnConsumerEvent;
            items.Clear();
        }
    }
}
