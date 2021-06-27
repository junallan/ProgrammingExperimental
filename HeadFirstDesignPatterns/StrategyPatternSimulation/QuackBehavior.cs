using System;

namespace StrategyPatternSimulation
{
    public interface IQuackBehaviour
    {
        public void Quacking();
    }


    public class Quack : IQuackBehaviour
    {
        public void Quacking()
        {
            Console.WriteLine("Quack");
        }
    }

    public class MuteQuack : IQuackBehaviour
    {
        public void Quacking()
        {
            Console.WriteLine("<< Silence >>");
        }
    }

    public class Squeck : IQuackBehaviour
    {
        public void Quacking()
        {
            Console.WriteLine("Squeak");
        }
    }
}