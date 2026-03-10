using System;
namespace SingletonPattern
{
    public sealed class ThreadSafetySingleton
    {
        ThreadSafetySingleton()
        {
        }

        private static readonly object padLock = new object();
        private static ThreadSafetySingleton instance = null;
        public static ThreadSafetySingleton Instance
        {
            get
            {
                lock (padLock)
                {
                    if(instance == null)
                    {
                        instance = new ThreadSafetySingleton();
                    }
                    return instance;
                }
            }
        }

        public void PrintClassName()
        {
            Console.WriteLine("Class name is ThreadSafetySingleton");
        }
    }
}
