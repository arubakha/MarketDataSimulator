using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;

namespace WPFThreading.Presentation
{
    class EnabledBasedStyleSelector : StyleSelector
    {
        public override Style SelectStyle(object item, System.Windows.DependencyObject container)
        {
            return base.SelectStyle(item, container);
        }
    }
}
