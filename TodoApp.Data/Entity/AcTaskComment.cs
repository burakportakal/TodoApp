using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcTaskComment
    {
        public int TaskCommentId { get; set; }
        public int? TaskId { get; set; }
        public string Comment { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual AcTask Task { get; set; }
    }
}
