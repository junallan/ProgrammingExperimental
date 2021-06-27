using System;

namespace StrategyPatternSimulation
{
    public class MallardDuck : Duck
    {
        public MallardDuck()
        {
            _quackBehaviour = new Quack();
            _flyBehavior = new FlyWithWing();
        }

        public override void Display()
        {
            Console.WriteLine("I'm a real Mallard duck");
        }
    }
}