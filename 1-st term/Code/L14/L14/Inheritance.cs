using System;
using System.Runtime.Serialization;

namespace L14
{
    [Serializable]
    class Vehicle
    {
        [NonSerialized]
        protected string company;
        protected string year;

        public string Year
        {
            private set
            {
                if (short.TryParse(value, out short x) && x <= DateTime.Now.Year)
                    year = value;
                else
                    year = "unknown";
            }
            get { return year; }
        }

        public virtual void GetInfo()
        {
            Console.WriteLine("Company: {0}", company);
            Console.WriteLine("Year: {0}", year);
        }

        public virtual void Move()
        {
            Console.WriteLine("Vehicle moving!");
        }

        public Vehicle()
        {
            company = "unknown";
            year = "unknown";
        }

        public Vehicle(string smth) : this()
        {
            if (short.TryParse(smth, out short x) && x <= DateTime.Now.Year)
                year = smth;
            else
                company = smth;
        }

        public Vehicle(string company, string year)
        {
            this.company = company;
            Year = year;
        }
    }

    [Serializable]
    class Car : Vehicle
    {
        private string name;

        public override void GetInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Name: {0}", name);
            base.GetInfo();
            Console.ResetColor();
        }

        public Car() : base()
        {
            name = "unknown";
        }

        public Car(string name, string company, string year) : base(company, year)
        {
            this.name = name;
        }

        public override void Move()
        {
            base.Move();
            Console.WriteLine("WROOOM-WROOOM!");
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
            Console.WriteLine();
        }
    }
}
