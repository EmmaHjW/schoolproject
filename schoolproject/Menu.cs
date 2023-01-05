using schoolproject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace School.Lab2
{
    public class Menu
    {
        public void ShowMenu()
        {
                Console.WriteLine();
                Console.WriteLine("1 * Log in as Admin\n2 * Log in as Staff\n3 * Show school information\n4 * Log out");
                var menuOptions = Console.ReadLine();
                switch (menuOptions)
                {
                    case "1": 
                        AdminMenu admin = new AdminMenu();
                        AdminMenu.MenuAdmin();
                        break;
                    case "2":
                        StaffMenu staff = new StaffMenu();
                        StaffMenu.MenuStaff();
                    
                        break;
                    case "3":
                        VisitorMenu visitor = new VisitorMenu();
                        VisitorMenu.MenuVisitor();
                    break;
                    case "4":
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("*");
                        Thread.Sleep(150);
                        Console.WriteLine("Logged out complete");
                        Environment.Exit(0);
                        break;
                    default:
                    Console.WriteLine("Make a choice between 1-3");
                    ShowMenu();
                        break;
                }
        }
    }
    
}
