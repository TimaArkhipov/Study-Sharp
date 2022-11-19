using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LR_C_sharp.Lab2
{
    class StudentEnumerator : IEnumerator
    {
        ArrayList exam;
        ArrayList test;
        int index = -1;

        public StudentEnumerator(ArrayList exam, ArrayList test)
        {
            this.exam = exam;
            this.test = test;
        }

        public object Current => (exam[index] as Exam).NameOfSubject;

        public bool MoveNext()
        {
            index++;
            for (;index < exam.Count; index++)
            {
                foreach (var item in test)
                {
                    if ((exam[index] as Exam).NameOfSubject == (item as Test).NameOfSubject)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Reset()
        {
            index = -1;
        }
    }
}
