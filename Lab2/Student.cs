using System;
using System.Collections;
using System.Collections.Generic;

public enum Education
{
    Specialist,
    Bachelor,
    SecondEducation
};


namespace LR_C_sharp.Lab2
{
    class Student : Person, IDateAndCopy
    {
        private Education education;
        private int numberOfGroup;
        private ArrayList testList = new ArrayList();
        private ArrayList passedExams = new ArrayList();

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
            TestList = st.TestList;
            PassedExams = st.PassedExams;
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
        public ArrayList TestList { get => testList; set => testList = value; }
        public ArrayList PassedExams { get => passedExams; set => passedExams = value; }

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
                ArrayList examList = PassedExams;
                for (int i = 0; i < examList.Count; i++)
                    average += ((Exam)examList[i]).Grade;
                average /= examList.Count;
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
            foreach (Exam item in PassedExams)
            {
                if (item.Grade > grade)
                    yield return item;
            }
        }

        // Доп.задание №1
        // Смог сделать без реализации Student Enumerator
        public IEnumerable<Object> ExamsAndTests()
        {

            ArrayList testExamList = new ArrayList(TestList);
            foreach (var item in PassedExams)
                testExamList.Add(item);
            foreach (var item in testExamList)
            {
                yield return item;
            }
        }
        // Доп.задание №1

        // Доп.задание №2
        public IEnumerable<Object> PassedExamsAndTests()
        {
            ArrayList testExamList = new ArrayList();
            foreach (var item in TestList)
            {
                if (((Test)item).Passed == true)
                    testExamList.Add(item);
            }
            foreach (var item in PassedExams)
            {
                if (((Exam)item).Grade > 2)
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
            foreach (var item in TestList)
            {
                if (((Test)item).Passed == true)
                {
                    foreach (var jtem in PassedExams)
                    {
                        if (((Exam)jtem).NameOfSubject == ((Test)item).NameOfSubject)
                        {
                            if (((Exam)jtem).Grade > 2)
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
            for (int i = 0; i < PassedExams.Count; i++)
                str += '\t' + PassedExams[i].ToString() + '\n';
            return str;
        }

        public string TestsToString()
        {
            string str = "";
            for (int i = 0; i < TestList.Count; i++)
                str += TestList[i].ToString();
            return str;
        }

        public void AddExams(params Exam[] elements)
        {
            ArrayList exams = new ArrayList(PassedExams);
            for (int i = 0; i < elements.Length; i++)
                exams.Add(elements[i]);
            PassedExams = exams;
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
            // 1
            // Хэш-коды одинаковые для двух одинаковых объектов(для их ссылок тоже), не знаю как исправить
            Person VR = new Person("Владимир", "Рулёв", new DateTime(2000, 1, 1));
            Person VRc = new Person("Владимир", "Рулёв", new DateTime(2000, 1, 1));
            //Person VRcopy = (Person)VR.DeepCopy();
            //ref Person f = ref VR;
            //ref Person f1 = ref VR;
            //Console.WriteLine("оригинал {0}\nЕго хэш-код: {1}\n", f.ToString(), f.GetHashCode());
            //Console.WriteLine("копия {0}\nЕго хэш-код: {1}\n", f1.ToString(), f1.GetHashCode());
            Console.WriteLine("Оригинал {0}\nЕго хэш-код: {1}\n", VR.ToString(), VR.GetHashCode().ToString());
            Console.WriteLine("Копия {0}\nЕго хэш-код: {1}\n", VRc.ToString(), VRc.GetHashCode().ToString()); 
            // 1

            // 2
            Exam exCpp = new Exam("C++", 5, new DateTime(2021, 1, 20));
            Exam exPy = new Exam("Python", 4, new DateTime(2020, 5, 10));
            Exam exPhp = new Exam("PHP", 3, new DateTime(2020, 3, 27));
            Exam exBd = new Exam("Базы данных", 3, new DateTime(2021, 12, 12));
            Exam exOs = new Exam("ОСиС", 4, new DateTime(2021, 9, 15));
            Exam exJava = new Exam("Java", 3, new DateTime(2020, 7, 14));
            ArrayList examList = new ArrayList() { exCpp, exPy, exPhp, exBd, exOs, exJava };
            Student stVR = new Student(VR, Education.Specialist, 154)
            {
                PassedExams = examList
            };
            Console.WriteLine(stVR.ToString());
            // 2

            // 3
            Console.WriteLine(stVR.Person.ToString());
            // 3

            // 4
            Student studentCopy = (Student)stVR.DeepCopy();
            stVR.Name = "Олег";
            Console.WriteLine("\nИзменённый оригинал{0}\n", stVR.ToShortString());
            Console.WriteLine("Копия{0}\n", studentCopy.ToShortString());
            // 4

            // 5
            try
            {
                studentCopy.NumberOfGroup = -3;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            // 5

            // 6
            Console.WriteLine("\n___________________________________________\nСписок всех экзаменов и зачётов:");
            studentCopy.TestList = new ArrayList() { new Test("C++", true), new Test("Java", false) };
            foreach (var item in studentCopy.ExamsAndTests())
            {
                Console.WriteLine(item.ToString());
            }
            // 6

            // 7
            int n = 3;
            Console.WriteLine("\n___________________________________________\nЭкзамены выше {0}:", n);
            foreach (var item in studentCopy.GetHigher(n))
            {
                Console.WriteLine(item.ToString());
            }
            // 7

            // Доп.задание №1
            // Не реализовано
            /*            foreach (var item in studentCopy)
                        {
                        }*/
            // Доп.задание №1

            // Доп.задание №2
            Console.WriteLine("\n___________________________________________\nВсе сданные экзамены и зачёты:");
            foreach (var item in studentCopy.PassedExamsAndTests())
            {
                Console.WriteLine(item);
            }
            // Доп.задание №2

            // Доп.задание №3
            Console.WriteLine("\n___________________________________________\nСданные зачёты, для которых сдан экзамен:");
            foreach (var item in studentCopy.PassedTestsOnSubject())
            {
                Console.WriteLine(item);
            }
            // Доп.задание №3
        }
    }
}
