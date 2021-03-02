namespace H2_DiningPhilsophers.Lib
{
    public class Fork
    {
        public Fork(string name)
        {
            Name = name;   
        }

        public string Name { get; private set; }
    }
}
