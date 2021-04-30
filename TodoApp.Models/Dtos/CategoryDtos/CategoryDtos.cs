using System.ComponentModel.DataAnnotations;
using TodoApp.Models.EntityModels;

namespace TodoApp.Models.Dtos
{
    public class AddCategoryDto
    {
        [Required]
        public string Category { get; set; }
    }
    public class DeleteCategoryDto
    {
        public int CategoryId { get; set; }
    }
    public class GetCategoryDto
    {
        public int CategoryId { get; set; }
    }
    public class UpdateCategoryDto
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
