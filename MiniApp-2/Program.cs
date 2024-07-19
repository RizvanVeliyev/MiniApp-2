namespace MiniApp_2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Classroom> classrooms = new List<Classroom>();
            int studentIdCounter = 1;
            int classroomIdCounter = 1;

            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Classroom yarat");
                Console.WriteLine("2. Student yarat");
                Console.WriteLine("3. Butun Telebeleri ekrana cixart");
                Console.WriteLine("4. Secilmis sinifdeki telebeleri ekrana cixart");
                Console.WriteLine("5. Telebe sil");
                Console.WriteLine("0. Cix");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        break;
                }
            }
    }
}