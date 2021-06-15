﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ReflectionSample
{
    class Program
    {
        private static readonly string _typeFromConfiguration = "ReflectionSample.Alien";
        private static NetworkMonitorSettings _networkMonitorSettings = new NetworkMonitorSettings();
        private static Type _warningServiceType;
        private static MethodInfo _warningServiceMethod;
        private static object _warningService;
        private static List<object> _warningServiceParameterValues;


        static void Main(string[] args)
        {
            //InspectingMetadata();

            //InstantiatingAndManipulatingObjects();

            //NetworkMonitorExample();

            var myList = new List<Person>();
            Console.WriteLine(myList.GetType().Name);
            Console.WriteLine(myList.GetType());

            var myDictionary = new Dictionary<string, int>();
            Console.WriteLine(myDictionary.GetType());

            var dictionaryType = myDictionary.GetType();

            foreach(var genericTypeArgument in dictionaryType.GenericTypeArguments)
            {
                Console.WriteLine(genericTypeArgument);
            }

            foreach(var genericTypeArgument in dictionaryType.GetGenericArguments())
            {
                Console.WriteLine(genericTypeArgument);
            }

            var openDictionaryType = typeof(Dictionary<,>);

            foreach(var genericArgument in openDictionaryType.GetGenericArguments())
            {
                Console.WriteLine(genericArgument);
            }

            var createdInstance = Activator.CreateInstance(typeof(List<Person>));
            Console.WriteLine(createdInstance.GetType());

            //var createdResult = Activator.CreateInstance(typeof(Result<>));

            //var openResultType = typeof(Result<>);
            //var closedResultType = openResultType.MakeGenericType(typeof(Person));
            //var createdResult = Activator.CreateInstance(closedResultType);

            var openResultType = Type.GetType("ReflectionSample.Result`1");
            var closedResultType = openResultType.MakeGenericType(Type.GetType("ReflectionSample.Person"));
            var createdResult = Activator.CreateInstance(closedResultType);

            Console.WriteLine(createdResult.GetType());

            var methodInfo = closedResultType.GetMethod("AlterAndReturnValue");
            Console.WriteLine(methodInfo);

            var genericMethodInfo = methodInfo.MakeGenericMethod(typeof(Employee));

            genericMethodInfo.Invoke(createdResult, new object[] { new Employee() });

            Console.ReadLine();
        }

        private static void NetworkMonitorExample()
        {
            BootStrapFromConfiguration();

            Console.WriteLine("Monitoring network... something went wrong.");

            Warn();
        }

        private static void Warn()
        {
            if(_warningService == null)
            {
                _warningService = Activator.CreateInstance(_warningServiceType);
            }

            var parameters = new List<object>();

            foreach(var propertyBagItem in _networkMonitorSettings.PropertyBag)
            {
                parameters.Add(propertyBagItem.Value);
            }

            _warningServiceMethod.Invoke(_warningService, parameters.ToArray());
        }

        private static void BootStrapFromConfiguration()
        {
            var appSettingsConfig = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true)
                                                              .Build();
            appSettingsConfig.Bind("NetworkMonitorSettings", _networkMonitorSettings);

            _warningServiceType = Assembly.GetExecutingAssembly().GetType(_networkMonitorSettings.WarningService);

            if(_warningServiceType == null)
            {
                throw new Exception("Configuration is invalid - warning service not found");
            }

            _warningServiceMethod = _warningServiceType.GetMethod(_networkMonitorSettings.MethodToExecute);

            if(_warningServiceMethod == null)
            {
                throw new Exception("Configuration is invalid - method to execute on warning service not found");
            }

            foreach(var parameterInfo in _warningServiceMethod.GetParameters())
            {
                if(!_networkMonitorSettings.PropertyBag.TryGetValue(parameterInfo.Name, out object parameterValue))
                {
                    throw new Exception($"Configuration is invalid - parameter {parameterInfo.Name} not found.");
                }

                _warningServiceParameterValues = new List<object>();

                try
                {
                    var typedValue = Convert.ChangeType(parameterValue, parameterInfo.ParameterType);
                    _warningServiceParameterValues.Add(typedValue);
                }
                catch
                {
                    throw new Exception($"Configuration is invalid - parameter {parameterInfo.Name} cannot be converted to expected type {parameterInfo.ParameterType}");
                }
            }
            
        }

        private static void InstantiatingAndManipulatingObjects()
        {
            var personType = typeof(Person);
            var personConstructors = personType.GetConstructors(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            foreach (var personConstructor in personConstructors)
            {
                Console.WriteLine(personConstructor);
            }

            var privatePersonConstructor = personType.GetConstructor(
                        BindingFlags.Instance | BindingFlags.NonPublic,
                        null,
                        new Type[] { typeof(string), typeof(int) },
                        null);

            Console.WriteLine(privatePersonConstructor);

            var person1 = personConstructors[0].Invoke(null);
            var person2 = personConstructors[1].Invoke(new object[] { "Jun" });
            var person3 = personConstructors[2].Invoke(new object[] { "Jun", 42 });

            var person4 = Activator.CreateInstance("ReflectionSample", "ReflectionSample.Person").Unwrap();

            var person5 = Activator.CreateInstance("ReflectionSample",
                "ReflectionSample.Person",
                true,
                BindingFlags.Instance | BindingFlags.Public,
                null,
                new object[] { "Jun" },
                null,
                null);

            var personTypeFromString = Type.GetType("ReflectionSample.Person");
            var person6 = Activator.CreateInstance(personTypeFromString, new object[] { "Jun" });

            var person7 = Activator.CreateInstance("ReflectionSample",
                "ReflectionSample.Person",
                true,
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { "Jun", 42 },
                null,
                null);

            var assembly = Assembly.GetExecutingAssembly();
            var person8 = assembly.CreateInstance("ReflectionSample.Persson");

            var actualTypeFromConfiguration = Type.GetType(_typeFromConfiguration);
            var iTalkInstance = Activator.CreateInstance(actualTypeFromConfiguration) as ITalk;
            iTalkInstance.Talk("Hello world!");

            dynamic dynamicITalkInstance = Activator.CreateInstance(actualTypeFromConfiguration);
            dynamicITalkInstance.Talk("Hello world!");

            var personForManipulation = Activator.CreateInstance("ReflectionSample",
                "ReflectionSample.Person",
                true,
                BindingFlags.Instance | BindingFlags.NonPublic,
                null,
                new object[] { "Jun", 42 },
                null,
                null).Unwrap();

            var nameProperty = personForManipulation.GetType().GetProperty("Name");

            nameProperty.SetValue(personForManipulation, "Sven");

            Console.WriteLine(personForManipulation);

            var ageField = personForManipulation.GetType().GetField("age");
            ageField.SetValue(personForManipulation, 34);

            var privateField = personForManipulation.GetType().GetField("_aPrivateField",
                BindingFlags.Instance | BindingFlags.NonPublic);
            privateField.SetValue(personForManipulation, "updated private field value");

            personForManipulation.GetType().InvokeMember("Name", BindingFlags.Instance | BindingFlags.Public | BindingFlags.SetProperty,
                null, personForManipulation, new[] { "Emma" });

            personForManipulation.GetType().InvokeMember("_aPrivateField", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.SetField,
                null, personForManipulation, new[] { "second update for private field value" });

            Console.WriteLine(personForManipulation);

            var talkMethod = personForManipulation.GetType().GetMethod("Talk");
            talkMethod.Invoke(personForManipulation, new[] { "something to say" });

            personForManipulation.GetType().InvokeMember("Yell",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.InvokeMethod,
                null, personForManipulation, new[] { "something to yell" });
        }

        private static void InspectingMetadata()
        {
            string name = "Jun";
            var stringType = name.GetType();
            Console.WriteLine(stringType);
            Console.WriteLine(typeof(string));

            var currentAssembly = Assembly.GetExecutingAssembly();
            var typesFromCurrentAssembly = currentAssembly.GetTypes();

            foreach (var type in typesFromCurrentAssembly)
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

            foreach (var constructor in oneTypeFromCurrentAssembly.GetConstructors())
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
        }
    }
}
