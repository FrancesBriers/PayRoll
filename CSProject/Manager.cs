using System;

namespace CSProject
{
    public class Manager : Staff
    {
        //fields
        private const float managerHourlyRate = 50;

        //properties
        public int Allowance { get; private set; }

        //constructor
        public Manager (string name) : base (name, managerHourlyRate)
        {
            
        } 
        //methods
        public override void CalculatePay()
        { 
            base.CalculatePay();
            Allowance = 1000;

            if (HoursWorked > 160)
            {
                TotalPay = BasicPay + Allowance;
            }
            else
            {
                Console.WriteLine("No allowance added");
            }

        }
        public override string ToString()
        {
            return "\nName of Staff: " + NameOfStaff + "\nTotal Pay: " + TotalPay + "\nBasic Pay: " + BasicPay + "\nAllowance: " + Allowance + "\nHours worked: " + HoursWorked;
        }

    }
}