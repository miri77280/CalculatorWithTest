using MyCalculator;
using NUnit.Framework;
using NSubstitute;
using System.Collections.Generic;
using System.Linq;

namespace TestCalculator
{
    [TestFixture]
    public class Tests
    {
        ICalculator calculator;
        List<CalculationResult> dataList;
        IDataSaver dataSaver;
        string lastValueStored = "";
        public Tests()
        {
            this.dataList = new List<CalculationResult>();
            this.dataSaver = Substitute.For<IDataSaver>();
            calculator = new Calculator(dataSaver);
        }
        [SetUp]
        public void Setup()
        {

            calculator.ZeroValue();
        }
       

        [Test, Order(1)]
        public void TestAdd()
        {
            Assert.AreEqual(10, calculator.Add(10));
        }

        [Test, Order(2)]
        public void TestSubstract()
        {
            calculator.Add(10);
            Assert.AreEqual(5, calculator.sustract(5));
        }
        [Test, Order(3)]
        public void TestMultiply()
        {
            calculator.Add(1);
            Assert.AreEqual(2, calculator.Multiply(2));
            Assert.Zero(calculator.Multiply(0));
        }
        [Test, Order(4)]
        public void TestDivide()
        {
            calculator.Add(10);
            Assert.AreEqual(5, calculator.Divide(2));
        }



        [Test, Order(5)]
        public void TestSave()
        {
            calculator.Add(10);
            var currentValue = calculator.GetValue().ToString();
            
            dataSaver.When(x => x.SaveData(calculator.GetValue().ToString())).Do(x => 
            {
                var calculationResult = new CalculationResult() { Result = currentValue, TimeStamp = System.DateTime.Today };
                dataList.Add(calculationResult);
             });

            dataSaver.SaveData(currentValue);
            lastValueStored = currentValue;
             Assert.That(dataList.Any(p => p.Result == currentValue));
        }



        [Test, Order(6)]
        public void TestRestore()
        {
            dataSaver.RestoreData().Returns(lastValueStored);
            var res = dataSaver.RestoreData();
            Assert.AreEqual("10", res);
        }
        //[Test]
        //public void TestRealSave()
        //{
        //    var fileSaver = new FileDataSaver();
        //    var calculatorReal = new Calculator(fileSaver);
        //    calculatorReal.Add(50);
        //    calculatorReal.SaveData();
        //    Assert.AreEqual("50", calculatorReal.RestoreData());
        //     }
    }
}