using System;
namespace SingletonPattern
{
    public sealed class FullyLazyInstantiation
    {
        private FullyLazyInstantiation()
        {
        }

        public static FullyLazyInstantiation Instance
        {
            get
            {
                return Nested.obj;
            }
        }

        public void PrintClassName()
        {
            Console.WriteLine("Class name is FullyLazyInstantiation");
        }

        private class Nested
        {
            static Nested() { }
            internal static readonly FullyLazyInstantiation obj = new FullyLazyInstantiation();
        }
    }
}
