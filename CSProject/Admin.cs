using System;

namespace CSProject
{
    class Admin : Staff
    {
        //fields
        private const float overtimeRate = 15.5f;
        private const float adminHourlyRate = 30;

        //Property
        public float Overtime { get; private set; }

        //Constructor
        public Admin (string name): base (name, adminHourlyRate)
        {

        }

        public override void CalculatePay()
        {
            base.CalculatePay();
            if (HoursWorked > 160)
            {
                Overtime = overtimeRate * (HoursWorked - 160);
                TotalPay = BasicPay + Overtime;
            }
            else
            {
                Console.WriteLine("No overtime added");
            }
        }

        public override string ToString()
        {
            return "\nName of Staff: " + NameOfStaff + "\nTotal Pay: " + TotalPay + "\nBasic Pay: " + BasicPay + "\nOvertime: " + Overtime + "\nHours worked: " + HoursWorked;
        }
    }
}