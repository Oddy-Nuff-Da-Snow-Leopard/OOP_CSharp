using System;
using System.IO;

namespace L13
{
    class MyFileInfo
    {
        FileInfo file;
        private readonly string path;

        public MyFileInfo(string path)
        {
            file = new FileInfo(path);
            this.path = path;
        }

        public void ShowName()
        {
            if (File.Exists(path))
                Console.WriteLine("Name: {0}", file.Name);
        }

        public void ShowFullName()
        {
            if (File.Exists(path))
                Console.WriteLine("Full name: {0}", file.FullName);
        }

        public void ShowExtension()
        {
            if (File.Exists(path))
                Console.WriteLine("Extension: {0}", file.Extension);
        }

        public void ShowSize()
        {
            if (File.Exists(path))
                Console.WriteLine("Size: {0} byte", file.Length);
        }

        public void ShowTimeInfo()
        {
            if (File.Exists(path))
            {
                Console.WriteLine("Creation time: {0}", file.CreationTime);
                Console.WriteLine("Last access time: {0}", File.GetLastAccessTime(path));
                Console.WriteLine("Last write time: {0}", file.LastWriteTime);
            }
        }

        public void ShowFullInformation()
        {
            if (File.Exists(path))
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"Full information of {file.Name} file");
                Console.ResetColor();

                ShowFullName();
                ShowExtension();
                ShowSize();
                ShowTimeInfo();
                Log.RecordInfo(file.FullName, "Full file information was obtained!");
            }

            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"File {file.Name} doesn't exist!");
                Console.ResetColor();
                Log.RecordInfo(file.FullName, "File information wasn't obtained!");
            }
            Console.WriteLine();

        }
    }
}
