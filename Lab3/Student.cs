using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab3
{
    class Student : Person, IDateAndCopy
    {
        private Education education;
        private int numberOfGroup;
        private List<Test> tests = new List<Test>();
        private List<Exam> exams = new List<Exam>();

        public List<Test> Tests { get => tests; set => tests = value; }
        public List<Exam> Exams { get => exams; set => exams = value; }
        public Education Education { get => education; set => education = value; }
        public int NumberOfGroup
        {
            get => numberOfGroup;
            set
            {
                if (value > 100 && value <= 599)
                    numberOfGroup = value;
                else
                    throw new Exception("Номер группы должен быть в диапазоне (100, 599]");
            }
        }

        public Student()
            : base("", "", new DateTime())
        {
            Education = new Education();
            NumberOfGroup = 101;
        }

        public Student(Student st)
            : base(st.Name, st.Surname, st.BirthDate)
        {
            Education = st.Education;
            numberOfGroup = st.NumberOfGroup;
            Tests = st.Tests;
            Exams = st.Exams;
        }
        public Student(string name, string surname, DateTime birthDate, Education edu, int group)
            : base(name, surname, birthDate)
        {
            Education = edu;
            NumberOfGroup = group;
        }

        public Student(Person person, Education edu, int group)
            : base(person.Name, person.Surname, person.BirthDate)
        {
            Education = edu;
            NumberOfGroup = group;
        }
        //public List<> TestList { get => tests; set => tests = value; }
        //public List PassedExams { get => exams; set => exams = value; }

        public Person Person
        {
            get => (Person)base.DeepCopy();
            set
            {
                Name = value.Name;
                Surname = value.Surname;
                BirthDate = value.BirthDate;
            }
        }

        public double AverageGrade
        {
            get
            {
                double average = 0;
                foreach (var item in Exams)
                    average += item.Grade;
                average /= Exams.Count;
                return average;
            }
        }

        public bool this[Education edu]
        {
            get
            {
                if (education == edu)
                    return true;
                else
                    return false;
            }
        }

        /*        public object this[int i]
                {
                    get
                    { 
                        ArrayList testExamList = new ArrayList(TestList);
                        for (int j = 0; j < PassedExams.Count; j++)
                            testExamList.Add(PassedExams[j]);
                        return testExamList[i];
                    }    
                }*/

        public IEnumerable<Exam> GetHigher(int grade)
        {
            foreach (var item in Exams)
            {
                if (item.Grade > grade)
                    yield return item;
            }
        }

        // Доп.задание №1
        // Смог сделать без реализации Student Enumerator
        public IEnumerable<Object> ExamsAndTests()
        {

            ArrayList testExamList = new ArrayList(Tests);
            foreach (var item in Exams)
                testExamList.Add(item);
            foreach (var item in testExamList)
            {
                yield return item;
            }
        }
        // Доп.задание №1

        // Доп.задание №2
        public IEnumerable<object> PassedExamsAndTests()
        {
            ArrayList testExamList = new ArrayList();
            foreach (var item in Tests)
            {
                if (item.Passed == true)
                    testExamList.Add(item);
            }
            foreach (var item in Exams)
            {
                if (item.Grade > 2)
                    testExamList.Add(item);
            }
            foreach (var item in testExamList)
            {
                yield return item;
            }
        }
        // Доп.задание №2

        // Доп.задание №3
        public IEnumerable<Object> PassedTestsOnSubject()
        {
            ArrayList testExamList = new ArrayList();
            foreach (var item in Tests)
            {
                if (item.Passed == true)
                {
                    foreach (var jtem in Exams)
                    {
                        if (jtem.NameOfSubject == item.NameOfSubject)
                        {
                            if (jtem.Grade > 2)
                            {
                                testExamList.Add(item);
                            }
                        }
                    }
                }
            }
            foreach (var item in testExamList)
            {
                yield return item;
            }
        }
        // Доп.задание №3

        public string ExamsToString()
        {
            string str = "";
            foreach(var item in Exams)
                str += '\t' + item.ToString() + '\n';
            return str;
        }

        public string TestsToString()
        {
            string str = "";
            foreach (var item in Tests)
                str += item.ToString();
            return str;
        }

        public void AddExams(params Exam[] elements)
        {
            List<Exam> examList = new List<Exam>(Exams);
            for (int i = 0; i < elements.Length; i++)
                examList.Add(elements[i]);
/*                Exams.Add(elements[i]);*/
            //? Лучше вводить переменную или обращаться каждый раз напрямую
            // Есть мысль, что при введении переменной получается, 
            // что мы обращаемся всего 1 раз, а не n
            Exams = examList;
        }

        public override string ToString()
        {
            string tests = TestsToString(),
                exams = ExamsToString();
            if (tests.Equals(""))
                tests = "-";
            if (exams.Equals(""))
                exams = "-";
            return base.ToString() + '\n' +
                "Форма обучения: " + EduToString() + '\n' +
                "Номер группы: " + NumberOfGroup + '\n' +
                "Зачёты: " + tests + '\n' +
                "Сданные экзамены: " + exams;
        }

        public virtual string ToShortString()
        {
            return base.ToString() + '\n' +
                "Форма обучения: " + EduToString() + '\n' +
                "Номер группы: " + NumberOfGroup + '\n' +
                "Средний балл: " + AverageGrade;
        }

        public string EduToString()
        {
            string edu = education switch
            {
                Education.Bachelor => "Бакалавриат",
                Education.Specialist => "Специалитет",
                Education.SecondEducation => "Второе образование",
                _ => "Неизвестно",
            };
            return edu;
        }

        public override object DeepCopy()
        {
            return new Student(this);
        }

        static void Main(string[] args)
        {
        }
    }
}
