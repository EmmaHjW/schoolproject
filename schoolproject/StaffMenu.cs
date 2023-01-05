using Microsoft.Data.SqlClient;
using schoolproject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Lab2
{
    public class StaffMenu
    {
        public static void MenuStaff()
        {
            int choice = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("* Show employees, please press [1] and 'Enter'\n* To add new employee, please press [2] and 'Enter'\n* To see students grades, please press [3] and 'Enter'\n* To add a new student, Please press [4] and 'Enter'\n* Exit Program? Press [5] and 'Enter'");
                var menuOptions = Console.ReadLine();
                switch (menuOptions)
                {
                    case "1":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        StaffMenu staffmenu = new StaffMenu();
                        Staff();
                        MenuStaff();
                        break;
                    case "2":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu2 = new AdminMenu();
                        Staff();
                        MenuStaff();
                        break;
                    case "3":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu3 = new AdminMenu();
                        GetAllStudents();
                        MenuStaff();
                        break;
                    case "4":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu4 = new AdminMenu();
                        Grade();
                        MenuStaff();
                        break;
                    case "5":
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
                        Console.WriteLine("Make a choice between 1-5");
                        MenuStaff();
                        break;
                }
                break;
            } while (choice != 4);
        }
        public static void Staff()
        {
            Console.Clear();
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");

            Console.WriteLine("Here is a list of all employees");
            Console.WriteLine();

            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT FirstName, LastName, YearOfEmployment, Titel FROM Employee\r\n" +
            "JOIN Titel ON TitelId = FK_TitelId\r\n" +
            "ORDER BY FirstName", sqlCon);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            Console.WriteLine("{0, -2} | {1, -16} | {2, -25} | {3, -15}", "First name", "Last name", "Year of employment", "Titel");
            Console.WriteLine(new string('-', 65));
            foreach (DataRow dr in dtbl.Rows)
            {
                Console.WriteLine("{0, -10} | {1, -16} | {2, -25} | {3, -15} ", dr["FirstName"], dr["LastName"], dr["YearOfEmployment"], dr["Titel"]);
            }
            Console.WriteLine(new string('-', 65));
            Console.WriteLine();
            Console.WriteLine("To see menu again, press 'Enter'");
            Console.ReadKey();
        }
        public static void GetAllStudents()
        {
            Console.Clear();
            using (var context = new SchoolDbContext())
            {
                var myStudents = from student in context.Students
                                 select student;
                foreach (var student in myStudents)
                {
                    Console.WriteLine(student.FirstName + " " + student.LastName + " " + student.PersonalNumber + " " + student.FkClassId);
                }
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

        public static void Course()
        {
            Console.Clear();
            using (var context = new SchoolDbContext())
            {
                var myCourses = from course in context.Courses
                                 select course;
                foreach (var course in myCourses)
                {
                    Console.WriteLine(course.CourseInfo);
                }
            }
        }
    }
}
