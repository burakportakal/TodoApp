using System.ComponentModel.DataAnnotations;
using TodoApp.Models.EntityModels;

namespace TodoApp.Models.Dtos
{
    public class AddTodoDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public TaskStatus TaskStatus { get; set; }
        public int RootTaskId { get; set; }
        public TaskPriority TaskPriority { get; set; }
    }
    public class DeleteTodoDto
    {
        public int TodoId { get; set; }
    }
    public class GetTodoDto
    {
        public int TaskId { get; set; }
    }
    public class UpdateTodoDto
    {
        [Required]
        public int TaskId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public TaskStatus TaskStatus { get; set; }
        public int RootTaskId { get; set; }
        [Required]
        public TaskPriority TaskPriority { get; set; }
    }
}
