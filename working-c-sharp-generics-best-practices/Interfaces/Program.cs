using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var studentsService = new StudentPrinterService(new StudentRepository());
            studentsService.PrintStudents(5);

            Console.WriteLine();

            Console.WriteLine($"Total Students Created: {Student.StudentCount}");

            //var authorService = new AuthorPrinterService(new AuthorRepository());
            //authorService.PrintAuthors();

          
        }
    }

   // public record Name(string First, string Last);

    public class StudentRepository : IRepository<Student>
    {
        private Student[] _names = new Student[10];

        public StudentRepository()
        {
            _names[0] = new Student("Steve", "Smith");
            _names[1] = new Student("Chad", "Smith");
            _names[2] = new Student("Ben", "Smith");
            _names[3] = new Student("Eric", "Smith");
            _names[4] = new Student("Julie", "Lerman");
            _names[5] = new Student("David", "Starr");
            _names[6] = new Student("Aaron", "Skonnard");
            _names[7] = new Student("Aaron", "Stewart");
            _names[8] = new Student("Aaron", "Powell");
            _names[9] = new Student("Aaron", "Frost");
        }


        public IEnumerable<Student> List()
        {
            int index = 0;
            while(index < _names.Length)
            {
                yield return _names[index];
                index++;
            }
        }
    }

    //public interface IAuthorRepository
    //{
    //    Author[] List();
    //}

    public interface IRepository<T>
    {
        IEnumerable<T> List();
    }

    public class AuthorRepository : IRepository<Author>
    { 
        public IEnumerable<Author> List()
        {
            var students = new StudentRepository().List();

            foreach (var student in students)
            {
                yield return new Author(student.FirstName, student.LastName);
            }
        }
    }


    public class StudentPrinterService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentPrinterService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents(int max = 100)
        {
            var students = _studentRepository.List().Take(max);

            //Array.Sort(students);

            Console.WriteLine("Students:");

            PrintStudentsToConsole(students);
        }

        private void PrintStudentsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Students:");
            foreach(var student in students)
            {
                Console.WriteLine(student);
            }
        }
    }

    public class AuthorPrinterService
    {
        private readonly IRepository<Author> _authorRepository;

        public AuthorPrinterService(IRepository<Author> authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public void PrintAuthors()
        {
            var authors = _authorRepository.List().ToArray();

            Array.Sort(authors);

            Console.WriteLine("Authors:");

            for (int i = 0; i < authors.Length; i++)
            {
                Console.WriteLine(authors[i]);
            }
        }
    }


    public class Student : IComparable<Student>  
    {
        public static int StudentCount = 0;
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Student(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            StudentCount++;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public int CompareTo(object obj)
        {
            if (obj is null) { return 1; }
            if(obj is Student otherStudent)
            {
                if(otherStudent.LastName == this.LastName)
                {
                    return this.FirstName.CompareTo(otherStudent.FirstName);
                }

                return this.LastName.CompareTo(otherStudent.LastName);
            }

            throw new ArgumentException("Not a student", nameof(obj));
        }

        public int CompareTo([AllowNull] Student other)
        {
            if (other is null) { return 1; }
         
                if (other.LastName == this.LastName)
                {
                    return this.FirstName.CompareTo(other.FirstName);
                }

                return this.LastName.CompareTo(other.LastName);
        
        }
    }

    public class Author : IComparable<Author>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public Author(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public int CompareTo(object obj)
        {
            if (obj is null) { return 1; }

            if (obj is Author otherAuthor)
            {
                return this.ToString().CompareTo(otherAuthor.ToString());
            }

            throw new ArgumentException("Not an Author", nameof(obj));
        }

        public int CompareTo([AllowNull] Author other)
        {
            if (other is null) { return 1; }
      
            return this.ToString().CompareTo(other.ToString());      
        }
    }


}
