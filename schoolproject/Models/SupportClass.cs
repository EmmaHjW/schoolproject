using System;
using System.Collections.Generic;

namespace schoolproject.Models
{
    public partial class SupportClass
    {
        public SupportClass()
        {
            Students = new HashSet<Student>();
        }

        public int SupportClassId { get; set; }
        public int? FkStudentId { get; set; }
        public int? FkEmployeeId { get; set; }

        public virtual Employee? FkEmployee { get; set; }
        public virtual Student? FkStudent { get; set; }
        public virtual ICollection<Student> Students { get; set; }
    }
}
