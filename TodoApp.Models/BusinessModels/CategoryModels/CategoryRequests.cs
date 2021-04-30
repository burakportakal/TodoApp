using TodoApp.Models.EntityModels;

namespace TodoApp.Models.BusinessModels
{
    public class AddCategoryRequest : RequestBase
    {
        public string Category { get; set; }
        public string UserName { get; set; }
    }
    public class DeleteCategoryRequest : RequestBase
    {
        public int CategoryId { get; set; }
        public string UserName { get; set; }
    }
    public class GetCategoryRequest : RequestBase
    {
        public string UserName { get; set; }
        public int CategoryId { get; set; }
    }
    public class UpdateCategoryRequest : RequestBase
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public string UserName { get; set; }

    }
}
