using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L8
{
    interface IAction<T>
    {
        void Add(T value);
        void Remove(T value);
        void Show();
    }

    class CollectionType<T> where T: class, IAction<T>, new()
    {
        private static byte indexNumber;

        public List<T> L;
        public CollectionType()
        {
            L = new List<T>();
            indexNumber++;
        }

        static CollectionType()
        {
            indexNumber = 0;
        }
            

        public void Add(T value)
        {
            L.Add(value);
        }

        public void Remove(T value)
        {
            L.Remove(value);
        }

        public void Show()
        {
            Console.WriteLine($"List {indexNumber}: ");
            foreach (var element in L)
            {
                Console.WriteLine(element);
            }
            Console.WriteLine();
        }

        public static CollectionType<T> operator +(CollectionType<T> C1, CollectionType<T> C2)
        {
            var C3 = new CollectionType<T>();
            C3.L.AddRange(C1.L);
            C3.L.AddRange(C2.L);
            return C3;
        }

    }

    class Student<T> 
    {
        public string Name { private set; get; }
        public string Surname { private set; get; }
        public T ID { private set; get; }

        public Student() { }

        public Student(string name, string surname, T id)
        {
            Name = name;
            Surname = surname;
            ID = id;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var C1 = new CollectionType<string>();
            C1.Add("January");
            C1.Add("February");
            C1.Add("March");
            C1.Add("April");

            var C2 = new CollectionType<string>();
            C2.Add("May");
            C2.Add("June");
            C2.Add("July");

            var C3 = C1 + C2;
            C3.Show();

            C3.Remove("May");
            C3.Show();

            //try
            //{
            //    var str = "fsdf";
            //    foreach (var element in C3.L)
            //    {
            //        if (element == str)
            //            throw new String();
            //    }
            //}
            //catch()
            //{
            //    Console.WriteLine("Element doesn't exist");

            //}

            //finally
            //{
            //    Console.WriteLine("This is finally");
            //}


            var Students = new CollectionType<Student<string>>();
            Students.Add(new Student<string>("Maksim", "Alekseev", "21234545"));
            Students.Add(new Student<string>("James", "Spleen", "564432534"));
            Students.Add(new Student<string>("Antony", "Mars", "23423423"));

            Students.Show();

            Console.ReadKey();
        }
    }
}
