using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab3
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
    }
}
