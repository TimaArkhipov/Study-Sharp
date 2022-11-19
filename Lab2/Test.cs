using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab2
{
    class Test : IDateAndCopy
    {
        public string NameOfSubject { get; set; }
        public bool Passed { get; set; }

        public Test()
        {
            NameOfSubject = "";
            Passed = false;
        }

        public Test(string nameOfSubject, bool passed)
        {
            NameOfSubject = nameOfSubject;
            Passed = passed;
        }

        public override string ToString()
        {
            string str = "\nНазвание предмета: " + NameOfSubject;
            if (Passed)
                str += "\nЗачет: сдан";
            else
                str += "\nЗачет: не сдан";
            return str;
        }

        public static bool operator ==(Test t1, Test t2)
        {
            return t1.NameOfSubject == t2.NameOfSubject &&
                t1.Passed == t2.Passed;
        }

        public static bool operator !=(Test t1, Test t2)
        {
            return t1.NameOfSubject != t2.NameOfSubject ||
                t1.Passed != t2.Passed;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Test p = (Test)obj;
                return this == p;
            }
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(NameOfSubject, Passed);
        }

        public object DeepCopy()
        {
            return new Test(NameOfSubject, Passed);
        }
    }
}
