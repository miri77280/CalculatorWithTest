using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    public interface IDataSaver
    {
        void SaveData(string data);
        string RestoreData();
                   
    }
}
