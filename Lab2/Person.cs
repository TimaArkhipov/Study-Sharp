using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab2
{
    class Person : IDateAndCopy
    {
        private string name;
        private string surname;
        private System.DateTime birthDate;

        public Person() { }

        public Person(string name, string surname, DateTime birthDate)
        {
            this.name = name;
            this.surname = surname;
            this.birthDate = birthDate;
        }

        public string Surname { get => surname; set => surname = value; }
        public string Name { get => name; set => name = value; }
        public DateTime BirthDate { get => birthDate; set => birthDate = value; }
        public DateTime Date { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        // ? как реализовать этот метод интерфейса?

        public int GetBirthYear()
        {
            return birthDate.Year;
        }
        public void SetBirthYear(int year)
        {
            BirthDate = new DateTime(birthDate.Day, birthDate.Month, year);
        }

        public override string ToString()
        {
            return "\nИмя: " + name + "\nФамилия: " + surname
                    + "\nДата рождения: " + birthDate.ToShortDateString();
        }
        public virtual string ToShortString()
        {
            return "\nName: " + name + "\nSurname: " + surname;
        }

        public override bool Equals(object obj)
        {
            return obj is Person person &&
                   Surname == person.Surname &&
                   Name == person.Name &&
                   BirthDate == person.BirthDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Surname, Name, BirthDate);
        }

        public virtual object DeepCopy()
        {
            return new Person(Name, Surname, BirthDate);
        }

        public static bool operator ==(Person p1, Person p2)
        {
            return p1.Surname == p2.Surname &&
                   p1.Name == p2.Name &&
                   p1.BirthDate == p2.BirthDate &&
                   p1.GetHashCode() == p2.GetHashCode();
        }

        public static bool operator !=(Person p1, Person p2)
        {
            return p1.Surname != p2.Surname ||
                   p1.Name != p2.Name ||
                   p1.BirthDate != p2.BirthDate ||
                   p1.GetHashCode() == p2.GetHashCode();
        }
    }
}
