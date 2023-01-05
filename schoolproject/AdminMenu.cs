using Microsoft.Data.SqlClient;
using schoolproject.Data;
using schoolproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace School.Lab2
{
    public class AdminMenu  // THIS WAS MY SQL MENU FIRST
    {
        public static void MenuAdmin()
        {
            int choice = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("* To add new employee, please press [1] and 'Enter'\n* To add a new student, Please press [2] and 'Enter'\n* To see students grades, please press [3] and 'Enter'\n* Exit Program? Press [4] and 'Enter'");
                var menuOptions = Console.ReadLine();
                switch (menuOptions)
                {
                    case "1":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        AdminMenu adminmenu = new AdminMenu();
                        AddStaff(); 
                        MenuAdmin();
                        break;
                    case "2":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu adminmenu2 = new AdminMenu();
                        AddStudent(); 
                        MenuAdmin();
                        break;
                    case "3":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu adminmenu3 = new AdminMenu();
                        Grade();
                        MenuAdmin();
                        break;
                    case "4":
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("See you next time");
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Make a choice between 1-4");
                        MenuAdmin();
                        break;
                }
                break;
            } while (choice != 4);
        }
        public static void AddStaff()
        {
            Console.Clear();

            Console.WriteLine("Add new employee.\nEnter First name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter Last name");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter personal number");
            var personalNumber = Console.ReadLine();
            Console.WriteLine("Enter Title;\n[1] = Teacher\n[2] = Principal\n[3] = Administrator\n[4] = Janitor");
            int newTitle;
            while (true)
            {
                var titleId = Console.ReadLine();
                if (int.TryParse(titleId, out newTitle) && newTitle >= 1 && newTitle <= 4)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Select 1-4");
                } 
            }
            string query = "";
            if (newTitle == 1)
            {
                    query = "INSERT INTO dbo.Employee (FirstName, LastName, PersonalNumber, FK_TitleId)" +
                    "VALUES (@firstName, @lastName, @personalNumber, @newTitle)" +
                    "INSERT INTO dbo.Employee_Course (FK_EmployeeId, FK_CourseId)" +
                    "VALUES (IDENT_CURRENT ('Employee'), @newId)";

                Console.WriteLine("Enter CourseId;\n[1] = Math 1, [2] = Math 2, [3] = Math 3\n[4] = English 1, [5] = English 2, [6] = English 3\n[7] = Sience 1, [8] = Sience 2, [9] = Sience 3\n[10] = Athletics\n[0] = Principal, Administrator, Janitor");
                
                int newId;
                while (true)
                {
                    var courseId = Console.ReadLine();
                    if (int.TryParse(courseId, out newId) && newId >= 1 && newId <= 10)
                    {
                        try
                        {
                            string conString = "Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true";
                            using (SqlConnection connection = new SqlConnection(conString))
                            {
                                //open connection to base
                                connection.Open();
                                //SQL-code
                                SqlCommand cmd = new SqlCommand(query, connection);

                                cmd.Parameters.AddWithValue("@firstName", firstName);
                                cmd.Parameters.AddWithValue("@lastName", lastName);
                                cmd.Parameters.AddWithValue("@personalNumber", personalNumber);
                                cmd.Parameters.AddWithValue("@newTitle", newTitle);
                                cmd.Parameters.AddWithValue("@newId", newId);
                                SqlDataReader sdr = cmd.ExecuteReader();
                                connection.Close();

                                Console.WriteLine();
                                Console.WriteLine($"We welcome {firstName} {lastName}");
                                Console.WriteLine($"To your new school, we are looking forward to get to know you.");
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("OOPs, Somthing went wrong" + e);
                        }
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Select 1 - 10");
                    }
                }
            }
            else
            {
               var query2 = "INSERT INTO dbo.Employee (FirstName, LastName, PersonalNumber, FK_TitleId) " +
                         "VALUES (@firstName, @lastName, @personalNumber, @newTitle)";
                try
                {
                    string conString = "Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true";
                    using (SqlConnection connection = new SqlConnection(conString))
                    {
                        //open connection to base
                        connection.Open();
                        //SQL-code
                        SqlCommand cmd = new SqlCommand(query2, connection);

                        cmd.Parameters.AddWithValue("@firstName", firstName);
                        cmd.Parameters.AddWithValue("@lastName", lastName);
                        cmd.Parameters.AddWithValue("@personalNumber", personalNumber);
                        cmd.Parameters.AddWithValue("@newTitle", newTitle);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        connection.Close();

                        Console.WriteLine();
                        Console.WriteLine($"We welcome {firstName} {lastName}");
                        Console.WriteLine($"To your new school, we are looking forward to get to know you.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("OOPs, Somthing went wrong" + e);
                }
            }
        }
        public static void AddStudent()
        {
            Console.Clear();

            Console.WriteLine("Add new student.\nEnter First name");
            var firstName = Console.ReadLine();
            Console.WriteLine("Enter Last name");
            var lastName = Console.ReadLine();
            Console.WriteLine("Enter personal number");
            var personalNumber = Console.ReadLine();
            int newId;
            Console.WriteLine("Enter Class, [1]A, [2]A, [3]A");
            while (true)
            {
                var classId = Console.ReadLine();
                if (int.TryParse(classId, out newId) && newId >= 1 && newId <= 3)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Select 1 - 3");
                }
            }
            try
            {
                string conString = "Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true";
                using (SqlConnection connection = new SqlConnection(conString))
                {
                    //SQL-code
                    SqlCommand cmd = new SqlCommand("INSERT INTO Student (FirstName, LastName, PersonalNumber, FK_ClassId)" +
                        "Values(@firstname, @lastname, @personalnumber, @classId)", connection);
                    //open connection to base
                    connection.Open();
                    //set input value to sql-query
                    cmd.Parameters.AddWithValue("@firstname", firstName);
                    cmd.Parameters.AddWithValue("@lastname", lastName);
                    cmd.Parameters.AddWithValue("@personalnumber", personalNumber);
                    cmd.Parameters.AddWithValue("@classId", newId);
                    SqlDataReader sdr = cmd.ExecuteReader();

                    Console.WriteLine();
                    Console.WriteLine($"We welcome {firstName} {lastName}");
                    Console.WriteLine($"To your new school, we are looking forward to get to know you.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("OOPs, Somthing went wrong" + e);
            }
        }

        public static void Grade()
        {
                Console.Clear();

                SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");

                Console.WriteLine("Here you can see a list of all students grades in all courses");
                Console.WriteLine();

                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT FirstName, LastName, GradeInfo, SetDate, CourseInfo FROM Grade\r\n" +
                            "JOIN Student ON FK_StudentId = StudentId\r\n" +
                            "JOIN Course ON CourseId = FK_CourseId\r\n" +
                            "WHERE SetDate >= DATEADD(month, -1, GETDATE())\r\n" +
                            "ORDER BY SetDate", sqlCon);

                DataTable grade = new DataTable();
                sqlda.Fill(grade);

                foreach (DataRow gr in grade.Rows)
                {
                    Console.WriteLine($"{gr["FirstName"]} {gr["LastName"]}, {gr["GradeInfo"]}, {gr["SetDate"]}, {gr["CourseInfo"]}");
                }
                Console.WriteLine();
                Console.WriteLine("To see menu again, press 'Enter'");
                Console.ReadKey();
        }
        
        
        
        
  
        //public static void GetAllStudentsClass()
        //{
        //    Console.Clear();
        //    using (var context = new SchoolContext())
        //    {
        //        var myStudents3 = from c in context.Classes
        //                          select c;
        //        foreach (var c in myStudents3)
        //        {
        //            Console.WriteLine($"{c.ClassId}, {c.ClassName}");
        //        }
        //        Console.WriteLine("To see students in one class, press 1, 2 or 3.");
        //        bool id = true;
        //        while (id)
        //        {
        //            var classInput = Console.ReadLine();
        //            int setClassId = 0;
        //            if (Int32.TryParse(classInput, out setClassId))
        //            {
        //                id = false;
        //                var myStudents4 = from s in context.Students
        //                                  join c in context.Classes
        //                                  on s.FkClassId equals c.ClassId
        //                                  where c.ClassId == setClassId
        //                                  select new
        //                                  {
        //                                      s.FirstName,
        //                                      s.LastName,
        //                                      c.ClassName
        //                                  };
        //                foreach (var c in myStudents4)
        //                {
        //                    Console.WriteLine(c.FirstName, c.LastName, c.ClassName);
        //                }
        //            }
        //            else
        //            {
        //                Console.WriteLine("Wrong input, try id 1-3");
        //            }
        //        }
        //    }
        //}
    }
}
