using System;


namespace StrategyPatternSimulation
{
	public abstract class Duck
	{
		public IFlyBehavior _flyBehavior;
		public IQuackBehaviour _quackBehaviour;

		public Duck()
		{
		}

		public abstract void Display();

		public void PerformFly()
		{
			_flyBehavior.Fly();
		}

		public void PerformQuack()
		{
			_quackBehaviour.Quacking();
		}

		public void Swim()
		{
			Console.WriteLine("All ducks float, even decoys!s");
		}

		public void SetFlyBehavior(IFlyBehavior flyBehavior)
		{
			_flyBehavior = flyBehavior;
		}

		public void SetQuackBehaviour(IQuackBehaviour quackBehaviour)
		{
			_quackBehaviour = quackBehaviour;
		}
	}
}