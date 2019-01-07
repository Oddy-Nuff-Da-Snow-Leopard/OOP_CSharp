using System;
using System.IO;

namespace L13
{
    class MyDriveInfo
    {
        DriveInfo drive;
        private readonly bool isReady;
        public MyDriveInfo(string name)
        {
            drive = new DriveInfo(name);
            if (drive.IsReady)
                isReady = true;
        }

        public void ShowName()
        {
            Console.WriteLine("Name: {0}", drive.Name);
        }

        public void ShowType()
        {
            Console.WriteLine("Type: {0}", drive.DriveType);
        }

        public void ShowRootDirectory()
        {
            Console.WriteLine("Root directory: {0}", drive.RootDirectory);
        }

        public void ShowFormat()
        {
            if (isReady)
                Console.WriteLine("Format: {0}", drive.DriveFormat);
        }

        public void ShowSpaceInfo()
        {
            if (isReady)
            {
                Console.WriteLine("Total size: {0} byte", drive.TotalSize);
                Console.WriteLine("Total free space: {0} byte", drive.TotalFreeSpace);
                Console.WriteLine("Available free space: {0} byte", drive.AvailableFreeSpace);
            }
        }

        public void ShowLabel()
        {
            if (isReady)
            {
                if (drive.VolumeLabel != "")
                    Console.WriteLine("Label: {0}", drive.VolumeLabel);
                else
                    Console.WriteLine("Label: No label");
            }
        }

        public void ShowFullInformation()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Full information of {drive.Name} drive");
            Console.ResetColor();

            ShowType();
            ShowRootDirectory();
            if (isReady)
            {
                ShowFormat();
                ShowSpaceInfo();
                ShowLabel();
                Log.RecordInfo(drive.Name, "Full disk information was obtained!");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"That's all information, because drive {drive.Name} doesn't ready!");
                Console.ResetColor();
                Log.RecordInfo(drive.Name, "Partial disk information was obtained!");
            }
            Console.WriteLine();

        }
    }
}
