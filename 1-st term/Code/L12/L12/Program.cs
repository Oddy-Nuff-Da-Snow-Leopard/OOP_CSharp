using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Collections;

namespace L12
{
    class Vehicle : IMovable
    {
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

        public void Move()
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

    class Car : Vehicle, IAdd
    {
        private string name;
        private ushort AmountOfFuel;
        private ushort AmountOfOil;

        public void AddFuel(ushort value)
        {
            AmountOfFuel += value;
        }

        public void AddOil(ushort value)
        {
            AmountOfOil += value;
        }

        public void DoSomething(byte a, byte b)
        {
            Console.WriteLine((byte)(a + b));
        }

        public void ShowAmountOfLiquids()
        {
            Console.WriteLine("Amount of fuel: {0} liters", AmountOfFuel);
            Console.WriteLine("Amount of oil: {0} liters", AmountOfOil);
        }

        public override void GetInfo()
        {
            Console.WriteLine("Name: {0}", name);
            base.GetInfo();
        }

        public Car() : base()
        {
            name = "unknown";
        }

        public Car(string name, string company, string year) : base(company, year)
        {
            this.name = name;
        }
    }

    class Reflector
    {
        public void WriteTypeInfoInFile(string typeName)
        {
            using (var streamWriter = new StreamWriter(@"D:\TypeInfo.txt"))
            {
                foreach (var member in Type.GetType(typeName, false, true).GetMembers())
                    streamWriter.WriteLine(member.DeclaringType + " " + member.MemberType + " " + member.Name);
            }
        }

        public void ShowAllPublicMethods(string typeName)
        {
            Console.Write($"Methods of ");
            ColorType(typeName);
            Console.WriteLine(" type:");
            foreach (var method in Type.GetType(typeName, false, true).GetMethods())
            {
                string modificator = "";
                if (method.IsStatic)
                    modificator += "static ";
                if (method.IsVirtual)
                    modificator += "virtual ";
                Console.Write(modificator + method.ReturnType.Name + " " + method.Name + " (");

                var parameters = method.GetParameters();
                for (byte i = 0; i < parameters.Length; i++)
                {
                    Console.Write(parameters[i].ParameterType.Name + " " + parameters[i].Name);
                    if (i + 1 < parameters.Length)
                        Console.Write(", ");
                }
                Console.WriteLine(")");
            }
            Console.WriteLine();
        }

        public void ShowAllFieldsAndProperties(string typeName)
        {
            Console.Write($"Fields of ");
            ColorType(typeName);
            Console.WriteLine(" type:");
            foreach (var field in Type.GetType(typeName, false, true).GetFields(BindingFlags.NonPublic | BindingFlags.Instance))
                Console.WriteLine(field.DeclaringType + " " + field.FieldType + " " + field.Name);

            Console.WriteLine();

            Console.Write($"Properties of ");
            ColorType(typeName);
            Console.WriteLine(" type:");
            foreach (var property in Type.GetType(typeName, false, true).GetProperties())
                Console.WriteLine(property.DeclaringType + " " + property.PropertyType + " " + property.Name);

            Console.WriteLine();
        }

        public void ShowAllInterfaces(string typeName)
        {
            foreach (var Iface in Type.GetType(typeName, false, true).GetInterfaces())
                Console.WriteLine(Iface);
        }

        public void ShowMethodNamesWithSpecifiedParameterType(string typeName, Type parameterType)
        {
            Console.Write($"Method names of ");
            ColorType(typeName);
            Console.WriteLine($" type, that contains parameter with the type {parameterType.FullName.ToUpper()}:");
            foreach (var method in Type.GetType(typeName, false, true).GetMethods())
            {
                foreach (var parameter in method.GetParameters())
                {
                    if (parameter.ParameterType == parameterType)
                    {
                        Console.WriteLine(method.Name);
                        break;
                    }
                }
            }
            Console.WriteLine();
        }

        public void InvokeMethod(string typeName, string methodName)
        {

            var lines = new List<string>();
            string line;
            using (var reader = new StreamReader(@"D:\Parameters.txt"))
            {
                while ((line = reader.ReadLine()) != null)
                    lines.Add(line);
            }

            object[] parameters = new object[lines.Count];
            
            for (byte i = 0; i < parameters.Length; i++)
                parameters[i] = byte.Parse(lines[i]);

            var method = Type.GetType(typeName, false, true).GetMethod(methodName);

            var Car = Activator.CreateInstance(method.DeclaringType);

            Console.Write("Method {0} invoked: ", method.Name);
            method.Invoke(Car, parameters);
        }
        
        private static Random rnd;
        private void ColorType(string typeName)
        {
            Console.ForegroundColor = (ConsoleColor)rnd.Next(1, 14);
            Console.Write(typeName);
            Console.ResetColor();
        }

        static Reflector()
        {
            rnd = new Random();
        }

    }

    class Program
    {

        static void Main(string[] args)
        {

            var Car = new Car("Lamborghini Aventador lp700-4", "Volkswagen Group", "2018");

            var Reflector = new Reflector();
            Reflector.WriteTypeInfoInFile(Car.ToString());
            Reflector.ShowAllPublicMethods("System.UInt32");
            Reflector.ShowAllFieldsAndProperties(Car.ToString());
            Reflector.ShowMethodNamesWithSpecifiedParameterType(Car.ToString(), typeof(System.UInt16));
            Reflector.InvokeMethod(Car.ToString(), "DoSomething");

            Console.ReadKey();
        }
    }
}