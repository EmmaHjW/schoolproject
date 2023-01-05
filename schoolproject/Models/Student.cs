using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
            SupportClasses = new HashSet<SupportClass>();
        }

        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PersonalNumber { get; set; }
        public int? FkClassId { get; set; }
        public int? FkSupportClassId { get; set; }

        public virtual Class? FkClass { get; set; }
        public virtual SupportClass? FkSupportClass { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
        public virtual ICollection<SupportClass> SupportClasses { get; set; }
    }
}
