using System;

namespace Code
{
    public class ToyBBuilder: IToyBuilder
    {
        Toy toy;
        public ToyBBuilder()
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
            toy.Body = "Steel";
        }

        public void SetHead()
        {
            toy.Head = "1";
        }

        public void SetLegs()
        {
            toy.Legs = "4";
        }

        public void SetLimbs()
        {
            toy.Limbs = "5";
        }

        public void SetModel()
        {
            toy.Model = "B";
        }
    }
}
