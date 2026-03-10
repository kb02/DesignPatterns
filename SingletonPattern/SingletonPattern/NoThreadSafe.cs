using System;
namespace SingletonPattern
{
    public sealed class NoThreadSafe
    {
        private NoThreadSafe()
        {
        }

        private static NoThreadSafe instance = null;
        public static NoThreadSafe Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new NoThreadSafe();
                }
                return instance;
            }
        }

        public void PrintClassName()
        {
            Console.WriteLine("Class name is NoThreadSafe");
        }
    }
}
