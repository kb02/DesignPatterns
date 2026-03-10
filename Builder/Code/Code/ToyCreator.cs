using System;
namespace Code
{
    public class ToyCreator
    {
        IToyBuilder _toyBuilder;
        public ToyCreator(IToyBuilder toyBuilder)
        {
            _toyBuilder = toyBuilder;
        }

        public void CreateToy()
        {
            _toyBuilder.SetModel();
            _toyBuilder.SetLimbs();
            _toyBuilder.SetLegs();
            _toyBuilder.SetHead();
            _toyBuilder.SetBody();
        }

        public Toy GetToy()
        {
            return _toyBuilder.GetToy();
        }
    }
}
