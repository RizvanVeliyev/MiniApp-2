using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniApp_2
{
    class Classroom
    {
        private static int _id = 0;
        public int Id { get; private set; }
        public string Name { get; private set; }
        public ClassroomType Type { get; private set; }
        public List<Student> Students { get; private set; }

        public Classroom(string name, ClassroomType type)
        {
            Id = ++_id;
            Name = name;
            Type = type;
            Students = new List<Student>();
        }

        public void AddStudent(Student student)
        {
            int limit;
            if (Type == ClassroomType.Backend) limit = 20;
            else limit = 15;
            if (Students.Count >= limit)
            {
                throw new Exception("Sinif limiti dolub.");
            }
            Students.Add(student);
        }

        public Student FindStudentById(int id)
        {
            if(Students.Find(s => s.Id == id)==null) throw new StudentNotFountException("Bele bir id ile telebe tapilmadi!");

            return Students.Find(s => s.Id == id);
        }

        public bool DeleteStudent(int id)
        {
            var student = Students.Find(s => s.Id == id);
            if (student != null)
            {
                Students.Remove(student);
                return true;
            }
            throw new StudentNotFountException("Silmek ucun bele bir id ile telebe tapilmadi!");
        }
    }
}
