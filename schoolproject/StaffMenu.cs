using Microsoft.Data.SqlClient;
using schoolproject.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
                Console.WriteLine("* Show employees, please press [1] and 'Enter'\n* Get student info, please press [2] and 'Enter'\n* To see students grades, please press [3] and 'Enter'\n* See all courses, Please press [4] and 'Enter'\n* Stored procedure [5]\n* Transaction [6]\n* Exit Program? Press [7] and 'Enter'");
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
                        GetAllStudents();
                        MenuStaff();
                        break;
                    case "3":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu3 = new AdminMenu();
                        Grade();
                        MenuStaff();
                        break;
                    case "4":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu4 = new AdminMenu();
                        Course();
                        MenuStaff();
                        break;
                    case "5":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu staffmenu5 = new AdminMenu();
                        SPStud();
                        MenuStaff();
                        break;
                    case "6":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        StaffMenu staffMenu = new StaffMenu();
                        TranGrade();
                        MenuStaff();
                        break;
                    case "7":
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

            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT FirstName, LastName, YearOfEmployment, Title FROM Employee\r\n" +
            "JOIN Title ON TitleId = FK_TitleId\r\n" +
            "ORDER BY FirstName", sqlCon);

            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);

            Console.WriteLine("{0, -2} | {1, -16} | {2, -25} | {3, -15}", "First name", "Last name", "Year of employment", "Title");
            Console.WriteLine(new string('-', 65));
            foreach (DataRow dr in dtbl.Rows)
            {
                Console.WriteLine("{0, -10} | {1, -16} | {2, -25} | {3, -15} ", dr["FirstName"], dr["LastName"], dr["YearOfEmployment"], dr["Title"]);
            }
            Console.WriteLine(new string('-', 65));
            Console.WriteLine();
            Console.WriteLine("To see menu again, press 'Enter'");
            Console.ReadKey();
            sqlCon.Close();
        }
        public static void GetAllStudents()
        {
            Console.WriteLine("Here you can see a list of all students");
            Console.WriteLine();

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
                        "ORDER BY SetDate", sqlCon);

            DataTable grade = new DataTable();
            sqlda.Fill(grade);

            Console.WriteLine("{0, -2} | {1, -12} | {2, -10} | {3, -20} | {4, -22}", "First name", "Last name", "Grade info", "Date", "Course");
            Console.WriteLine(new string('-', 65));
            foreach (DataRow gr in grade.Rows)
            {
                Console.WriteLine("{0, -10} | {1, -12} | {2, -10} | {3, -20} | {4, -22} ", gr["FirstName"], gr["LastName"], gr["GradeInfo"], gr["SetDate"], gr["CourseInfo"]);
            }
            Console.WriteLine();
            Console.WriteLine("To see menu again, press 'Enter'");
            Console.ReadKey(); 
            sqlCon.Close();
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

        public static void SPStud()
        {
            Console.Clear();

            Console.WriteLine("Enter studentId: ");
            var studId = Console.ReadLine();
            string constring = "Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true";
            using (SqlConnection sqlCon = new SqlConnection(constring)) 
            {
                Console.Clear();
                SqlCommand cmd = new SqlCommand("StudentInfo", sqlCon);
                //SqlDataAdapter sqlda = new SqlDataAdapter("StudentInfo", sqlCon);
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCon.Open();
                cmd.Parameters.AddWithValue("@Id", studId);
                SqlDataReader sdr = cmd.ExecuteReader();

                while (sdr.Read())
                {
                    Console.WriteLine($"{sdr["StudentId"]} {sdr["FirstName"]} {sdr["LastName"]} {sdr["PersonalNumber"]} {sdr["ClassName"]}");
                }
                Console.WriteLine();
                Console.WriteLine("To see menu again, press 'Enter'");
                Console.ReadKey();
                sdr.Close();

            }   
        }
        public static void TranGrade()
        {
            //User input student Id
            Console.Clear();
            Console.WriteLine("Please enter student: ");

            using (var context = new SchoolDbContext())
            {
                var myStudent = from s in context.Students
                                select s;
                foreach (var student in myStudent)
                {
                    Console.WriteLine($"{student.StudentId}, {student.FirstName} {student.LastName}");
                }
            }

            int studentId = int.Parse(Console.ReadLine());
            List<string> course = new List<string>();
            //User input course Id
            Console.WriteLine("Please enter course: ");

            using (var context = new SchoolDbContext())
            {
                var myCourse = from c in context.Courses
                               join g in context.Grades on c.CourseId equals g.FkCourseId
                               where g.GradeInfo.Equals(null) && g.FkStudentId == studentId
                               select c;

                foreach (var grade in myCourse)
                {
                    Console.WriteLine($"{grade.CourseId}, {grade.CourseInfo}");
                    course.Add(grade.CourseInfo);
                }
            }
            int courseId = int.Parse(Console.ReadLine());
            //user input grade
            Console.WriteLine("Enter grade value 1-5 where 1 is lowest grade and 5 highest: ");
            int gradeInput = int.Parse(Console.ReadLine());

            string constring = (@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");
            using (SqlConnection con = new SqlConnection(constring)) 
            { 
                string code = $"Update Grade " +
                            $"Set SetDate = GETDATE(), GradeInfo = {gradeInput} " +
                            $"WHERE FK_StudentId = {studentId} AND FK_CourseId = {courseId} ";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                SqlTransaction tran = con.BeginTransaction();
                cmd.Transaction = tran;
                try
                {
                    cmd.CommandText = code;
                    var returnRows = cmd.ExecuteNonQuery();
                    tran.Commit();
                    Console.WriteLine(returnRows + "Correct");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    try
                    {
                        tran.Rollback();
                    }
                    catch (Exception exRollBack)
                    {
                        Console.WriteLine(exRollBack.Message);
                    }
                    
                }
                tran.Dispose();
                con.Close();
            }
        }
    }
}
