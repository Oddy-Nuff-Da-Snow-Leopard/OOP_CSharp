using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L9
{
    class Program
    {
        delegate void Message();

        private static void GoodMorning()
        {
            Console.WriteLine("Good Morning!");
        }
        private static void GoodEvening()
        {
            Console.WriteLine("Good Evening!");
        }


        delegate int Operation(int x, int y);

        private static int Add(int x, int y) { return x + y; }
        private static int Multiply(int x, int y) { return x * y; }

        class MathOperation
        {
            public int Subtract(int x, int y) { return x - y; }
        }


        private static void Hello()
        {
            Console.WriteLine("Hello!");
        }

        private static void HowAreYou()
        {
            Console.WriteLine("How are you?");
        }


        class Account
        {
            public delegate void AccountStateHandler(string message);
            AccountStateHandler _del;

            public void RegisterHandler(AccountStateHandler del)
            {
                _del = del;
            }

            public void Add(AccountStateHandler del)
            {
                _del += del;
            }

            public void Remove(AccountStateHandler del)
            {
                _del -= del;
            }

            public Account(int sum)
            {
                CurrentSum = sum;
            }

            public int CurrentSum { get; private set; }

            public void Put(int sum)
            {
                CurrentSum += sum;
                _del.Invoke($"{sum}$ were added to the account!");
            }

            public void Withdraw(int sum)
            {
                if (sum <= CurrentSum)
                {
                    CurrentSum -= sum;
                    Add(ShowGreenMessage);
                    _del.Invoke($"{sum}$ was withdrawn from the account!");
                    Remove(ShowGreenMessage);
                }
                else
                {
                    Add(ShowRedMessage);
                    _del.Invoke("Not enough money on the account!");
                    Remove(ShowRedMessage);
                }
                Add(ShowMessage);
            }
        }


        private static void ShowMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static void ShowGreenMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private static void ShowRedMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }



        static void Main(string[] args)
        {
            Message mes;
            if (DateTime.Now.Hour < 12)
                mes = GoodMorning;
            else
                mes = GoodEvening;
            mes();
            Console.WriteLine();


            Operation del = Add;
            int result = del(4, 5);
            Console.WriteLine(result);

            del = Multiply;
            result = del(4, 5);
            Console.WriteLine(result);

            MathOperation mathOp = new MathOperation();
            del = mathOp.Subtract;
            result = del(4, 5);
            Console.WriteLine(result);

            del = new Operation(Multiply);
            
            result = del(100, 5);
            Console.WriteLine(result);
            Console.WriteLine();


            Message mes1;
            mes1 = Hello;
            mes1 += HowAreYou;
            mes1 += Hello;
            mes1();
            Console.WriteLine();
            mes1 -= Hello;
            mes1();
            Console.WriteLine();

            Message mes2 = mes1;
            Message mes3 = new Message(mes1 + mes2);
            mes3();

            mes3.Invoke();

            Console.ReadKey();
            Console.Clear();


            var account = new Account(200);
            account.RegisterHandler(new Account.AccountStateHandler(ShowMessage));
            account.Withdraw(100);
            account.Withdraw(150);
            account.Put(300);
            account.Withdraw(150);
            account.Withdraw(150);



            Console.ReadKey();
        }
    }

}
