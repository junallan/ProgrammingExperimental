using CodeProjectCache;
using Generic.Commands;
using System;

namespace ClassesMethods
{
    class Program
    {
        static void Main(string[] args)
        {
            string message = "Generics are fun!";
            var cache = new Cache<string, string>();
            string key = "fun-message";
            cache.AddOrUpdate(key, message);
            string cachedResult = cache.Get(key);
            Console.WriteLine(cachedResult);

            string message2 = "Lazy loading is fun, too.";
            string message2key = "lazy-message";
            Cache.Global.AddOrUpdate(message2key, message2);
            object cachedResult2 = Cache.Global.Get(message2key);
            Console.WriteLine(cachedResult2);

            string[] inputs = { "Follow ", "Steve ", "on ", "Pluralsight.com ", "to ", "get ", "updates!" };
            ICommand<string> c = new ConcatCommand(inputs);
            string results = c.Execute();
            Console.WriteLine(results);
        }
    }
}
