using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace CSProject
{
    public interface IPaySlip
    {
        void GeneratePaySlip(List<Staff> myStaff);
        void GenerateSummary(List<Staff> myStaff);
        string ToString();
    }

    public class PaySlip : IPaySlip
    {
        //fields
        private int month;
        private int year;

        //Enum
        enum MonthsOfYear
        {
            Jan = 1, Feb = 2, Mar = 3, April = 4, May = 5, June = 6, July = 7, Aug = 8 , Sept = 9, Oct = 10, Nov = 11, Dec = 12
        }

        public PaySlip (int payMonth, int payYear)
        {
            month = payMonth;
            year = payYear;
        }

        public void GeneratePaySlip(List<Staff> myStaff)
        {
            string path;

            foreach (Staff f in myStaff)
            {
                path = "C:\\" + f.NameOfStaff + ".txt";
                using (StreamWriter sw = new StreamWriter(path)) 
                {
                    sw.WriteLine("PAYSLIP FOR {0} {1}", (MonthsOfYear)month, year);
                    sw.WriteLine("=============================================");
                    sw.WriteLine("Name of Staff: {0}", f.NameOfStaff);
                    sw.WriteLine("Hours Worked: {0}", f.HoursWorked);
                    sw.WriteLine(" ");
                    sw.WriteLine("Basic Pay: {0:C}", f.BasicPay);

                    if (f.GetType() == typeof(Manager))
                        sw.WriteLine("Allowance: {0:C}", ((Manager)f).Allowance);
                    else if (f.GetType() == typeof(Admin))
                        sw.WriteLine("Overtime: {0:C}", ((Admin)f).Overtime);
                    else sw.WriteLine("");

                    sw.WriteLine("=============================================");
                    sw.WriteLine("Total Pay: {0:C}", f.TotalPay);
                    sw.WriteLine("=============================================");

                    sw.Close();
                    return; 
                }
                
            }

        }

        public void GenerateSummary(List<Staff> myStaff)
        {
            var path = "C:\\Summary.txt";
            using (StreamWriter sw = new StreamWriter(path, true)) 
            {
                sw.WriteLine("Staff with less than 10 working hours");
                sw.WriteLine("");

                foreach (var s in from s in myStaff
                                  where s.HoursWorked < 10
                                  orderby s.NameOfStaff ascending
                                  select new { s.NameOfStaff, s.HoursWorked })
                Console.WriteLine("Name of Staff: {0}, Hours Worked: {1}", s.NameOfStaff, s.HoursWorked);
                sw.Close();
            }
        }

        public override string ToString()
        {
            return "month = " + month + "year = " + year;
        }

    }
}