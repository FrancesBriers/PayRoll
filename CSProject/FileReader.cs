using System;
using System.Collections.Generic;
using System.IO;

namespace CSProject
{
    internal interface IFileReader
    {
        List<Staff> ReadFile();
    }

    class FileReader : IFileReader
    {
        public List<Staff> ReadFile()
        {
            List<Staff> myStaff = null;// = new List<Staff>();
            string[] result = new string[2];
            string path = "C:\\staff.txt";
            string[] separator = { ", " };

            if (File.Exists(path))
            {
                //StreamReader sr = new StreamReader(path);
                var stream = File.OpenRead(path);
                //stream.Position = 0;
                myStaff = ReadFromStream(stream);
                //using (sr)
                //{
                //    while (!sr.EndOfStream)
                //    {
                //        var line = sr.ReadLine();
                //        result = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                //        if (result[1] == "Manager") myStaff.Add(new Manager(result[0]));
                //        else if (result[1] == "Admin") myStaff.Add(new Admin(result[0]));
                //        WriteLine(line);
                //    }
                //    sr.Close();
                //}
            }
            else
            {
                Console.WriteLine("File doesnot exist.");
            }
            return myStaff;
        }

        public List<Staff> ReadFromStream(Stream stream)
        {
            List<Staff> myStaff = new List<Staff>();
            string[] separator = { ", " };

            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line != null)
                    {
                        var result = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                        if (result[1] == "Manager") myStaff.Add(new Manager(result[0]));
                        else if (result[1] == "Admin") myStaff.Add(new Admin(result[0]));
                    }

                    Console.WriteLine(line);
                }
                sr.Close();
            }
            return myStaff;
        }

    }
}