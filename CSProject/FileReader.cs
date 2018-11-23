using System;
using System.Collections.Generic;
using System.IO;

namespace CSProject
{
    public interface IFileReader
    {
        List<Staff> ReadFile();
    }

    public class FileReader : IFileReader
    {
        private readonly StaffType StaffType = new StaffType(new FactoryStaff());

        public StaffType StaffType1
        {
            get { return StaffType; }
        }

        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = null;// = new List<Staff>();
            string[] result = new string[2];
            string path = "C:\\staff.txt";
            string[] separator = { ", " };

            if (File.Exists(path))
            {
                var stream = File.OpenRead(path);
                myStaff = StaffType1.ReadFromStream(stream);
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }
            return myStaff;
        }
    }
}