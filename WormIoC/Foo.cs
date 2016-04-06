using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WormIoC
{
    class Foo : IFoo
    {
        private IBar bar;

        public Foo(IBar bar)
        {
            this.bar = bar;
        }
        public void Output(string msg)
        {
            bar.Output(msg);
        }
    }
}
