using System;

namespace CSProject
{
    public interface IStaff
    {
        float TotalPay { get; }
        float BasicPay { get; }
        string NameOfStaff { get; }
        int HoursWorked { get; set; }
        void CalculatePay();
    }

    public class Staff : IStaff
    {
        //fields
        private float hourlyRate;
        private int hWorked;

        //properties
        public float TotalPay { get; protected set; }
        public float BasicPay { get; private set;  }
        public string NameOfStaff { get; private set; }
        public int HoursWorked
        {
            get
            {
                return hWorked;
            }

            set
            {
                if (value <= 0)
                    hWorked = 0;
                else
                    hWorked = value;
            }
        }
        //method
        public virtual void CalculatePay()
        {
            Console.WriteLine("Calculating pay........");
            BasicPay = hWorked * hourlyRate;
            TotalPay = BasicPay;
        }

        public override string ToString()
        {
            return "\nName of Staff: " + NameOfStaff + "\nTotal Pay: " + TotalPay + "\nHourly Rate: "
                   + hourlyRate + "\nHours worked: " + HoursWorked;
        }
        //constructor
        public Staff (string name, float rate)
        {
            NameOfStaff = name;
            hourlyRate = rate;

        }

        /// <inheritdoc />
        public Staff()
        {
        }
    }
}