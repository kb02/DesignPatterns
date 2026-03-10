using System;

namespace SingletonPattern
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            NoThreadSafe instance = NoThreadSafe.Instance;
            instance.PrintClassName();

            ThreadSafetySingleton threadSafetyInstance  = ThreadSafetySingleton.Instance;
            threadSafetyInstance.PrintClassName();

            ThreadSafeDoubleCheck threadSafetyDoubleCheckInstance = ThreadSafeDoubleCheck.Instance;
            threadSafetyDoubleCheckInstance.PrintClassName();

            NoLocksAndLazySingleton noLockAndLazyInstance = NoLocksAndLazySingleton.Instance;
            noLockAndLazyInstance.PrintClassName();

            FullyLazyInstantiation fullyLazyInstance = FullyLazyInstantiation.Instance;
            fullyLazyInstance.PrintClassName();

            DotnetLazyType dotnetLaztTypeInstance = DotnetLazyType.Instance;
            dotnetLaztTypeInstance.PrintClassName();
        }
    }
}
