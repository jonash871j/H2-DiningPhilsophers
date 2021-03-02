using System;
using System.Threading;

namespace H2_DiningPhilsophers.Lib
{
    public delegate void PhilosopherEvent(Philosopher philosopher);

    public class Philosopher
    {
        public Philosopher(string name, Fork leftFork, Fork rightFork)
        {
            Name = name;
            LeftFork = leftFork;
            RightFork = rightFork;
            _state = PhilosopherState.Waiting;
        }

        private PhilosopherState _state;
        private static Random rng = new Random();

        public Fork LeftFork { get; private set; }
        public Fork RightFork { get; private set; }
        public string Name { get; private set; }
        public PhilosopherState State 
        { 
            get => _state;
            private set
            {
                PhilosopherStateChanged.Invoke(this);
                _state = value;
            }
        }

        public PhilosopherEvent PhilosopherStateChanged { get; set; }
        public PhilosopherEvent PhilosopherDoneEating { get; set; }

        public void TakeFork()
        {
            while (true)
            {
                if (Monitor.TryEnter(RightFork))
                {
                    if (Monitor.TryEnter(LeftFork))
                    {
                        BeginEating();

                        Monitor.Pulse(RightFork);
                        Monitor.Pulse(LeftFork);

                        Monitor.Exit(RightFork);
                        Monitor.Exit(LeftFork);

                        State = PhilosopherState.Thinking;
                    }
                    else
                    {
                        Monitor.Exit(RightFork);
                    }
                }

                if (State == PhilosopherState.Thinking)
                {
                    Thread.Sleep(rng.Next(1000, 4000));
                    State = PhilosopherState.Waiting;
                }
            }
        }

        private void BeginEating()
        {
            State = PhilosopherState.Eating;
            Thread.Sleep(rng.Next(1500, 3000));
            PhilosopherDoneEating.Invoke(this);
        }
    }
}
