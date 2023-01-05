using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class EmployeeCourse
    {
        public int EmployeeCourseId { get; set; }
        public int? FkCourseId { get; set; }
        public int? FkEmployeeId { get; set; }

        public virtual Course? FkCourse { get; set; }
        public virtual Employee? FkEmployee { get; set; }
    }
}
