using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab2
{
    class Exam : IDateAndCopy
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

        public object DeepCopy()
        {
            return new Exam(NameOfSubject, Grade, Date);
        }

        public override string ToString()
        {
            return "\nНазвание предмета: " + nameOfSubject + "\nОценка: " + grade +
                "\nДата экзамена: " + date.ToShortDateString();
        }

        public static bool operator ==(Exam e1, Exam e2)
        {
            return e1.NameOfSubject == e2.NameOfSubject &&
                   e1.Grade == e2.Grade &&
                   e1.Date.Equals(e2.Date);
        }

        public static bool operator !=(Exam e1, Exam e2)
        {
            return e1.NameOfSubject != e2.NameOfSubject ||
                   e1.Grade != e2.Grade ||
                   !e1.Date.Equals(e2.Date);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Exam p = (Exam)obj;
                return this == p;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NameOfSubject, Grade, Date);
        }
    }
}
