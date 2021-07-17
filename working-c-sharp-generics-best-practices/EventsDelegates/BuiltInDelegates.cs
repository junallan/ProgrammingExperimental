using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventsDelegates
{
    public delegate T Operation<T>(T op1, T op2);
    public class NewMath<T> where T : struct
    {
        public Operation<T> Add;

        private readonly Predicate<T> _predicate;

        private List<T> _args = new();

        public NewMath(IEnumerable<T> args, Operation<T> addFunction, Predicate<T> predicate)
        {
            Add += addFunction;
            _predicate = predicate;
            _args.AddRange(args);
        }

        public T Sum()
        {
            T total = default(T);
            foreach(var arg in _args)
            {
                if(_predicate(arg))
                {
                    total = Add(total, arg);
                }       
            }
            return total;
        }
    }

    public static class DelegateRunner
    { 
        private static bool Big(int input)
        {
            return input > 1000;
        }

        public static void Execute()
        {
            var input = new[] { 10, 1200, 20, 2000};
            var instance = new NewMath<int>(input, (a, b) => a + b, Big);

            Console.WriteLine($"NewMath filtered sum is {instance.Sum()}");
        }
    }

}
