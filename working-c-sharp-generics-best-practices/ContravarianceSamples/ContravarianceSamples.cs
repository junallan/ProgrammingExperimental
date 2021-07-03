using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContravarianceSamples
{
    interface ISequenceWriter<in T>
    {
        void Add(T item);
    }

    interface ISequenceReader<out T>
    {
        T GetNextItem();
    }

    interface ISequence<T> : ISequenceReader<T>, ISequenceWriter<T> { }

    internal class ContravarianceSamples
    {
        internal static void Execute()
        {
            var sequence = new MemorySequence<Person>();

            AddPeople(sequence);
            AddAuthors(sequence);

            PrintSequence(sequence);
        }

        private static void PrintSequence(ISequenceReader<Person> sequence)
        {
            while(true)
            {
                var currentItem = sequence.GetNextItem();
                if (currentItem == null) break;
                Console.WriteLine(currentItem);
            }
        }

        private static void AddAuthors(ISequenceWriter<Author> sequence)
        {
            sequence.Add(new Author { FirstName = "Steve", LastName = "Smith" });
        }

        private static void AddPeople(ISequenceWriter<Person> sequence)
        {
            sequence.Add(new Person { FirstName = "George", LastName = "Washington" });
        }
    }

   



    class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }
    }

    class Author : Person { }
}
