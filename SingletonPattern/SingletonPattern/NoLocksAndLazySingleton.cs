using System;
namespace SingletonPattern
{
    public sealed class NoLocksAndLazySingleton
    {
        private static readonly NoLocksAndLazySingleton instance = new NoLocksAndLazySingleton();

        static NoLocksAndLazySingleton()
        {
        }
        private NoLocksAndLazySingleton()
        {
        }

        public static NoLocksAndLazySingleton Instance
        {
            get
            {
                return instance;
            }
        }

        public void PrintClassName()
        {
            Console.WriteLine("Class name is NoLocksAndLazySingleton");
        }
    }
}
