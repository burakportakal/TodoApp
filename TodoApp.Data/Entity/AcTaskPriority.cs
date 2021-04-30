using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcTaskPriority
    {
        public AcTaskPriority()
        {
            AcTasks = new HashSet<AcTask>();
        }

        public int TaskPriorityId { get; set; }
        public string Priority { get; set; }

        public virtual ICollection<AcTask> AcTasks { get; set; }
    }
}
