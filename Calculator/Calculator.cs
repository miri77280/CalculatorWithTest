namespace MyCalculator
{
    //class x : NinjectModul
    //{
    //    // registering dependencies
    //    // bind between interfaces and implmentation
    //}

    // One time setup -> Rebind ( to mock )
    // When we asking for calcuator -> with ICalculator
    // Calculator ? IDataSaver ? -> mock


    public class Calculator : ICalculator
    {
        private double result = 0;
       
        private IDataSaver dataSaver;

        public Calculator(IDataSaver dataSaver)
        {
            this.dataSaver = dataSaver;
        }

         public  double GetValue()
        {
            return result;
        }

        public void ZeroValue()
        {
            result = 0;
        }

        public double Add(double num)
        {
            return result += num;
        }

        public double Substract(double num)
        {
            return result -= num;
        }

        public double Divide(double num)
        {
            return result /= num;
        }

        public double Multiply(double num)
        {
            return result *= num;
        }

        public string RestoreData()
        {
            return this.dataSaver.RestoreData();
        }

        public void SaveData(string data="")
        {
           this.dataSaver.SaveData(result.ToString());
        }
    }
}