using System;
using System.Reflection;

namespace ReflectionSample
{
    class Program
    {
        static void Main(string[] args)
        {
            string name = "Jun";
            var stringType = name.GetType();
            Console.WriteLine(stringType);
            Console.WriteLine(typeof(string));

            var currentAssembly = Assembly.GetExecutingAssembly();
            var typesFromCurrentAssembly = currentAssembly.GetTypes();

            foreach(var type in typesFromCurrentAssembly)
            {
                Console.WriteLine(type.Name);
            }

            var oneTypeFromCurrentAssembly = currentAssembly.GetType("ReflectionSample.Person");
            Console.WriteLine(oneTypeFromCurrentAssembly.Name);

            var externalAssembly = Assembly.Load("System.Text.Json");
            var typesFromExternalAssembly = externalAssembly.GetTypes();
            var oneTypeFromExternalAssembly = externalAssembly.GetType("System.Text.Json.JsonProperty");

            var modulesFromExternalAssembly = externalAssembly.GetModules();
            var oneModuleFromExternalAssembly = externalAssembly.GetModule("System.Text.Json.dll");

            var typesFromModuleFromExternalAssembly = oneModuleFromExternalAssembly.GetTypes();
            var oneTypeFromModuleFromExternalAssembly = oneModuleFromExternalAssembly.GetType("System.Text.Json.JsonProperty");

            foreach(var constructor in oneTypeFromCurrentAssembly.GetConstructors())
            {  
                Console.WriteLine(constructor);
            }

            //foreach(var method in oneTypeFromCurrentAssembly.GetMethods())
            //{
            //    Console.WriteLine(method);
            //}

            foreach (var method in oneTypeFromCurrentAssembly.GetMethods(/*BindingFlags.Instance | */BindingFlags.Public | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{method}, public: {method.IsPublic}");
            }

            foreach (var method in oneTypeFromCurrentAssembly.GetFields(BindingFlags.Instance | BindingFlags.NonPublic))
            {
                Console.WriteLine($"{method}, public: {method.IsPublic}");
            }

            Console.ReadLine();
        }
    }
}
