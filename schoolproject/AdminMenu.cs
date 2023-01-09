using Microsoft.Data.SqlClient;
using schoolproject.Data;
using schoolproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
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
                Console.WriteLine("* To add new employee, please press [1] and 'Enter'\n* To add a new student, please press [2] and 'Enter'\n* To see students grades, please press [3] and 'Enter'\n* To see each departments salary, please press [4] and 'Enter'\n* To see each departments averange salary, please press [5] and 'Enter'\n* Exit Program? Press [6] and 'Enter'");
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
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu adminmenu4 = new AdminMenu();
                        Salary();
                        MenuAdmin();
                        break;
                    case "5":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        AdminMenu adminmenu5 = new AdminMenu();
                        AvgSalary();
                        MenuAdmin();
                        break;
                    case "6":
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
        public static void Salary()
        {
            Console.Clear();

            SqlConnection sql = new SqlConnection(@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");

            Console.WriteLine("Here is a list of what each department pays out each month");
            Console.WriteLine();

            SqlDataAdapter sql1 = new SqlDataAdapter("SELECT Title, result1.Total\r\n" +
                "FROM Title,\r\n" +
                "(SELECT Employee.FK_TitleId, " +
                "SUM(Salary) Total\r\n" +
                "FROM Employee\r\n" +
                "GROUP BY FK_TitleId) AS result1\r\n" +
                "WHERE result1.FK_TitleId = Title.TitleId", sql);

            DataTable salary = new DataTable();
            sql1.Fill(salary);
            sql1.Dispose();

            foreach (DataRow sa in salary.Rows)
            {
                Console.WriteLine($"{sa["Title"]} {sa["Total"]}:-");
            }
            Console.WriteLine();
            Console.WriteLine("To see menu again, press 'Enter'");
            Console.ReadKey();
        }
        public static void AvgSalary()
        {
            Console.Clear();

            SqlConnection sql = new SqlConnection(@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");

            Console.WriteLine("Here is the averange salary at each department");
            Console.WriteLine();

            SqlDataAdapter sql2 = new SqlDataAdapter("SELECT Title, result1.Averange_Salary\r\n" +
                "FROM Title,\r\n" +
                "(SELECT Employee.FK_TitleId, AVG(Salary) Averange_Salary\r\n" +
                "FROM Employee\r\n" +
                "GROUP BY FK_TitleId) AS result1\r\n" +
                "WHERE result1.FK_TitleId = Title.TitleId", sql);

            DataTable avgSalary = new DataTable();
            sql2.Fill(avgSalary);
            sql2.Dispose();

            foreach (DataRow asa in avgSalary.Rows)
            {
                Console.WriteLine($"{asa["Title"]} {asa["Averange_Salary"]}:-");
            }

            Console.WriteLine();
            Console.WriteLine("To see menu again, press 'Enter'\"");
            Console.ReadKey();
        }
       
    }
}
