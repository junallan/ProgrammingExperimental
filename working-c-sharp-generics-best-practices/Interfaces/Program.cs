using System;
using System.Diagnostics.CodeAnalysis;

namespace Interfaces
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var studentsService = new StudentPrinterService(new StudentRepository());
            studentsService.PrintStudents();

            Console.WriteLine();

            var authorService = new AuthorPrinterService(new AuthorRepository());
            authorService.PrintAuthors();

          
        }
    }

    //public interface IStudentRepository
    //{
    //    Student[] List();
    //}

    public class StudentRepository : IRepository<Student>
    {
        public Student[] List()
        {
            var students = new Student[10];
            students[0] = new Student("Steve", "Smith");
            students[1] = new Student("Chad", "Smith");
            students[2] = new Student("Ben", "Smith");
            students[3] = new Student("Eric", "Smith");
            students[4] = new Student("Julie", "Lerman");
            students[5] = new Student("David", "Starr");
            students[6] = new Student("Aaron", "Skonnard");
            students[7] = new Student("Aaron", "Stewart");
            students[8] = new Student("Aaron", "Powell");
            students[9] = new Student("Aaron", "Frost");

            return students;
        }
    }

    //public interface IAuthorRepository
    //{
    //    Author[] List();
    //}

    public interface IRepository<T>
    {
        T[] List();
    }

    public class AuthorRepository : IRepository<Author>
    { 
        public Author[] List()
        {
            var authors = new Author[10];
            var students = new StudentRepository().List();
            for (int i = 0; i < students.Length; i++)
            {
                authors[i] = new Author(students[i].FirstName, students[i].LastName);
            }

            return authors;
        }
    }


    public class StudentPrinterService
    {
        private readonly IRepository<Student> _studentRepository;
        public StudentPrinterService(IRepository<Student> studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public void PrintStudents()
        {
            var students = _studentRepository.List();

            Array.Sort(students);

            Console.WriteLine("Students:");

            for (int i = 0; i < students.Length; i++)
            {
                Console.WriteLine(students[i]);
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
            var authors = _authorRepository.List();

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
        public string FirstName { get; set; }
        public string LastName { get; set; }

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
