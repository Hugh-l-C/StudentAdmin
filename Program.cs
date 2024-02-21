using System;
using System.Collections.Generic;

namespace StudentAdmin
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            DatabaseManager dbm = new DatabaseManager("Students.sqlite");

            Student john = new Student();
            john.ID = 1;
            john.Name = "John Doe";
            john.ContactDetails = "07504981815";
            john.Nickname = "Dead Eye";
            john.HeightCm = 179;
            dbm.SaveStudent(john);

            Student phill = new Student();
            phill.ID = 2;
            phill.Name = "Phill Yaboots";
            phill.ContactDetails = "0723463421007";
            phill.Nickname = "Wellies";
            phill.HeightCm = 168;
            dbm.SaveStudent(phill);

            Student alice = new Student();
            alice.ID = 4;
            alice.Name = "Alice Whothehellis";
            alice.ContactDetails = "0123463421007";
            alice.Nickname = "Anon";
            alice.HeightCm = 158;
            dbm.SaveStudent(alice);

            Student dave = new Student();
            dave.ID = 3;
            dave.Name = "Dave Late";
            dave.ContactDetails = "0123463421007";
            dave.Nickname = "Better late than never";
            dave.HeightCm = 183;
            dbm.SaveStudent(dave);

            Student hugh = new Student();
            hugh.ID = 4;
            hugh.Name = "Hugh Chadwick";
            hugh.ContactDetails = "0123463434344";
            hugh.Nickname = "Leci Lecturer";
            hugh.HeightCm = 180;
            dbm.SaveStudent(hugh);

            Student test1 = dbm.GetStudent(1);
            Console.WriteLine($"Student name = {test1.Name} aka {test1.Nickname} height = {test1.HeightCm}");
            Student test2 = dbm.GetStudent(2);
            Console.WriteLine($"Student name = {test2.Name} aka {test2.Nickname} height = {test2.HeightCm}");
            Student test3 = dbm.GetStudent(3);
            Console.WriteLine($"Student name = {test3.Name} aka {test3.Nickname} height = {test3.HeightCm}");
            Student test4 = dbm.GetStudent(4);
            Console.WriteLine($"Student name = {test4.Name} aka {test4.Nickname} height = {test4.HeightCm}");
            List<Student> allStudents = dbm.GetAllStudents();

            Console.WriteLine("Loop through all students");
            for(int i = 0;i < allStudents.Count; i++)
            {
                Console.WriteLine($"Student name = {allStudents[i].Name} aka {allStudents[i].Nickname} height = {allStudents[i].HeightCm}");
            }

            Console.WriteLine("Loop through all students again");
            foreach (Student thisStudent in allStudents)
            {
                Console.WriteLine($"Student name = {thisStudent.Name} aka {thisStudent.Nickname} height = {thisStudent.HeightCm}");
            }
        }
    }
}

