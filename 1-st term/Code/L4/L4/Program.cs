using System;
using MySpace;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var objA = new ODArray(8);
            objA.PrintArray();

            var objB = new ODArray(13);
            objB.PrintArray();

            var objC = objA * objB;
            objC.PrintArray();

            HelpFunctions.CheckForNegative(objA);
            objB.Abs();
            HelpFunctions.CheckForNegative(objB);
            Console.WriteLine();

            HelpFunctions.Compare(objB, objA);
            HelpFunctions.Compare(objB, objC);
            
            HelpFunctions.FindOutMinAndMax(objC);

            Console.ReadKey();
            Console.Clear();


            var objE = new ODArray(10);
            objE.PrintArray();
            objE.date.ShowDateOfCreation();
            objE.owner = new Owner("1337", "Maksim", "Itransition");
            objE.owner.ShowInfo();
            Console.WriteLine();

            var objF = new ODArray(4);
            objF.PrintArray();
            objF.date.ShowDateOfCreation();
            objF.owner = new Owner("228", "Ivan", "EPAM");
            objF.owner.ShowInfo();
            Console.ReadKey();
            Console.Clear();

            Console.ReadKey();
            Console.WriteLine("Extension methods");
            Console.Write("Enter some string: ");
            string str = Console.ReadLine();
            Console.Write("Now enter symbol: ");
            char symbol = Convert.ToChar(Console.Read());
            ushort amount = str.CountDuplicate(symbol);
            Console.WriteLine($"Number of repetitions = {amount}");
            string s = str.RemoveDuplicate(symbol);
            Console.WriteLine($"String without symbol {symbol}: {s}");
            Console.WriteLine("Native string is: " + str);

            Console.ReadKey();
            var avgSum = objF.FindAvgSum();
            Console.WriteLine($"\nAverage sum of elements: {avgSum}");
            Console.ReadKey();
        }
    }
}