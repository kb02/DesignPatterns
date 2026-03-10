namespace Code
{
    public interface IToyBuilder
    {
        void SetModel();
        void SetLimbs();
        void SetLegs();
        void SetBody();
        void SetHead();
        Toy GetToy();
    }
}
