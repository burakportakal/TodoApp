using System;
using System.Collections.Generic;
using TodoApp.Models.EntityModels;

namespace TodoApp.Models.BusinessModels
{
    public class AddTodoResponse : ResponseBase
    {
        public Todo Todo { get; set; }
    }

    public class DeleteTodoResponse : ResponseBase
    {
    }
    public class UpdateTodoResponse : ResponseBase
    {
        public Todo Todo { get; set; }
    }
    public class GetTodoResponse : ResponseBase
    {
        public Todo TodoObj { get; set; }
        public IEnumerable<Todo> TodoList { get; set; }

        public bool IsResponseTypeList { get; set; }
    }

    public class Todo
    {
        public int TaskId { get; set; }
        public string Name { get; set; }
        public TaskStatus Status { get; set; }
        public int RootTaskId { get; set; }
        public TaskPriority TaskPriority { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
