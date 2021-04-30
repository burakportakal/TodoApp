using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcCategory
    {
        public AcCategory()
        {
            AcTasks = new HashSet<AcTask>();
        }

        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool? IsDeleted { get; set; }
        public int UserId { get; set; }

        public virtual AcUser User { get; set; }
        public virtual ICollection<AcTask> AcTasks { get; set; }
    }
}
