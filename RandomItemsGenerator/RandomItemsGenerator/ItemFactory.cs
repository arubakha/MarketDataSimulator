using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomItemsGenerator.Business
{
    interface IItemFactory
    {
        IItem GetItem();
    }

    public static class ItemFactory
    {
        public static IItem GetItem()
        {
            return new Item();
        }
    }
}
