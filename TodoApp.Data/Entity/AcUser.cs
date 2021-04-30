using System;
using System.Collections.Generic;

#nullable disable

namespace TodoApp.Data.Entity
{
    public partial class AcUser
    {
        public AcUser()
        {
            AcCategories = new HashSet<AcCategory>();
            AcTasks = new HashSet<AcTask>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime CreateDate { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Salt { get; set; }

        public virtual ICollection<AcCategory> AcCategories { get; set; }
        public virtual ICollection<AcTask> AcTasks { get; set; }
    }
}
