using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RandomItemsGenerator.Business
{
    public interface IItem
    {
        int ID { get; }
        string Description { get; }
        int RandomData { get; }
        bool Enabled { get; set; }
    }

    public class Item : IItem
    {
        static Random random = new Random(DateTime.Now.Millisecond);

        public int ID { get; private set; }
        public string Description { get; private set; }
        public int RandomData { get; private set; }
        public bool Enabled { get; set; }
        
        public Item()
        {
            ID = random.Next(0, 11);

            StringBuilder word = new StringBuilder();
            int wordLength = random.Next(5, 16);
            char character;
            for (int i = 0; i < wordLength; i++)
            {
                character = Convert.ToChar(random.Next(65, 91) + random.Next(0, 2) * 32);
                word.Append(character);
            }
            Description = word.ToString();

            RandomData = random.Next();

            Enabled = Convert.ToBoolean(random.Next(0, 2));
        }

        public override string ToString()
        {
            return string.Format("{0}\t{1} {2}", ID.ToString(), Description, RandomData.ToString());
        }
    }
}
