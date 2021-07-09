using System;

namespace EventsDelegates
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Generic Events and Delegates");

            var authorRepo = new Repository<Author>();
            authorRepo.EntityAdded += Repo_EntityAdded;
            authorRepo.Add(new Author("Jun", "Parreno"));

           
        }

        private static void Repo_EntityAdded(object sender, EntityAddedEventArgs<Author> args)
    }

    public record Author(string FirstName, string LastName);
    public record Course(string CourseName);
}
