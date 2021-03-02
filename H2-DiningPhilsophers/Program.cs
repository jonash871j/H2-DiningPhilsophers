using System;
using H2_DiningPhilsophers.Lib;

class Program
{
    static void Main(string[] args)
    {
        DiningManager manager = new DiningManager();

        foreach (Philosopher philosopher in manager.Philosophers)
        {
            philosopher.PhilosopherStateChanged += OnPhilosopherStateChanged;
            philosopher.PhilosopherDoneEating += OnPhilosopherDoneEating;
        }

        manager.StartDining();
    }

    private static void OnPhilosopherDoneEating(Philosopher philosopher)
    {
        Console.WriteLine(philosopher.Name + " is now done eating");
    }

    static void OnPhilosopherStateChanged(Philosopher philosopher)
    {
        switch (philosopher.State)
        {
            case PhilosopherState.Eating:
                Console.WriteLine(philosopher.Name + " is now eating with " + philosopher.LeftFork.Name + " and " + philosopher.RightFork.Name);
                break;
            case PhilosopherState.Thinking:
                Console.WriteLine(philosopher.Name + " is thinking...");
                break;
            case PhilosopherState.Waiting:
                Console.WriteLine(philosopher.Name + " is waiting...");
                break;
            default:
                break;
        }
    }
}
