using System;
namespace L14
{
    abstract class Vehicle
    {
        protected string company;
        protected ushort? year;
        protected string id;

        public ushort? Year
        {
            set
            {
                if (value < DateTime.Now.Year)
                    year = value;
                else year = null;
            }

            get => year;
        }

        public Vehicle() { }

        public Vehicle(string company)
        {
            this.company = company;
        }

        public Vehicle(string company, ushort year) : this(company)
        {
            Year = year;
        }

        public Vehicle(string company, ushort year, string id) : this(company, year)
        {
            this.id = id;
        }

        public void HowOld()
        {
            Console.WriteLine(DateTime.Now.Year - year);
        }

        public virtual void DoSmth()
        {
            Console.WriteLine("Here something happens!");
        }

        public abstract void Move();
        public abstract string Owner { set; get; }


        public override string ToString()
        {
            return $"Company: {company} \nYear: {year} \nID: {id} \n";
        }
    }


    class Car : Vehicle, ISwitch, IAdd
    {
        protected string name;

        public Car(string name, string company, ushort year, string id) : base(company, year, id)
        {
            this.name = name;
        }

        public override string ToString()
        {
            return $"Name: {name} \nCompany: {company} \nYear: {year} \nID: {id} \n";
        }

        public override int GetHashCode()
        {
            return name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() != GetType())
                return false;

            var car = (Car)obj;
            return ((name == car.name) && (company == car.company) && (year == car.year));
        }

        public override void Move()
        {
            Console.WriteLine("Car started moving! Congratulations!");
        }

        public override string Owner { set; get; }

        public bool Lights { set; get; }
        public void SwitchLights()
        {
            if (Lights)
                Lights = false;
            else Lights = true;
            Console.WriteLine("Lights switched!");
        }

        public bool Music { set; get; }
        public void SwitchMusic()
        {
            if (Music)
                Music = false;
            else Music = true;
            Console.WriteLine("Music switched!");
        }

        public void AddFuel()
        {
            Console.WriteLine("Fuel added!");
        }

        public void AddOil()
        {
            Console.WriteLine("Oil added!");
        }
    }


    sealed class Rocket : Vehicle, IAdd, Ismth1, Ismth2
    {
        public override string Owner { set; get; }
        public override void Move()
        {
            Console.WriteLine("Congratulations, you fly into space!");
        }

        public override void DoSmth()
        {
            Console.WriteLine("2 + 2 = 5");
        }

        //implicit; always public; also can be virtual or absract
        public void AddFuel()
        {
            Console.WriteLine("Fuel added!");
        }

        public void AddOil()
        {
            Console.WriteLine("Oil added!");
        }

        
        //explicit; always priate; can't be overridden or blocked with operator new
        void Ismth1.Move()
        {
            Console.WriteLine("This method from the 1-st interface!");
        }

        void Ismth2.Move()
        {
            Console.WriteLine("This method from the 2-nd interface!");
        }

        void Ismth1.StartEngine()
        {
            Console.WriteLine("This method from the 1-st interface!");
        }

        void Ismth2.StartEngine()
        {
            Console.WriteLine("This method from the 2-nd interface!");
        }
    }


    class Aircraft : Vehicle
    {
        public override string Owner { set; get; }
        public override void Move()
        {
            Console.WriteLine("Congratulations, you fly!");
        }
    }
}