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
        //public Operation<T> Add;

        public Func<T, T, T> Add;

        private readonly Func<T,bool> _predicate;
        private readonly Action<T> _outputIgnoredItems;

        private List<T> _args = new();

        public NewMath(IEnumerable<T> args, Func<T,T,T> addFunction, Func<T,bool> predicate, Action<T> outputIgnoredItems = null)
        {
            Add = addFunction;
            _predicate = predicate;
            _outputIgnoredItems = outputIgnoredItems;
            _args.AddRange(args);
        }

        public T Sum()
        {
            return _args.Aggregate(default(T), ProcessItem);
            //var ignoreItems = _args.Where(i => !_predicate(i)).ToList();
            //ignoreItems.ForEach(i => _outputIgnoredItems?.Invoke(i));

            //return _args.Where(_predicate).Aggregate(Add);

            //T total = default(T);
            //foreach(var arg in _args)
            //{
            //    if(_predicate(arg))
            //    {
            //        total = Add(total, arg);
            //    }   
            //    else
            //    {
            //        _outputIgnoredItems?.Invoke(arg);
            //    }
            //}
            //return total;
        }

        public T ProcessItem(T aggregation, T item)
        {
            if(_predicate(item))
            {
                return Add(aggregation, item);
            }

            _outputIgnoredItems?.Invoke(item);

            return aggregation;
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
            var instance = new NewMath<int>(input, (a, b) => a + b, Big, (i) => Console.WriteLine($"Ignoring {i}."));

            Console.WriteLine($"NewMath filtered sum is {instance.Sum()}");
        }
    }

}
