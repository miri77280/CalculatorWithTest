using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    public interface ICalculator
    {
        double Add(double num);
        double sustract(double num);
        Double Multiply(double num);
        double Divide(double num);
        double GetValue();
        void ZeroValue();


    }
}
