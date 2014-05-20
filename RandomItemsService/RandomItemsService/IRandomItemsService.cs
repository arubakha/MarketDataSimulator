using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace RandomItemsService
{
    [ServiceContract]
    public interface IRandomItemsService
    {
        [OperationContract(IsOneWay=true)]
        void Start();

        [OperationContract]
        RandomItem GetNextItem();

        [OperationContract(IsOneWay = true)]
        void Stop();
    }



    [DataContract]
    public class RandomItem
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int RandomData { get; set; }

        [DataMember]
        public bool Enabled { get; set; }
    }
}
