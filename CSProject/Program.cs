using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace CSProject
{
    class PayRollProgram
    {
        private IUserInput _userInput;
        private IFileReader _fileReader;

        public PayRollProgram()
        : this(new UserInput(), new FileReader())
        {
        }

        public PayRollProgram(IUserInput userInput, IFileReader fileReader)
        {
            _fileReader = fileReader;
            _userInput = userInput;
        }

        public void PayRoll()
        {
            //variables
            var year = _userInput.Year();
            var month = _userInput.Month;
            var myStaff = _fileReader.ReadFile();

            for (int i = 0; i < myStaff.Count; i++)
            {
                try
                {
                    WriteLine("Enter hours worked for {0}: ", myStaff[i].NameOfStaff);
                    myStaff[i].HoursWorked = _userInput.GetHoursWorked();
                    myStaff[i].CalculatePay();
                    WriteLine(myStaff[i].ToString());
                }
                catch (Exception e)
                {
                    WriteLine(e.Message);
                    i--;
                }
            }

            PaySlip ps = new PaySlip(month, year);
            ps.GeneratePaySlip(myStaff);
            ps.GenerateSummary(myStaff);

            _userInput.WaitForNextInput();
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            new PayRollProgram().PayRoll();
        }
    }
}
