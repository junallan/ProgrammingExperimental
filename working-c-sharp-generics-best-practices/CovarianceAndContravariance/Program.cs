using System;

namespace CovarianceAndContravariance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Reflection and Variance");

            ReflectionSamples.Execute
        }
    }

    public interface IProcessor<T>
    {
        void Process(T input);
    }

    public class Processor<T> : IProcessor<T>
    {
        public void Process(T input)
        {
            Console.WriteLine($"Generic Processor of T, processing {input}");
        }
    }

    public record Customer(string firstName, string lastName);
}
