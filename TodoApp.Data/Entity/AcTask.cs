using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcTask
    {
        public AcTask()
        {
            AcTaskActivities = new HashSet<AcTaskActivity>();
            AcTaskComments = new HashSet<AcTaskComment>();
            AcTaskLabels = new HashSet<AcTaskLabel>();
            InverseRootTask = new HashSet<AcTask>();
        }

        public int TaskId { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public int? CategoryId { get; set; }
        public int? Status { get; set; }
        public int? RootTaskId { get; set; }
        public int? TaskPriorityId { get; set; }
        public DateTime CreateDate { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AcCategory Category { get; set; }
        public virtual AcTask RootTask { get; set; }
        public virtual AcTaskStatus StatusNavigation { get; set; }
        public virtual AcTaskPriority TaskPriority { get; set; }
        public virtual AcUser User { get; set; }
        public virtual ICollection<AcTaskActivity> AcTaskActivities { get; set; }
        public virtual ICollection<AcTaskComment> AcTaskComments { get; set; }
        public virtual ICollection<AcTaskLabel> AcTaskLabels { get; set; }
        public virtual ICollection<AcTask> InverseRootTask { get; set; }
    }
}
