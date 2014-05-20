using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using RandomItemsGenerator.Business;

namespace WPFThreading.Presentation
{
    class LengthBasedItemTemplateSelector : DataTemplateSelector
    {
        public DataTemplate ShortTemplate { get; set; }
        public DataTemplate LongTemplate { get; set; }

        public override DataTemplate SelectTemplate(object itemParam, System.Windows.DependencyObject container)
        {
            Item item = itemParam as Item;
            if (item != null)
            {
                return item.Description.Length < 10 ? ShortTemplate : LongTemplate;
            }

            return base.SelectTemplate(item, container);
        }
    }
}
