using System;
namespace SingletonPattern
{
    public sealed class DotnetLazyType
    {
        private DotnetLazyType()
        {
        }
        private static readonly Lazy<DotnetLazyType> lazy = new Lazy<DotnetLazyType>(() => new DotnetLazyType());
        public static DotnetLazyType Instance
        {
            get
            {
                return lazy.Value;
            }
        }
        public void PrintClassName()
        {
            Console.WriteLine("Class is DotnetLazyType");
        }
    }
}
