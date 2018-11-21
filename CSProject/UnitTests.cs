using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;

namespace CSProject
{
    [TestFixture]
    public class UnitTests
    {
        [Test]
        public void GivenManagerThenManagersAreCreated()
        {
            var exampleData = "Joe, Manager";

            var fr = new FileReader();

            var staff = fr.ReadFromStream(GenerateStreamFromString(exampleData));

            Assert.That(staff.Count, Is.EqualTo(1));
            Assert.That(staff[0], Is.InstanceOf<Manager>());
            Assert.That(staff[0].NameOfStaff, Is.EqualTo("Joe"));


        }

        public static Stream GenerateStreamFromString(string s)
        {
            var stream = new MemoryStream();
            var writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        

    }

    [TestFixture]
    public class ManagerTests
    {
        [Test]
        public void GivenAManagerWhoWorked10HoursThenCalculatePayReturnsExpected()
        {
            var manager = new Manager("Joe");

            manager.HoursWorked = 10;
            manager.CalculatePay();

            Assert.That(manager.TotalPay, Is.EqualTo(500.0));
        }

        [Test]
        public void GivenAManagerWhoWorked200HoursThenCalculatePayReturnsExpected()
        {
            var manager = new Manager("Debbie");

            manager.HoursWorked = 200;
            manager.CalculatePay();

            Assert.That(manager.TotalPay, Is.EqualTo(11000.0));
        }
    }

    [TestFixture]
    public class AdminTest
    {
        [Test]
        public void GivenAAdminWhoWorked50HoursThenCalculatePayReturnsExpected()
        {
            var admin = new Admin("Dawn");

            admin.HoursWorked = 30;
            admin.CalculatePay();

            Assert.That(admin.TotalPay, Is.EqualTo(900.0));
        }

        [Test]
        public void GivenAAdminWhoWorked180HoursThenCalculatePayReturnsExpected()
        {
            var admin = new Admin("Paul");

            admin.HoursWorked = 180;
            admin.CalculatePay();

            Assert.That(admin.TotalPay, Is.EqualTo(5710.0));
        }
    }

    public class FakeUnitTests
    {
        [Test]
        public void GivenMonthAndYear_ReturnPaySlip()
        {
            var fakeUserInput = new FakeUserInput();

            var rate = (float)1.2;
            var staff = new Staff("Joe", rate);
            List<Staff> myStaff = new List<Staff> {staff};
            var fakeFileReader = new FakeFileReader(myStaff);

            PayRollProgram input = new PayRollProgram(fakeUserInput, fakeFileReader);

            Assert.That(staff.TotalPay, Is.EqualTo(0));

            input.PayRoll();

            Assert.That(staff.HoursWorked, Is.EqualTo(fakeUserInput.GetHoursWorked()));

            Assert.That(staff.TotalPay, Is.EqualTo(fakeUserInput.GetHoursWorked() * rate));


           // var payslip = new PaySlip(fakeUserInput.Month, fakeUserInput.Year());
            //Assert.That(payslip.ToString(), Is.EqualTo(2));
           // Assert.That(payslip.GenerateSummary(new List<Staff>()), Is.EqualTo("text");
        }
    }

    class FakeUserInput : IUserInput
    {
        public int Month => 2;

        public int Year()
        {
            return 2018;
        }

        public int GetHoursWorked()
        {
            return 40;
        }

        public void WaitForNextInput()
        {
            
        }
    }

    class FakeFileReader : IFileReader
    {
        private List<Staff> _staff;

        public FakeFileReader(List<Staff> staff)
        {
            _staff = staff;
        }

        public List<Staff> ReadFile()
        {
            return _staff;
        }
    }



}
