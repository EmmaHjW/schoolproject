using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class Course
    {
        public Course()
        {
            EmployeeCourses = new HashSet<EmployeeCourse>();
            Grades = new HashSet<Grade>();
        }

        public int CourseId { get; set; }
        public string? CourseInfo { get; set; }

        public virtual ICollection<EmployeeCourse> EmployeeCourses { get; set; }
        public virtual ICollection<Grade> Grades { get; set; }
    }
}
