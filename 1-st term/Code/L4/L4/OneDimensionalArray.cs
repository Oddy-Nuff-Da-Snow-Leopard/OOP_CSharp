using System;
using System.Linq;

namespace MySpace
{
    static class HelpFunctions
    {
        public static void CheckForNegative(ODArray a)
        {
            if (a)
                Console.WriteLine($"Array {a.SerialNumber} doesn't contain negative element(s)");
            else
                Console.WriteLine($"Array {a.SerialNumber} contains negative element(s)");
        }

        public static void Compare(ODArray a1, ODArray a2)
        {
            if (a1 > a2)
                Console.WriteLine($"Array {a1.SerialNumber} is longer than array {a2.SerialNumber}");
            else
                Console.WriteLine($"Array {a1.SerialNumber} is less than array {a2.SerialNumber}");
        }

        public static void FindOutMinAndMax(ODArray a)
        {
            var min = a.Array.Min();
            var max = a.Array.Max();
            Console.WriteLine($"Array {a.SerialNumber}:\nminimal element = {min}, maximum element = {max}");
        }
    }

    public class Owner
    {
        private readonly string id;
        private readonly string name;
        private readonly string org;

        public Owner(string id, string name, string org)
        {
            this.id = id;
            this.name = name;
            this.org = org;
        }

        public void ShowInfo()
        {
            Console.WriteLine($"Creator ID: {id}");
            Console.WriteLine($"Creator name: {name}");
            Console.WriteLine($"Creator organization: {org}");
        }
    }

    public class ODArray
    {
        #region Fields
        private static byte amount = 1;
        private static Random rnd = new Random();

        private ushort length;

        public Owner owner;
        public Date date;
        #endregion

        #region Properties 
        public ushort Length
        {
            set // private!
            {
                if (value >= 0)
                    length = value;

                else Console.WriteLine("Error, entered a negative number!");
            }
            get { return length; }
        }

        public byte SerialNumber { get; private set; }
        public short[] Array { get; private set; }
        #endregion

        #region Constructor
        public ODArray(ushort length)
        {
            date = new Date(this);
            Length = length;
            Array = new short[Length];
            for (var i = 0; i < length; i++)
                Array[i] = (short)rnd.Next(-100, 100);

            SerialNumber = amount++;
        }
        #endregion
        
        #region Operators overload
        public static ODArray operator *(ODArray a1, ODArray a2)
        {
            return new ODArray((ushort)(a1.length + a2.length));
        }

        public static bool operator true(ODArray a)
        {
            return a.CheckForNegativeElement();
        }

        public static bool operator false(ODArray a)
        {
            return a.CheckForNegativeElement();
        }

        public static bool operator >(ODArray a1, ODArray a2)
        {
            return a1.length > a2.length;
        }

        public static bool operator <(ODArray a1, ODArray a2)
        {
            return a1.length < a2.length;
        } 
        #endregion

        public class Date
        {
            private readonly DateTime dateOfCreation;
            private ODArray parent;
            public Date(ODArray parent)
            {
                dateOfCreation = DateTime.Now;
                this.parent = parent;
            }

            public void ShowDateOfCreation()
            {
                Console.WriteLine($"Time when array {parent.SerialNumber} was created: {dateOfCreation} ");
            }
        }

        #region Help functions
        private bool CheckForNegativeElement()
        {
            bool flag = true;
            foreach (short i in Array)
            {
                if (i < 0)
                {
                    flag = false;
                    break;
                }
                else continue;
            }
            return flag;
        }

        public void PrintArray()
        {
            if (Length > 0)
            {
                Console.WriteLine($"Array {SerialNumber}: ");
                foreach (short i in Array)
                    Console.Write($"{i} ");
                Console.WriteLine("\n");
            }
            else if (Length == 0)
                Console.WriteLine($"Array {SerialNumber} is empty\n");
        }
    #endregion
    }
}