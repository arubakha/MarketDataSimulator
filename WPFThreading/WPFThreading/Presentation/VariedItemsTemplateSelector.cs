using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;

namespace WPFThreading.Presentation
{
    class VariedItemsTemplateSelector : DataTemplateSelector
    {
        public DataTemplate StringTemplate { get; set; }
        public DataTemplate CommandTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            DataTemplate custom = item as DataTemplate;

            if (item is string)
            {
                return StringTemplate;
            }
            else if (item is RoutedUICommand)
            {
                return CommandTemplate;
            }

            return base.SelectTemplate(item, container);
        }

    }
}
