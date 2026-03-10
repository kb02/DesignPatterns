using System;

namespace Code
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Builder Pattern!");
            var toyAbuilder = new ToyCreator(new ToyABuilder());
            toyAbuilder.CreateToy();
            toyAbuilder.GetToy();

            var toyBbuilder = new ToyCreator(new ToyBBuilder());
            toyBbuilder.CreateToy();
            toyBbuilder.GetToy();
        }
    }
}
