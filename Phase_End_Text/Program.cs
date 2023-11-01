using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phase_End_Text
{
    class Teacher
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string ClassSection { get; set; }

        public override string ToString()
        {
            return $"ID: {ID}, Name: {Name}, Class and Section: {ClassSection}";
        }
    }

    class Program
    {
        static List<Teacher> teachers = new List<Teacher>();
        static string filePath = "teachers.txt";

        static void Main(string[] args)
        {
            LoadTeacherData();
            while (true)
            {
                Console.WriteLine("Teacher Data Management System");
                Console.WriteLine("1. Add Teacher");
                Console.WriteLine("2. View Teachers");
                Console.WriteLine("3. Update Teacher Information");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");

                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddTeacher();
                            break;
                        case 2:
                            ViewTeachers();
                            break;
                        case 3:
                            UpdateTeacher();
                            break;
                        case 4:
                            SaveTeacherData();
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid option.");
                }
            }
        }

        static void LoadTeacherData()
        {
            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (string line in lines)
                {
                    string[] data = line.Split(',');
                    if (data.Length == 3)
                    {
                        Teacher teacher = new Teacher
                        {
                            ID = int.Parse(data[0]),
                            Name = data[1],
                            ClassSection = data[2]
                        };
                        teachers.Add(teacher);
                    }
                }
            }
        }

        static void SaveTeacherData()
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (Teacher teacher in teachers)
                {
                    writer.WriteLine($"{teacher.ID},{teacher.Name},{teacher.ClassSection}");
                }
            }
        }

        static void AddTeacher()
        {
            Console.WriteLine("Enter Teacher Details:");
            Console.Write("ID: ");
            int id = int.Parse(Console.ReadLine());
            Console.Write("Name: ");
            string name = Console.ReadLine();
            Console.Write("Class and Section: ");
            string classSection = Console.ReadLine();

            Teacher newTeacher = new Teacher
            {
                ID = id,
                Name = name,
                ClassSection = classSection
            };

            teachers.Add(newTeacher);
            Console.WriteLine("Teacher added successfully!");
        }

        static void ViewTeachers()
        {
            if (teachers.Count == 0)
            {
                Console.WriteLine("No teachers found.");
            }
            else
            {
                Console.WriteLine("List of Teachers:");
                foreach (Teacher teacher in teachers)
                {
                    Console.WriteLine(teacher);
                }
            }
        }

        static void UpdateTeacher()
        {
            Console.Write("Enter the Teacher's ID to update: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                Teacher teacherToUpdate = teachers.Find(t => t.ID == id);
                if (teacherToUpdate != null)
                {
                    Console.Write("New Name: ");
                    string newName = Console.ReadLine();
                    Console.Write("New Class and Section: ");
                    string newClassSection = Console.ReadLine();

                    teacherToUpdate.Name = newName;
                    teacherToUpdate.ClassSection = newClassSection;
                    Console.WriteLine("Teacher information updated successfully!");
                }
                else
                {
                    Console.WriteLine("Teacher not found with the given ID.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid teacher ID.");
            }
        }
    }
}
