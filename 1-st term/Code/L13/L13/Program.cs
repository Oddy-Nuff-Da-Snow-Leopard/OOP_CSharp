using System;

namespace L13
{
    class Program
    {
        static void Main(string[] args)
        {
            //var drives = DriveInfo.GetDrives();

            var drive = @"C:\";
            var C = new MyDriveInfo(drive);
            C.ShowFullInformation();

            drive = @"E:\";
            var E = new MyDriveInfo(@"E:\");
            E.ShowFullInformation();

            Console.ReadKey();
            Console.Clear();


            var path = @"D:\2 КУРС\Дизайн и юзабилити интерфейсов пользователя\Отчёты";
            var dir1 = new MyDirectoryInfo(path);
            dir1.ShowFullInformation();

            var dir2 = new MyDirectoryInfo("S:\\");
            dir2.ShowFullInformation();

            Console.ReadKey();
            Console.Clear();


            path = @"D:\L14\Students.xml";
            var file1 = new MyFileInfo(path);
            file1.ShowFullInformation();

            var file2 = new MyFileInfo("D:\\HelloWorld.txt");
            file2.ShowFullInformation();

            Console.ReadKey();
            Console.Clear();


            path = @"D:\C#";
            var manager = new FileManager(path);

            manager.PerformTask();

            Console.ReadKey();
            Console.Clear();


            Log.ReadFile();

            Console.WriteLine();

            Log.SearchInfo("disk");

            Log.ShowAmountOfRecords();

            Console.ReadKey();
        }
    }
}