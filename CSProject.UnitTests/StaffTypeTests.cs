using System;
using System.Collections.Generic;
using System.IO;
using Moq;
using NUnit.Framework;

namespace CSProject.UnitTests
{
    [TestFixture]
    public class StaffTypeTests
    {



        [TestCase("Manager","Joe")]
        [TestCase("Admin","Sue")]
        public void GivenManagerThenManagersAreCreated(string typeName, string name)
        {
            var exampleData = $"{name}, {typeName}";

            var expectedStaff = new Mock<IStaff>();

            var fakeFactoryStaff = new Mock<IFactoryStaff>();
            fakeFactoryStaff
                .Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedStaff.Object);

            var staffType = new StaffType(fakeFactoryStaff.Object);

            var staff = staffType.ReadFromStream(GenerateStreamFromString(exampleData));

            fakeFactoryStaff
                .Verify(f => f.Create(typeName, name));

            Assert.That(staff.Count, Is.EqualTo(1));
            Assert.That(staff, Has.Member(expectedStaff.Object));
        }

        [Test]
        public void GivenAPayRollFileFormatThenAListOFStaffIsCreated()
        {
            var name1 = "Joe";
            var typeName1 = "Manager";
            var name2 = "Sue";
            var typeName2 = "Admin";

            var exampleData = $"{name1}, {typeName1}" + Environment.NewLine +
                              $"{name2}, {typeName2}";

            var expectedStaff = new Mock<IStaff>();

            var fakeFactoryStaff = new Mock<IFactoryStaff>();
            fakeFactoryStaff
                .Setup(f => f.Create(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(expectedStaff.Object);

            var staffType = new StaffType(fakeFactoryStaff.Object);

            var staff = staffType.ReadFromStream(GenerateStreamFromString(exampleData));

            fakeFactoryStaff
                .Verify(f => f.Create(typeName1, name1));
            fakeFactoryStaff
                .Verify(f => f.Create(typeName2, name2));

            Assert.That(staff.Count, Is.EqualTo(2));
            Assert.That(staff, Has.Member(expectedStaff.Object));
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
            var myStaff = new List<IStaff> {staff};
            var fakeFileReader = new FakeFileReader(myStaff);

            PayRollProgram input = new PayRollProgram(fakeUserInput, fakeFileReader, new AlternateFactoryPaySlip(), new FactoryStaff());

            Assert.That(staff.TotalPay, Is.EqualTo(0));

            input.PayRoll();

            Assert.That(staff.HoursWorked, Is.EqualTo(fakeUserInput.GetHoursWorked()));

            Assert.That(staff.TotalPay, Is.EqualTo(fakeUserInput.GetHoursWorked() * rate));

        }

        [Test]
        public void WithFactory_GivenMonthAndYear_ReturnPaySlip()
        {
            var fakeUserInput = new FakeUserInput();

            var rate = (float)1.2;
            var staff = new Staff("Joe", rate);
            var myStaff = new List<IStaff> { staff };
            var fakeFileReader = new FakeFileReader(myStaff);

            var paySlip = new PaySlip(1, 2000);

            var fakeFactory = new FakeFactory(paySlip);
            //FactoryPaySlip.SetCustomerPaySlip(paySlip);

            PayRollProgram input = new PayRollProgram(fakeUserInput, fakeFileReader, new AlternateFactoryPaySlip(), new FactoryStaff());

            Assert.That(staff.TotalPay, Is.EqualTo(0));

            input.PayRoll();

            Assert.That(staff.HoursWorked, Is.EqualTo(fakeUserInput.GetHoursWorked()));

            Assert.That(staff.TotalPay, Is.EqualTo(fakeUserInput.GetHoursWorked() * rate));

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
        private List<IStaff> _staff;

        public FakeFileReader(List<IStaff> staff)
        {
            _staff = staff;
        }

        public List<IStaff> ReadFile()
        {
            return _staff;
        }
    }


    class FakeFactory : IAlternateFactoryPaySlip
    {
        private readonly PaySlip _stub;

        public FakeFactory(PaySlip stub)
        {
            _stub = stub;
        }
        public IPaySlip Create(int month, int year)
        {
            return _stub;
        }
    }
    
}
