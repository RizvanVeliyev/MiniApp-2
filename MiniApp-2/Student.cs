using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp_2
{
    class Student
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Surname { get; private set; }

        public Student(string name, string surname)
        {
            Id = ++_id;
            Name = name;
            Surname = surname;
        }
    }
}
