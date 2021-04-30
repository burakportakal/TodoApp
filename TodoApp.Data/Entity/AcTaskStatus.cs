using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcTaskStatus
    {
        public AcTaskStatus()
        {
            AcTasks = new HashSet<AcTask>();
        }

        public int TaskStatusId { get; set; }
        public string Status { get; set; }

        public virtual ICollection<AcTask> AcTasks { get; set; }
    }
}
