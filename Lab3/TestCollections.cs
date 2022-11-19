using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace LR_C_sharp.Lab3
{
    class TestCollections
    {
        private List<Person> persons = new List<Person>();
        private List<string> stringList = new List<string>();

        private Dictionary<Person, Student> dict_PS =
            new Dictionary<Person, Student>();

        private Dictionary<string, Student> dict_sS =
            new Dictionary<string, Student>();

        public static Student autoGen(int value)
        {
            // Auto generation elements for collections
 /*           .AddRange(new List<Person>()
            {

            }
                );*/
            return new Student();
        }

        public TestCollections(int amount)
        {

        }

        public double SearchTime(object obj, int mode)
        {
            /* mode
             * 1 - obj = Person, search in List<Person> 
             * 
             * 2 - obj = string, search in List<string>
             * 
             * 3 - obj = Person, search in 
             *              Dictionary<Person, Student>
             * 4 - obj = Student, search in 
             *              Dictionary<Person, Student>
             * 5 - obj = string, search in 
             *              Dictionary<string, Student>
             * 6 - obj = Student, search in 
             *              Dictionary<string, Student>
             */
            
            
            // maybe throw exception
            if (mode < 1 || mode > 6)
                return 0;
            else
            {
                Stopwatch stopWatch = new Stopwatch();
                switch (mode)
                {
                    case 1:
                        {
                            Person p = (Person)obj;
                            stopWatch.Start();
                            persons.Find(
                                new Predicate<Person>(
                                    (Person x) => x.Equals(p)
                                    )
                                );
                            stopWatch.Stop();
                            break;
                        }
                    case 2:
                        {
                            string s = (string)obj;
                            stopWatch.Start();
                            stringList.Find(
                                new Predicate<string>(
                                    (string x) => x.Equals(s)
                                    )
                                );
                            stopWatch.Stop();
                            break;
                        }
                    case 3:
                        {
                            Person p = (Person)obj;
                            stopWatch.Start();
                            dict_PS.First(x => x.Key.Equals(p));
                            stopWatch.Stop();
                            break;
                        }
                    case 4:
                        {
                            Student s = (Student)obj;
                            stopWatch.Start();
                            dict_PS.First(x => x.Value.Equals(s));
                            stopWatch.Stop();
                            break;
                        }
                    case 5:
                        {
                            string s = (string)obj;
                            stopWatch.Start();
                            dict_sS.First(x => x.Value.Equals(s));
                            stopWatch.Stop();
                            break;
                        }
                    case 6:
                        {
                            Student s = (Student)obj;
                            stopWatch.Start();
                            dict_sS.First(x => x.Value.Equals(s));
                            stopWatch.Stop();
                            break;
                        }
                }
                return stopWatch.Elapsed.TotalMilliseconds;
            }

        }
    }
}
