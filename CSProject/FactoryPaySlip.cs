using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProject
{
    static class FactoryPaySlip
    {
        private static PaySlip customPaySlip = null;

        public static PaySlip Create(int month, int year)
        {
            if (customPaySlip != null)
                return customPaySlip;
            return new PaySlip(month, year);
        }

        public static void SetCustomerPaySlip(PaySlip paySlip)
        {
            customPaySlip = paySlip;
        }

    }

    public interface IAlternateFactoryPaySlip
    {
        IPaySlip Create(int month, int year);

    }

    public class AlternateFactoryPaySlip : IAlternateFactoryPaySlip
    {
        public IPaySlip Create(int month, int year)
        {
            return new PaySlip(month, year);
        }
    }

}
