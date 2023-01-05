using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeCourses = new HashSet<EmployeeCourse>();
            SupportClasses = new HashSet<SupportClass>();
        }

        public int EmployeeId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public double? Salary { get; set; }
        public DateTime? YearOfEmployment { get; set; }
        public int? FkTitleId { get; set; }

        public virtual ICollection<EmployeeCourse> EmployeeCourses { get; set; }
        public virtual ICollection<SupportClass> SupportClasses { get; set; }
    }
}
