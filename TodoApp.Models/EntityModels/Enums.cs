using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Models.EntityModels
{
    public enum TaskStatus
    {
        Undefined,
        Todo,
        InProgress,
        Completed
    }

    public enum TaskPriority
    {
        Undefined,
        P1,
        P2,
        P3
    }
}
