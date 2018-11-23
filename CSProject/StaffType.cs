using System;
using System.Collections.Generic;
using System.IO;

namespace CSProject
{
    public class StaffType
    {
        private readonly IFactoryStaff _staffFactory;

        public StaffType(IFactoryStaff staffFactory)
        {
            _staffFactory = staffFactory;
        }
        public List<IStaff> ReadFromStream(Stream stream)
        {
            var myStaff = new List<IStaff>();
            string[] separator = { ", " };

            using (var sr = new StreamReader(stream))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line != null)
                    {
                        var result = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);

                        var staffType = result[1];
                        var name = result[0];
                        
                        myStaff.Add(_staffFactory.Create(staffType, name));
                    }

                    Console.WriteLine(line);
                }
                sr.Close();
            }
            return myStaff;
        }
    }
}