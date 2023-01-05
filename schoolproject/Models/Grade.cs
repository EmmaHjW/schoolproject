using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class Grade
    {
        public int GradeId { get; set; }
        public int? GradeInfo { get; set; }
        public DateTime? SetDate { get; set; }
        public int? FkStudentId { get; set; }
        public int? FkCourseId { get; set; }

        public virtual Course? FkCourse { get; set; }
        public virtual Student? FkStudent { get; set; }
    }
}
