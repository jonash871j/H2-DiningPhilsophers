using System.Threading;

namespace H2_DiningPhilsophers.Lib
{
    public class DiningManager
    {
        public DiningManager()
        {
            Forks = new Fork[]
            {
                new Fork("F1"),
                new Fork("F2"),
                new Fork("F3"),
                new Fork("F4"),
                new Fork("F5"),
            };

            Philosophers = new Philosopher[]
            {
                new Philosopher("P1", Forks[0], Forks[1]),
                new Philosopher("P2", Forks[1], Forks[2]),
                new Philosopher("P3", Forks[1], Forks[2]),
                new Philosopher("P4", Forks[3], Forks[4]),
                new Philosopher("P5", Forks[4], Forks[0]),
            };
        }

        public Fork[] Forks { get; private set; }
        public Philosopher[] Philosophers { get; private set; }

        public void StartDining()
        {
            for (int i = 0; i < Philosophers.Length; i++)
            {
                Thread thread = new Thread(Philosophers[i].TakeFork);
                thread.Start();
            }
        }
    }
}
