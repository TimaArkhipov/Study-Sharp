using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab3
{
    class StudentCollection  //IComparer<Student>
    {
        private List<Student> students = new List<Student>();

        public double MaxAverageGrade
        {
            get
            {
                if (students.Count != 0)
                    return students.Max(a => a.AverageGrade);
                else
                    return 0.1;
            }
        }

        public IEnumerable<Student> SubsetSpec
        {
            get 
            {
                return students.Where(student =>
                            student.Education ==
                            Education.Specialist);
            }
        }

        public List<Student> AverageMarkGroup(double value)
        {
            var h = students
                .GroupBy(student => student.AverageGrade)
                .ToList();
            return new List<Student>();
            //return h;
        }

        public void AddDefaults()
        {
            students.Add(new Student()
            {
                Name = "Дефолт",
                Surname = "Дефолтов",
                BirthDate = new DateTime(2002, 2, 2),
                Education = Education.Bachelor,
                NumberOfGroup = 155
            }
                        );
            students.Add(new Student()
            {
                Name = "Сергей",
                Surname = "Шишкин",
                BirthDate = new DateTime(1999, 7, 14),
                Education = Education.Specialist,
                NumberOfGroup = 156
            }
                        );
            students.Add(new Student()
            {
                Name = "Алексей",
                Surname = "Хрупкий",
                BirthDate = new DateTime(1993, 3, 20),
                Education = Education.SecondEducation,
                NumberOfGroup = 269
            }
                        );
        }

        public void AddStudents(params Student[] studs)
        {
            students.AddRange(studs);
            //foreach (Student item in studs)
            //    students.Add(item);
        }

        //public int Compare(Student? x, Student? y)
        //{
        //    if (!x.Equals(null) && !y.Equals(null))
        //        return x.AverageGrade.CompareTo(y.AverageGrade);
        //    else if (x.Equals(null) && !y.Equals(null))
        //        throw new ArgumentNullException("Первый аргумент(x) сравнения = null");
        //    else if (!x.Equals(null) && y.Equals(null))
        //        throw new ArgumentNullException("Второй аргумент(y) сравнения = null");
        //    else
        //        throw new ArgumentNullException("Оба аргумента сравнения = null");
        //}

        public override string? ToString()
        {
            string ? result = string.Empty;
            foreach (var item in students)
                result += item.ToString();
            return result;
        }
        
        public string? ToShortString()
        {
            string? result = string.Empty;
            foreach (var item in students)
                result += item.ToShortString();
            return result;
        }

        public void SortByBirthDate()
        {
            students.Sort(((new Student()) as Person).Compare);
        }

        public void SortByGrade()
        {
            //students.Sort(this);
        }

        //public static void Main()
        //{
        //    StudentCollection sc = new StudentCollection();
        //    sc.AddDefaults();
        //    sc.SortByBirthDate();
        //    Console.WriteLine(sc.ToString());
        //}
    }
}
