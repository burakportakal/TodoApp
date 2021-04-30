using System.Collections.Generic;

namespace TodoApp.Models.BusinessModels
{
    public class AddCategoryResponse : ResponseBase
    {
        public Category Category { get; set; }
    }
    public class DeleteCategoryResponse : ResponseBase
    {
    }
    public class GetCategoryResponse : ResponseBase
    {
        public Category CategoryObj { get; set; }

        public IEnumerable<Category> Categories { get; set; }
        public bool IsResponseTypeList { get; set; }
    }

    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Todo> TodoList { get; set; }
    }
    public class UpdateCategoryResponse : ResponseBase
    {
        public Category Category { get; set; }
    }
}
