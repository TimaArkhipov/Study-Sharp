using System;
using System.Collections.Generic;
using System.Text;

namespace LR_C_sharp.Lab3
{
    class Test
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
    }
}
