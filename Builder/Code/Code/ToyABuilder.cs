using System;

namespace Code
{
    public class ToyABuilder: IToyBuilder
    {
        Toy toy;
        public ToyABuilder()
        {
            toy = new Toy();
        }

        public Toy GetToy()
        {
            Console.Write(toy.Model);
            Console.Write(toy.Head);
            Console.Write(toy.Body);
            Console.Write(toy.Limbs);
            Console.Write(toy.Legs);
            Console.WriteLine("******************************************");
            return toy;
        }

        public void SetBody()
        {
            toy.Body = "Plastic";
        }

        public void SetHead()
        {
            toy.Head = "1";
        }

        public void SetLegs()
        {
            toy.Legs = "3";
        }

        public void SetLimbs()
        {
            toy.Limbs = "2";
        }

        public void SetModel()
        {
            toy.Model = "A";
        }
    }
}
