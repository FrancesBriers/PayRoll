using System;

namespace CSProject
{
    public class PayRollProgram
    {
        private IUserInput _userInput;
        private IFileReader _fileReader;
        private readonly IAlternateFactoryPaySlip _paySlipfactory;
        private readonly IFactoryStaff _staffFactory;

        public PayRollProgram()
            : this(new UserInput(), new FileReader(), new AlternateFactoryPaySlip(), new FactoryStaff())
        {
        }

        public PayRollProgram(IUserInput userInput, IFileReader fileReader, IAlternateFactoryPaySlip paySlipFactory, IFactoryStaff staffFactory)
        {
            _fileReader = fileReader;
            _paySlipfactory = paySlipFactory;
            _userInput = userInput;
            _staffFactory = staffFactory;
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
                    Console.WriteLine("Enter hours worked for {0}: ", myStaff[i].NameOfStaff);
                    myStaff[i].HoursWorked = _userInput.GetHoursWorked();
                    myStaff[i].CalculatePay();
                    Console.WriteLine(myStaff[i].ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    i--;
                }
            }

            var paySlip = _paySlipfactory.Create(month, year); // new PaySlip(month, year);
            paySlip.GeneratePaySlip(myStaff);
            paySlip.GenerateSummary(myStaff);

            _userInput.WaitForNextInput();
        }
    }
}