using MiniApp_2.Enums;
using MiniApp_2.Exceptions;
using MiniApp_2.Extensions;
using MiniApp_2.Models;
using Newtonsoft.Json;
using System.ComponentModel.Design;

namespace MiniApp_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string databaseFile = "data.json";
            List<Classroom> classrooms = LoadClassrooms(databaseFile);
            menu:
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Classroom yarat");
            Console.WriteLine("2. Student yarat");
            Console.WriteLine("3. Butun Telebeleri ekrana cixart");
            Console.WriteLine("4. Secilmis sinifdeki telebeleri ekrana cixart");
            Console.WriteLine("5. Telebe sil");
            Console.WriteLine("0. Cix");

            while (true)
            {

                Console.Write("seciminizi edin:");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        CreateClassroom(ref classrooms);
                        SaveClassrooms(classrooms, databaseFile);
                        break;
                    case 2:
                        CreateStudent(ref classrooms);
                        SaveClassrooms(classrooms, databaseFile);
                        break;
                    case 3:
                        DisplayAllStudents(classrooms);
                        break;
                    case 4:
                        DisplayClassroomStudents(classrooms);
                        break;
                    case 5:
                        DeleteStudent(ref classrooms);
                        SaveClassrooms(classrooms, databaseFile);
                        break;
                    case 6:
                        goto menu;
                    case 0:
                        SaveClassrooms(classrooms, databaseFile);
                        return;
                    default:
                        Console.WriteLine("Duzgun secim edin.");
                        break;
                }
            }
        }

        static void CreateClassroom(ref List<Classroom> classrooms)
        {
            while (true)
            {
                Console.Write("Sinif adi (2 boyuk herf ve 3 reqem): ");
                classname:
                string classroomName = Console.ReadLine();
                if (classroomName.IsValidClassroomName())
                {
                    Console.WriteLine("Sinif tipi (Backend=1, FrontEnd=2): ");
                    ClassroomType type = (ClassroomType)int.Parse(Console.ReadLine());
                    classrooms.Add(new Classroom(classroomName, type));
                    break;
                }
                else
                {
                    Console.WriteLine("Sinif adi duzgun deyil. Yeniden daxil edin:");
                    goto classname;
                }
            }
        }

        static void CreateStudent(ref List<Classroom> classrooms)
        {
            while (true)
            {
                Console.Write("Telebe adi: ");
                stName:
                string studentName = Console.ReadLine();
                if (!studentName.IsValidName())
                {
                    Console.WriteLine("Telebe adi duzgun deyil. Yeniden daxil edin:");
                    goto stName;
                }

                Console.Write("Telebe soyadi: ");
                stSurname:
                string studentSurname = Console.ReadLine();
                if (!studentSurname.IsValidSurname())
                {
                    Console.WriteLine("Telebe soyadi duzgun deyil. Yeniden cehd edin.");
                    goto stSurname;
                }

                Console.Write("Sinif Id: ");
                int classId = int.Parse(Console.ReadLine());
                try
                {
                    var classroom = classrooms.Find(c => c.Id == classId) ?? throw new ClassroomNotFoundException("Sinif tapilmadi!");
                    classroom.AddStudent(new Student(studentName, studentSurname));
                    break;
                }
                catch (ClassroomNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                }
                
            }
        }

        static void DisplayAllStudents(List<Classroom> classrooms)
        {
            

            foreach (var cls in classrooms)
            {
                foreach (var student in cls.Students)
                {
                    Console.WriteLine($"Student Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}, Classroom: {cls.Name}");
                }
            }
        }

        static void DisplayClassroomStudents(List<Classroom> classrooms)
        {
            Console.Write("Sinif Id: ");
            int classId = int.Parse(Console.ReadLine());
            var selectedClassroom = classrooms.Find(c => c.Id == classId);
            if (selectedClassroom != null)
            {
                foreach (var student in selectedClassroom.Students)
                {
                    Console.WriteLine($"Id: {student.Id}, Name: {student.Name}, Surname: {student.Surname}");
                }
            }
            else
            {
                Console.WriteLine("Sinif tapilmadi.");
            }
        }

        static void DeleteStudent(ref List<Classroom> classrooms)
        {
            Console.Write("Silinecek Telebenin Id: ");
            int studentId = int.Parse(Console.ReadLine());
            try
            {
                bool isDeleted = false;
                foreach (var cls in classrooms)
                {
                    if (cls.DeleteStudent(studentId))
                    {
                        isDeleted = true;
                        break;
                    }
                }
                if (!isDeleted) throw new StudentNotFoundException("Bele bir telebe yoxdur!");
            }
            catch (StudentNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void SaveClassrooms(List<Classroom> classrooms, string databaseFile)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", databaseFile);
            using (StreamWriter sr = new StreamWriter(path))
            {
                string json = JsonConvert.SerializeObject(classrooms);
                sr.WriteLine(json);
            }
        }

        static List<Classroom> LoadClassrooms(string databaseFile)
        {
            string path = Path.Combine(Directory.GetCurrentDirectory(), "..", "..", "..", databaseFile);
            if (File.Exists(path))
            {
                using (StreamReader streamReader = new StreamReader(path))
                {
                    string result = streamReader.ReadToEnd();
                    var classrooms = JsonConvert.DeserializeObject<List<Classroom>>(result);
                    if (classrooms == null)
                    {
                        classrooms = new List<Classroom>();
                    }
                    return classrooms;
                }
            }
            else
            {
                return new List<Classroom>();
            }
        }





    }
}