using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace CSProject
{
    public interface IFactoryStaff
    {
        Staff Create(string staffType, string name);
    }

    public class FactoryStaff : IFactoryStaff
    {
        public Staff Create(string staffType, string name)
        {
            if (staffType == "Manager")
                return new Manager(name);
            if (staffType == "Admin")
                return new Admin(name);

            throw new ApplicationException("Unknown staff type");
        }
    }
}
