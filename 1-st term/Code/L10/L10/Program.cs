using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.ObjectModel;

namespace L10
{
    class Student
    {
        private string name;
        private string surname;

        public string Name { get => name; }
        public string Surname { get => surname; }

        public Student() { }

        public Student(string name, string surname)
        {
            this.name = name;
            this.surname = surname;
        }

        public string GetInfo()
        {
            return $"Name: {name}, surname: {surname}";
        }
    }

    class Students
    {
        private Student[] Arr;

        public Students(byte length)
        {
            Arr = new Student[length];
        }

        public Student this[byte index]
        {
            get { return Arr[index]; }
            set { Arr[index] = value; }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(" Task 1\n");

            var ArrLst = new ArrayList();
            var Rnd = new Random();
            for (byte i = 0; i < 5; i++)
                ArrLst.Add((byte)Rnd.Next(1, 20));

            ArrLst.Add("one");
            ArrLst.AddRange(new string[] { "two", "three" });

            var S = new Student("Maksim", "Alekseev");
            ArrLst.Add(S);

            Console.WriteLine("ArrayList: ");
            foreach (var element in ArrLst)
                Console.WriteLine($"{element}, {element.GetType()}");

            ArrLst.RemoveAt(0);
            ArrLst.RemoveRange(1, 2);
            ArrLst.Remove("two");
            ArrLst.RemoveAt(ArrLst.Count - 1);

            Console.WriteLine();
            Console.WriteLine("Amount of elements: {0}", ArrLst.Count);
            Console.WriteLine("ArrayList: ");
            foreach (var element in ArrLst)
                Console.WriteLine(element);

            Console.WriteLine();
            Console.WriteLine($"Index of value \"three\" is {ArrLst.IndexOf("three")}");


            Console.ReadKey();
            Console.Clear();



            Console.WriteLine(" Task 2\n");

            var Stack1 = new Stack<char>();
            var str = new char[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g' };
            for (byte i = 0; i < 8; i++)
                Stack1.Push(str[Rnd.Next(0, str.Length - 1)]);

            Console.Write("Stack: ");
            foreach (char element in Stack1)
                Console.Write($"{element} ");
            Console.WriteLine();

            Console.Write("How many elements should be deleted? ");
            var n = Convert.ToByte(Console.ReadLine());
            for (byte i = 0; i < n; i++)
                Stack1.Pop();
            Console.Write("Stack: ");
            foreach (char element in Stack1)
                Console.Write($"{element} ");
            Console.WriteLine("\n");


            var List1 = new List<char>();
            List1.AddRange(Stack1);
            Console.Write("List: ");
            foreach (char element in List1)
                Console.Write($"{element} ");
            Console.WriteLine();

            Console.Write("Enter value to search: ");
            char symbol = Convert.ToChar(Console.Read());
            bool flag = false;
            foreach (char element in List1)
            {
                if (element == symbol)
                {
                    Console.WriteLine("Entered value found, it's index is: {0}", List1.IndexOf(symbol));
                    flag = true;
                    break;
                }
            }
            if (!flag)
                Console.WriteLine("Entered value not found");

            Console.ReadKey();
            Console.Clear();



            Console.WriteLine(" Task 3\n");

            var Students = new Students(5);
            Students[0] = new Student("James", "Spleen");
            Students[1] = new Student("Antony", "Mars");
            Students[2] = new Student("Dudd", "Dwyer");
            Students[3] = new Student("Scott", "Travis");
            Students[4] = new Student("Tony", "Montana");

            var Stack2 = new Stack<Student>();

            for (byte i = 0; i < 5; i++)
                Stack2.Push(Students[i]);

            Console.WriteLine("Stack: ");
            foreach (Student i in Stack2)
                Console.WriteLine(i.GetInfo());
            Console.WriteLine();

            Console.Write("How many elements should be deleted? ");
            Console.ReadLine();
            n = Convert.ToByte((object)Console.ReadLine());
            for (byte i = 0; i < n; i++)
                Stack2.Pop();

            Console.WriteLine("Stack: ");
            foreach (Student i in Stack2)
                Console.WriteLine(i.GetInfo());
            Console.WriteLine("\n");

            var List2 = new List<Student>();
            List2.AddRange(Stack2);
            Console.WriteLine("List: ");
            foreach (Student i in List2)
                Console.WriteLine(i.GetInfo());
            Console.WriteLine();

            Console.Write("Enter value to search: ");
            var value = Console.ReadLine();
            flag = false;
            foreach (Student i in List2)
            {
                if (i.Name == value || i.Surname == value)
                {
                    Console.WriteLine("Entered value found, it's index is: {0}", List2.IndexOf(i));
                    flag = true;
                    break;
                }
            }
            if (!flag)
                Console.WriteLine("Entered value not found");

            Console.ReadKey();
            Console.Clear();


            Console.WriteLine(" Task 4\n");

            ObservableCollection<Student> users = new ObservableCollection<Student>();
            users.Add(Students[0]);
            users.Add(Students[1]);
            users.Add(Students[2]);

            foreach (var student in users)
            {
                Console.WriteLine(student.GetInfo());
            }
            Console.WriteLine();

            users.CollectionChanged += Users_CollectionChanged;

            users.Add(Students[3]);
            foreach (var student in users)
            {
                Console.WriteLine(student.GetInfo());
            }
            Console.WriteLine();

            users.RemoveAt(1);
            foreach (var student in users)
            {
                Console.WriteLine(student.GetInfo());
            }
            Console.WriteLine();

            users[0] = new Student("Tony", "Montana");
            foreach (var student in users)
            {
                Console.WriteLine(student.GetInfo());
            }

            Console.Read();
        }

        private static void Users_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add: 
                    Student newUser = e.NewItems[0] as Student;
                    Console.WriteLine("New object added: {0}", newUser.Name);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    Student oldUser = e.OldItems[0] as Student;
                    Console.WriteLine("Object deleted: {0}", oldUser.Name);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    Student replacedUser = e.OldItems[0] as Student;
                    Student replacingUser = e.NewItems[0] as Student;
                    Console.WriteLine("Object {0} has been replaced by object {1}",
                                        replacedUser.Name, replacingUser.Name);
                    break;
            }
        }
    }

}