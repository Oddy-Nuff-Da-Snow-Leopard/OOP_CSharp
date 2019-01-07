using System;
using System.Collections.Generic;
namespace Lab5
{
    class Program
    {
        enum Days { Monday = 1, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday };
        public struct S
        {
            int number;
        }

        class PR
        {
            public List<Vehicle> data = new List<Vehicle>();
            void LShow()
            {
                foreach (Vehicle temp in data)
                {
                    temp.ToString();
                }
            }
            void LAdd(Vehicle temp)
            {
                data.Add(temp);
            }
            void LRem(Vehicle temp)
            {
                data.Remove(temp);
            }
        }

        class Control
        {
            PR pr;
            public Control(PR pr)
            {
                this.pr = pr;
            }
            void FindCar()
            {
                Console.WriteLine("Введите год: ");
                int x = Convert.ToInt32(Console.ReadLine());
                foreach (Vehicle temp in pr.data)
                    if (temp.Year == x)
                        temp.ToString();
            }
        }

        static void Add(IAdd A)
        {
            A.AddFuel();
        }

        static void Main(string[] args)
        {
            var Car1 = new Car("Toyota Supra", "Toyota", 2009, "1137-A213-DSJ123");

            //Console.WriteLine(Car1.ToString());
            //Console.WriteLine(Car1);

            //Console.WriteLine(Car1.GetHashCode());
            //Console.WriteLine();

            //var Car2 = new Car("Toyota Supra", "Toyota", 2009, "123-3A-432B");
            //Console.WriteLine(Car1.Equals(Car2));
            //Car1.HowOld();
            //Console.WriteLine();

            //var Car3 = new Car("Nissan GTR R-35", "Nissan", 2017, "1231-AFGS-124");
            //Console.WriteLine(Car3.ToString());

            //Car3.SwitchLights();
            //Console.WriteLine(Car3.Lights);
            //Car3.SwitchMusic();
            //Console.WriteLine(Car3.Music);
            //Console.WriteLine();
            //Car3.AddFuel();
            //Car3.AddOil();
            //Console.WriteLine();


            var Falcon9 = new Rocket();
            Falcon9.DoSmth();
            //Console.WriteLine();

            //Treatment method

            Falcon9.AddFuel(); // via class object

            //Переменные ссылочного интерфейсного типа
            //могут ссылаться на любой объект, реализующий ее интерфейс
            IAdd Ilink = new Rocket();
            Ilink.AddFuel(); // via interface link

            //Присваивание ссылке на интерфейс
            //объектов различных типов, 
            //поддерживающих этот интерфейс
            Add(Falcon9); //Upcasting


            //Явное указание имени интерфейса перед реализуемым элементом
            //Ismth1 Falcon8 = new Rocket();
            //Falcon8.StartEngine();

            //В таком случае соответствующий элемент не входит в интерфейс класса
            //var Falcon7 = new Rocket();
            //Falcon7.StartEngine(); - ERROR

            //Implicit upcasting
            //((Ismth1)(Falcon7)).StartEngine();
            //((Ismth2)(Falcon7)).StartEngine();


            Console.ReadKey();
        }
    }
}