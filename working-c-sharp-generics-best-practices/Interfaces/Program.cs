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

            //Console.WriteLine($"Total Students Created: {Student.StudentCount}");

            //var authorService = new AuthorPrinterService(new AuthorRepository());
            //authorService.PrintAuthors();

          
        }
    }

   // public record Name(string First, string Last);

    public class StudentRepository : IPersonRepository<Student>
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

        public Student Create(Student student)
        {
            throw new NotImplementedException();
        }

        public Student CreateDefault()
        {
            throw new NotImplementedException();
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

        public IEnumerable<Student> Search(string name)
        {
            return List().Where(student => student.LastName.Contains(name)); //
                //student.LastName.Contains(name));
        }

        public IEnumerable<Student> SortedList()
        {
            var students = List().ToList();
            students.Sort();
            return students;
        }
    }

    public interface IRepository<T> where T : IComparable<T>
    {
        IEnumerable<T> List();
        IEnumerable<T> SortedList();
    }

    public interface IPersonRepository<T> : IRepository<T> where T : Person, IComparable<T>
    {
        IEnumerable<T> Search(string name);
        T Create(Student student);
        T CreateDefault();
    }

    public interface IPersonRepository
    {
        IEnumerable<T> Search<T>(string name) where T : Person;
        T Create<T>(Student name) where T : Person;
        T CreateDefault<T>() where T : Person, new();
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

        public IEnumerable<Author> SortedList()
        {
            throw new NotImplementedException();
        }
    }


    public class StudentPrinterService
    {
        private readonly IPersonRepository<Student> _studentRepository;
        public StudentPrinterService(IPersonRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents(int max = 100)
        {
            var students = _studentRepository.SortedList().Take(max).ToArray();

            //Array.Sort(students);

            //Console.WriteLine("Students:");

            PrintStudentsToConsole(students);

            var smiths = _studentRepository.Search("Smith");
            PrintSmithsToConsole(smiths);
        }

        private void PrintStudentsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Students:");
            foreach(var student in students)
            {
                Console.WriteLine(student);
            }
        }

        private void PrintSmithsToConsole(IEnumerable<Student> students)
        {
            Console.WriteLine("Smiths:");
            foreach (var student in students)
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

    public abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }


    public class Student : Person, IComparable<Student>  
    {
        public Student(string firstName, string lastName)
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

    public class Author : Person, IComparable<Author>
    {
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
