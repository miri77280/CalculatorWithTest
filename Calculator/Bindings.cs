using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculator
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {

            Bind<IDataSaver>().To<FileDataSaver>();
            Bind<ICalculator>().To<Calculator>();

        }
    }
}
