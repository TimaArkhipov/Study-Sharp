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

        //public ref Person basePerson
        //{
        //    get
        //    {
        //        return ref Person;
        //    }

        //}

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
                "Средний балл: " + AverageGrade + '\n' +
                "Число зачётов: " + Tests.Count + '\n' +
                "Число экзаменов: " + Exams.Count + '\n';
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

        public void sortExams(int mode)
        {
            List<Exam> examsTemp = Exams;
            switch (mode)
            {
                case 0: //Название предмета
                    for (int i = 0; i < examsTemp.Count; i++)
                    {
                        if (examsTemp.ElementAt(i).CompareTo(examsTemp.ElementAt(i + 1)) > 0)
                        {
                            (examsTemp[i + 1], examsTemp[i]) = (examsTemp[i], examsTemp[i + 1]);
                        }
                    }
                    break;
                case 1: // Оценка
                    for (int i = 0; i < examsTemp.Count; i++)
                    {
                        if ((new Exam()).Compare(examsTemp.ElementAt(i), examsTemp.ElementAt(i + 1)) > 0)
                        {
                            (examsTemp[i + 1], examsTemp[i]) = (examsTemp[i], examsTemp[i + 1]);
                        }
                    }
                    break;
                case 2: // Дата
                    examsTemp.Sort(new ExamDateComparer());
                    break;
                default:
                    throw new Exception("Неверный режим сортировки");
            }
            Exams = examsTemp;
        }

        static void Main(string[] args)
        {
            // Подготовка
            Exam exCpp = new Exam("C++", 5, new DateTime(2021, 1, 20));
            Exam exPy = new Exam("Python", 4, new DateTime(2020, 5, 10));
            Exam exPhp = new Exam("PHP", 3, new DateTime(2020, 3, 27));
            Exam exBd = new Exam("Базы данных", 3, new DateTime(2021, 12, 12));
            Exam exOs = new Exam("ОСиС", 4, new DateTime(2021, 9, 15));
            Exam exJava = new Exam("Java", 3, new DateTime(2020, 7, 14));
            List<Exam> examList = new List<Exam>() { exCpp, exPy, exPhp, exBd, exOs, exJava };
            List<Exam> examList1 = new List<Exam>() { exCpp, exPhp, exBd, exJava };
            List<Exam> examList2 = new List<Exam>() { exPy, exOs, exJava };

            List<Student> students = new List<Student>();
            students.Add(new Student()
            {
                Name = "Андрей",
                Surname = "Карпов",
                BirthDate = new DateTime(1000, 1, 1),
                Education = Education.SecondEducation,
                NumberOfGroup = 166
            });
            students.Add(new Student()
            {
                Name = "Сергей",
                Surname = "Лопанцов",
                BirthDate = new DateTime(2000, 12, 26),
                Education = Education.Specialist,
                NumberOfGroup = 178
            });
            students.Add(new Student()
            {
                Name = "Имя",
                Surname = "Фамилия",
                BirthDate = new DateTime(1998, 7, 10),
                Education = Education.Bachelor,
                NumberOfGroup = 202
            });

            students[0].Exams = examList;
            students[1].Exams = examList1;
            students[2].Exams = examList2;

            // Подготовка

            // 1
            StudentCollection sc = new StudentCollection();
            sc.AddStudents(students[0], students[1], students[2]);

            // 2
            // 2

            // 3
            Console.WriteLine("Максимальное значение среднего " +
                "балла для элементов списка: {0}\n", 
                sc.MaxAverageGrade.ToString());
            Console.WriteLine("Студенты с формой обучения " +
                "Education.Specialist: {0}\n",
                sc.SubsetSpec);
            // c - нет пока
            // 3
            
            // 4
            TestCollections tc = new TestCollections(4);
            // 4
        }
    }
}
