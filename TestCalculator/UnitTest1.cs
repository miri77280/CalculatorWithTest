using MyCalculator;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;
using Ninject;

namespace TestCalculator
{
    [TestFixture]
    public class Tests
    {
       

        private ICalculator _calculator;
        private List<CalculationResult> _dataList;
        private IDataSaver _dataSaver;
        private string lastValueStored = "";

        public Tests()
        {
            // Use underscore for private members
            _dataList = new List<CalculationResult>();
           // _dataSaver = Substitute.For<IDataSaver>();
           // _calculator = new Calculator(_dataSaver);
        }

        // Running one time
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            //Bind<IDataSaver>().To<FileDataSaver>();
            //  bindings.Rebind<IDataSaver>().To<Substitute.For<IDataSaver>()>().InRequestScope();

            var kernel = new StandardKernel();
            // Set up kernel
            var substitute = Substitute.For<IDataSaver>();
            kernel.Rebind<IDataSaver>().ToConstant(substitute);
            _dataSaver = kernel.Get<IDataSaver>();
            _calculator = new Calculator(_dataSaver);
        }

        // Running before every test
        [SetUp]
        public void Setup()
        {
            _calculator.ZeroValue();
        }

        [Test, Order(1)]
        public void TestAdd()
        {
            Assert.AreEqual(10, _calculator.Add(10));
        }

        [Test, Order(2)]
        public void TestSubstract()
        {
            _calculator.Add(10);
            Assert.AreEqual(5, _calculator.Substract(5));
        }

        [Test, Order(3)]
        public void TestMultiply()
        {
            _calculator.Add(1);
            Assert.AreEqual(2, _calculator.Multiply(2));
            Assert.Zero(_calculator.Multiply(0));
        }

        [Test, Order(4)]
        public void TestDivide()
        {
            _calculator.Add(10);
            Assert.AreEqual(5, _calculator.Divide(2));
        }

        [Test, Order(5)]
        public void TestSave()
        {
            _calculator.Add(10);
            var currentValue = _calculator.GetValue().ToString();

            _dataSaver
                .When(x => x.SaveData(_calculator.GetValue().ToString()))
                .Do(x =>
            {
                var calculationResult = new CalculationResult()
                {
                    Result = currentValue,
                    TimeStamp = System.DateTime.Today
                };

                _dataList.Add(calculationResult);
            });

            _calculator.SaveData(currentValue);
            lastValueStored = currentValue;
            Assert.That(_dataList.Any(p => p.Result == currentValue));
        }

        [Test, Order(6)]
        public void TestRestore()
        {
            // TRIPLE A.A.A.

            // 'A'rrange
            // 'A'ct
            // 'A'ssert

            // Arrange
            lastValueStored = "1234";
            _dataSaver.RestoreData().Returns(lastValueStored);
              // TODO:
            // Test, that passing mock to Calculator
            // Asing calucalator to "load data"
            // and assert on loaded data

            // ACT
            var expected = _calculator.RestoreData();

            // ASSERT: Check last value
            Assert.That(expected, Is.EqualTo(lastValueStored));
        }

        [Test]
        public void TestRealSave()
        {
            var fileSaver = new FileDataSaver();
            var calculatorReal = new Calculator(fileSaver);
            calculatorReal.Add(50);
            calculatorReal.SaveData();

            Assert.That("50", Is.EqualTo(calculatorReal.RestoreData()));
        }
    }
}