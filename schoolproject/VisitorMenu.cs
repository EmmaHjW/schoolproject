using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using schoolproject.Data;
using schoolproject.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Runtime.Versioning;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace School.Lab2
{
    public class VisitorMenu  // THIS WAS MY EF MENU FIRST
    {
        public static void MenuVisitor()
        {
            int choice = 0;
            do
            {
                Console.WriteLine();
                Console.WriteLine("* To see all employees, please press [1] and 'Enter'\n* Show how many employees the school has, please press [2] and 'Enter'\n* To see infomation, please press[3]\n* Exit Program? Press [4] and 'Enter'");
                var menuOptions = Console.ReadLine();
                switch (menuOptions)
                {
                    case "1":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        VisitorMenu visitormenu = new VisitorMenu();
                        Staff();
                        MenuVisitor();
                        break;
                    case "2":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        VisitorMenu visitormenu2 = new VisitorMenu();
                        NumOfStaff();
                        MenuVisitor();
                        break;
                        case"3":
                        Console.WriteLine("Just wait a second, your requested data will appear soon");
                        Console.WriteLine();
                        VisitorMenu visitormenu3 = new VisitorMenu();
                        Information();
                        MenuVisitor();
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
                        Console.WriteLine("Make a choice between 1-5");
                        MenuVisitor();
                        break;
                }
                break;
            } while (choice != 5);

        }

        public static void Staff()
        {
            Console.WriteLine(new String('♥', 45));
            Console.WriteLine();
            Console.WriteLine("We are working here: ");
            Console.Clear();
            SqlConnection sqlCon = new SqlConnection(@"Data Source=LAPTOP-EQE3P3DB; Initial Catalog=School; Integrated Security=true");

            Console.WriteLine("Here is a list of all employees");
            Console.WriteLine();

            SqlDataAdapter sqlda = new SqlDataAdapter("SELECT FirstName, LastName, YearOfEmployment, Title FROM Employee\r\n" +
            "JOIN Title ON TitleId = FK_TitleId\r\n" +
            "ORDER BY FirstName", sqlCon);
            DataTable dtbl = new DataTable();
            sqlda.Fill(dtbl);
            sqlda.Dispose();

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
            Console.WriteLine(new string('♥', 45));
        }

        public static void NumOfStaff()
        {
            Console.Clear();
            using (var context = new SchoolDbContext())
            {
                var myEmployees = from title in context.Titles
                                  join e in context.Employees on title.TitleId equals e.FkTitleId
                                  select new {
                                  FName = e.FirstName,
                                  LName = e.LastName,
                                  title = title.Title1
                                  };

                foreach (var employee in myEmployees)
                {
                    Console.WriteLine(employee.FName + " " + employee.LName + " " + employee.title);
                }
            }

            var db = new SchoolDbContext();
            var count = db.Employees.Count();
            Console.WriteLine("Number of employees: " + count);
        }

        public static void Information()
        {
            Console.Clear();
            Console.WriteLine("Hi, welcome to high school SEVEN's website.\nHere are some contact information:\nPhonenumber: 555667788\nEmail: highschoolseven@seven.com\nYou are more then welcome to contact us for any information needed. We are looking forward to here from you!");
        }

    }
}




