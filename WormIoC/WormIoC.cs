using System;

namespace WormIoC
{
    public class WormIoC
    {
        static Container testContainer;

        public static void Main(string[] args)
        {
            testContainer = new Container();

            testContainer.Register<IBar, Bar>();
            testContainer.Register<IFoo, Foo>(Lifecycle.Singleton);

            IFoo test = testContainer.Retrieve<IFoo>();
            test.Output("this is a test");
            Console.ReadKey();

            try
            {
                Console.WriteLine(testContainer.Retrieve<IFormatProvider>().GetType());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
