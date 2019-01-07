using System;
using System.Runtime.Serialization;

namespace L14
{
    [DataContract]
    class SSD
    {
        [DataMember]
        private string name;
        [DataMember]
        private ushort cost;
        [DataMember]
        private ushort capacity;
        [DataMember]
        private string memoryType;
        [IgnoreDataMember]
        private string controller;

        public SSD(string name, ushort cost, ushort capacity, string memoryType, string controller)
        {
            this.name = name;
            this.cost = cost;
            this.capacity = capacity;
            this.memoryType = memoryType;
            this.controller = controller;
        }

        public void GetInfo()
        {
            Console.WriteLine("Information about SSD:");
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Cost: {cost}$");
            Console.WriteLine($"Capacity: {capacity}GB");
            Console.WriteLine($"Memory type: {memoryType}");
            Console.WriteLine($"Controller: {controller}");
        }
    }

    [DataContract]
    class Computer
    {
        [DataMember]
        private string name;
        [DataMember]
        private ushort cost;
        [DataMember]
        private SSD ssd;

        public Computer(string name, ushort cost, SSD ssd)
        {
            this.name = name;
            this.cost = cost;
            this.ssd = ssd;
        }


        public void GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Cost: {cost}$");
            ssd.GetInfo();
            Console.ResetColor();
        }

        [OnSerializing]
        private void OnSerializing(StreamingContext context)
        {
            Console.WriteLine("Object which should be serialized:");
            GetInfo();
            Console.WriteLine("Serializing...");
        }

        [OnSerialized]
        public void OnSerialized(StreamingContext context)
        {
            Console.WriteLine("Object serialized successfully!\n");
        }

        [OnDeserializing]
        private void OnDeserializing(StreamingContext context)
        {
            Console.WriteLine("Deserializing...");
        }

        [OnDeserialized]
        public void OnDeserialized(StreamingContext context)
        {
            Console.WriteLine("Object deserialized successfully!");
            GetInfo();
        }
    }
}
