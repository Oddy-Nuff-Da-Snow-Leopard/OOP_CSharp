using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace L14
{
    public class Laptop
    {
        [XmlAttribute("name")]
        public string name;

        public Laptop() { }
    
        public Laptop(string name)
        {
            this.name = name;
        }
    }

    //[XmlRoot("Root")]
    public class Programmer
    {
        public string name;
        public string surname;
        [XmlIgnore]
        public byte age;
        //[XmlText]
        public ushort salary;
        public Laptop laptop;

        public byte Age
        {
            set
            {
                if (value >= 18 && value <= 70)
                    age = value;
            }
            get => age;
        }

        public Programmer()
        {
            laptop = new Laptop();
        }

        public Programmer(string name, string surname, byte age, ushort salary)
        {
            this.name = name;
            this.surname = surname;
            Age = age;
            this.salary = salary;
            laptop = new Laptop();
        }

        public Programmer(string name, string surname, byte age, ushort salary, string laptopName) : this(name, surname, age, salary)
        {
            laptop = new Laptop(laptopName);
        }

        public void StartWork()
        {
            Console.WriteLine("Obey sir!");
        }

        public void GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"Surname: {surname}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Salary: {salary}$");
            Console.WriteLine($"Laptop: {laptop.name}");
            Console.ResetColor();
        }

        //[OnSerializing]
        //public void OnSerializing(StreamingContext context)
        //{
        //    Console.WriteLine("Object which should be serialized:");
        //    GetInfo();
        //    Console.WriteLine("Serializing...");
        //}

        //[OnSerialized]
        //public void OnSerialized(StreamingContext context)
        //{
        //    Console.WriteLine("Object serialized successfully!\n");
        //}

        //[OnDeserializing]
        //public void OnDeserializing(StreamingContext context)
        //{
        //    Console.WriteLine("Deserializing...");
        //}

        //[OnDeserialized]
        //public void OnDeserialized(StreamingContext context)
        //{
        //    Console.WriteLine("Object deserialized successfully!");
        //    GetInfo();
        //}
    }
}
