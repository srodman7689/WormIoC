using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormIoC
{
    class Bar : IBar
    {
        public void Output(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
