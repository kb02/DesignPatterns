using System;
namespace SingletonPattern
{
    public sealed class ThreadSafeDoubleCheck
    {
        ThreadSafeDoubleCheck()
        {
        }

        private static readonly object padLock = new object();
        private static ThreadSafeDoubleCheck instance = null;

        public static ThreadSafeDoubleCheck Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (padLock)
                    {
                        if(instance == null)
                        {
                            instance = new ThreadSafeDoubleCheck();
                        }
                    }
                }
                return instance;
            }
        }

        public void PrintClassName()
        {
            Console.WriteLine("Class name is ThreadSafeDoubleCheck");
        }
    }
}
