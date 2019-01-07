using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace L7
{
    class Car
    { 
        public string name;
        public string year;
        public ulong cost;

        private string Year
        {
            set
            {
                int x;
                if (int.TryParse(value, out x))
                {
                    if (x < DateTime.Now.Year)
                        year = value;
                    else
                        throw new YearException("invalid year entered!");
                }
                else
                    throw new YearException("not a number was entered!");
            }

            get => year;
        }

        public Car()
        {
            year = "unknown";
            name = "unknown";
        }

        public Car(string name) : this()
        {
            this.name = name;
        }

        public Car(string name, string year) : this(name)
        {
            Year = year;
        }

        public void GetInfo()
        {
            Console.WriteLine("Name: {0}, year: {1}", name, year);
        }
    }


    class CarException : Exception
    {
        public CarException(string message) : base(message)
        {
            Data.Add("Time: ", DateTime.Now);

            HelpLink = "http://www.belstu.by";
        }
    }

    class YearException : CarException
    {
        public YearException(string message) : base(message) { }
    }

    class CostException : CarException
    {
        public CostException(string message) : base(message) { }
    }


    static class ExtensionMethod
    {
        public static void GetInfo(this Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message + "\n");
            Console.WriteLine("Source that caused the exception: " + ex.Source + "\n");
            Console.WriteLine("Where created current exception: " + ex.TargetSite + "\n");
            Console.WriteLine("Call stack: " + ex.StackTrace + "\n");

            if (ex.Data != null)
            {
                Console.WriteLine("Exact date of occurrence: ");
                foreach(DictionaryEntry element in ex.Data)
                    Console.WriteLine($"\t{element.Key}{element.Value}");
                Console.WriteLine();
            }
            Console.WriteLine("Helplink: " + ex.HelpLink);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //
            try
            {
                var Car1 = new Car("Lamborghini Diablo", "2019");
            }

            catch (YearException ex)
            {
                ex.GetInfo();
            }

            catch (CostException ex)
            {
                ex.GetInfo();
            }

            finally
            {
                Console.WriteLine("Finally!");
                Console.ReadKey();
                Console.Clear();
            }


            //
            try
            {
                sbyte x, y;
                try
                {
                    Console.Write("Enter x: ");
                    x = Convert.ToSByte(Console.ReadLine());
                    Console.Write("Enter y: ");
                    y = Convert.ToSByte(Console.ReadLine());

                    var result = x / y;
                }

                catch (OverflowException)
                {
                    Console.WriteLine("OverFlowException happened");
                }

                finally
                {
                    Console.WriteLine("Finally in nested try!");
                }
            }

            catch (DivideByZeroException)
            {
                Console.WriteLine("DivideByZeroException happened");
            }

            finally
            {
                Console.WriteLine("Finally!");
                Console.ReadKey();
                Console.Clear();
            }


            //
            try
            {
                char[] arr = new char[4];
                arr[5] = 'a';

                object obj = "some string";
                int number = (int)obj;
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("IndexOutOfRangeException happend");
            }
            catch (InvalidCastException)
            {
                Console.WriteLine("InvalidCastException happend");
            }
            finally
            {
                Console.WriteLine("Finally!");
                Console.ReadKey();
                Console.Clear();
            }


            //
            try
            {
                TestClass.Method1();
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine($"Catch in Main : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally in Main");
                Console.ReadKey();
                Console.Clear();
            }


            bool A = false;
            Debug.Assert(A != false, "Value A can't be false!");
            Console.ReadKey();
        }
    }


    class TestClass
    {
        public static void Method1()
        {
            try
            {
                Method2();
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Catch in Method1 : {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Finally in Method1");
            }
            Console.WriteLine("End of Method1");
        }
        static void Method2()
        {
            try
            {
                int x = 8;
                int y = x / 0;
            }
            finally
            {
                Console.WriteLine("Finally in Method2");
            }
            Console.WriteLine("End of Method2");
        }
    }
}

