using System;
using System.Collections.Generic;
using System.Linq;

namespace L11
{
    class Program
    {
        static void Main(string[] args)
        {
            var months = new string[]
            {
                "March", "May", "August", "October",
                "April", "February", "June", "December",
                "July", "September", "January", "November"
            };

            Console.Write("Enter length: ");
            byte n = Convert.ToByte(Console.ReadLine());

            var monthsWithACertainNumberOfLetters = from m in months
                                                    where m.Length == n
                                                    select m;

            foreach (var month in monthsWithACertainNumberOfLetters)
                Console.Write($"{month} ");
            Console.WriteLine("\n");


            var summerAndWinterMonths = months.Intersect(new string[]
            {
                "June", "July", "August",
                "December", "January", "February"
            });

            foreach (var month in summerAndWinterMonths)
                Console.Write($"{month} ");
            Console.WriteLine("\n");


            var monthsInAlphabeticalOrder = from m in months
                                            orderby m
                                            select m;

            foreach (var month in monthsInAlphabeticalOrder)
                Console.Write($"{month} ");
            Console.WriteLine("\n");


            var monthsContainingTheLetterU = from m in months
                                             where m.Contains("u") && m.Length >= 4
                                             select m;

            foreach (var month in monthsContainingTheLetterU)
                Console.Write($"{month} ");
            Console.WriteLine();

            Console.ReadKey();
            Console.Clear();



            var listOfVectors = new List<Vector>
            {
                new Vector(4, true),
                new Vector(new int[] { 1, 4, 0, 10, 2 }),
                new Vector(6, false),
                new Vector(5, true),
                new Vector(4, true)
            };

            foreach (var vector in listOfVectors)
                vector.Print();


            var amountOfVectorsThatContainZero = (from v in listOfVectors
                                                  where v.IsWithZeroelement()
                                                  select v).Count();

            Console.WriteLine($"\nAmount of vectors, that contain zero element: {amountOfVectorsThatContainZero}\n");


            listOfVectors.RemoveAt(2);
            listOfVectors.Insert(2, new Vector(4, true));

            foreach (var vector in listOfVectors)
                vector.Print();
            Console.WriteLine();


            var smallestModuleVector = (from v in listOfVectors
                                        orderby v.CountModule()
                                        select v).First();

            smallestModuleVector.Print();
            Console.WriteLine();


            var arrayOfVectorsOfACertainLength = (from v in listOfVectors
                                                  where v.size == 3 || v.size == 5 || v.size == 7
                                                  select v).ToArray();

            foreach (var vector in arrayOfVectorsOfACertainLength)
                vector.Print();
            Console.WriteLine();


            var maximumModuleVector = (from v in listOfVectors
                                       orderby v.CountModule()
                                       select v).Last();

            maximumModuleVector.Print();
            Console.WriteLine();


            var firstVectorWithNegativeValue = (from v in listOfVectors
                                                from e in v.vector
                                                where e < 0
                                                select v).First();

            firstVectorWithNegativeValue.Print();
            Console.WriteLine();


            var orderedListOfVectorsBySize = (from v in listOfVectors
                                                  orderby v.size
                                                  select v).ToList();

            foreach (var vector in orderedListOfVectorsBySize)
                vector.Print();
            Console.WriteLine();

            Console.ReadKey();
            Console.Clear();



            listOfVectors.Add(new Vector(5, true));
            listOfVectors.Add(new Vector(new int[] { 1, 6, 8, 3, 6 }));
            listOfVectors.Add(new Vector(7, true));


            foreach (var vector in listOfVectors)
                vector.Print();
            Console.WriteLine();


            var someQuery = from v in listOfVectors
                            from e in v.vector
                            where v.CountModule() > 200
                            where e == 0
                            orderby v.size
                            select v;

            foreach (var vector in someQuery)
                vector.Print();


            var list1 = new List<int>();
            list1.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

            var list2 = new List<int>();
            list2.AddRange(new int[] { 2, 4, 6, 8, 10 });

            var joinedList = from t1 in list1
                             join t2 in list2 on t1 equals t2
                             orderby t1
                             select t1;

            foreach (var element in joinedList)
                Console.Write(element + " ");

            Console.WriteLine();

            Console.ReadKey();
        }

    }
}
