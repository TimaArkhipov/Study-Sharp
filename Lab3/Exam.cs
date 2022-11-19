using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab3
{
    class Exam : IDateAndCopy, IComparable, IComparer<Exam>
    {
        private string nameOfSubject;
        private int grade;
        private System.DateTime date;

        public Exam()
        {
            this.nameOfSubject = "-";
            this.grade = -1;
            this.date = new DateTime();
        }
        public Exam(string nameOfSubject, int grade, DateTime date)
        {
            this.nameOfSubject = nameOfSubject;
            this.grade = grade;
            this.date = date;
        }

        public string NameOfSubject { get => nameOfSubject; set => nameOfSubject = value; }
        public int Grade { get => grade; set => grade = value; }
        public DateTime Date { get => date; set => date = value; }

        public int Compare(Exam? x, Exam? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("Сравнение невозможно если один(или оба) объекта null");
            }
            else
            {
                return x.Grade.CompareTo(y.Grade);
            }
        }

        public int CompareTo(object? obj)
        {
            return nameOfSubject.CompareTo(obj);
        }

        public object DeepCopy()
        {
            return new Exam(NameOfSubject, Grade, Date);
        }

        public override string ToString()
        {
            return "\nНазвание предмета: " + nameOfSubject + "\nОценка: " + grade +
                "\nДата экзамена: " + date.ToShortDateString();
        }
    }

    public class ExamDateComparer : IComparer<Exam>
    {
        int IComparer<Exam>.Compare(Exam? x, Exam? y)
        {
            if (x == null || y == null)
            {
                throw new ArgumentNullException("Сравнение невозможно если один(или оба) объекта null");
            }
            else
            {
                if (x.Date == new DateTime() || y.Date == new DateTime())
                {
                    throw new Exception("Какая-то из дат или обе не заданы");
                }
                else
                {
                    return x.Date.CompareTo(y.Date);
                }
            }
        }
    }
}
