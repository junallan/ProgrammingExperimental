using System;
using System.Collections.Generic;

namespace EventsDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Events and Delegates");

            int count = 0;
            
            var authorRepo = new Repository<Author>();
            var courseRepo = new Repository<Course>();

            var authorLoader = new DataLoader<Author>(authorRepo);
            var courseLoader = new DataLoader<Course>(courseRepo);

            authorLoader.Load(Authors());
            Console.WriteLine($"Loaded {authorLoader.Counter} authors.");
            courseLoader.Load(Courses());
            Console.WriteLine($"Loaded {courseLoader.Counter} courses.");

            authorRepo.EntityAdded += Repo_EntityAdded;
            authorRepo.EntityAdded += (o, e) => count++;
            authorRepo.Add(new Author("Jun", "Parreno"));
            Console.WriteLine($"{count} authors added.");

            Console.WriteLine("Listing al authors:");
            var authors = authorRepo.List();

            foreach(var author in authors)
            {
                Console.WriteLine(author);
            }

            Console.WriteLine("Listing all courses:");
            var courses = courseRepo.List();
            foreach(var course in courses)
            {
                Console.WriteLine(course);
            }
        }

        private static void Repo_EntityAdded(object sender, EntityAddedEventArgs<Author> args)
        {
            Console.WriteLine($"Author added: {args.EntityAdded} (via Program.cs) by {sender}");
        }

        public static IEnumerable<Author> Authors()
        {
            yield return new Author("Jun", "Parreno");
        }

        public static IEnumerable<Course> Courses()
        {
            yield return new Course("Domain-Driven Design Fundamentals");
            yield return new Course("Kanban: Getting Started");
            yield return new Course("C# Design Patterns: Rules Engine Pattern");
            yield return new Course("C# Design patterns: Memento");
            yield return new Course("C# Design Patterns: Template Method");
            yield return new Course("Design Patterns Overview");
            yield return new Course("C# Design Patterns: Singleton");
            yield return new Course("C# Design Patterns: Proxy");
            yield return new Course("C# Design Patterns: Adapter");
            yield return new Course("Refactoring for C# Developers");
            yield return new Course("SOLID Principles for C# Developers");
        }
    }

   

    public record Author(string FirstName, string LastName);
    public record Course(string CourseName);
}
