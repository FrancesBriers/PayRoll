using System;

public interface IUserInput
{
    int Month { get; }

    int Year();

    int GetHoursWorked();

    void WaitForNextInput();
}


public class UserInput : IUserInput
{
    public int Month
    {
        get
        {
            int month = 0;
            while (month == 0)
            {
                Console.Write("\nPlease enter the month: ");

                try
                {
                    month = Convert.ToInt32(Console.ReadLine());

                    if (month < 1 || month > 12)
                    {
                        Console.WriteLine("Month must be between 1 or 12");
                        month = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message + "Please try again. ");
                }
            }

            return month;
        }
    }

    public int Year()
    {
        int year = 0;

        while (year == 0)
        {
            Console.Write("\nPlease enter the year: ");
            try
            {
                year = Convert.ToInt32(Console.ReadLine());
            }
            catch (FormatException)
            {
                Console.WriteLine("Please enter a valid year");
            }
        }

        return year;
    }

    public int GetHoursWorked()
    {
        return Convert.ToInt32(Console.ReadLine());
    }

    public void WaitForNextInput()
    {
        Console.Read();
    }
}