namespace MyCalculator
{
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

        public void SaveData(string data = null)
        {
            if (data == null)
                ZeroValue();

            this.dataSaver.SaveData(result.ToString());
        }
    }
}