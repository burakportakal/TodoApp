using TodoApp.Models.BusinessModels;

namespace TodoApp.Commands.CategoryCommands
{
    public class AddCategoryCommand: Command<AddCategoryRequest, AddCategoryResponse>, ICommand
    {
    }
    public class DeleteCategoryCommand : Command<DeleteCategoryRequest, DeleteCategoryResponse>, ICommand
    {
    }
    public class GetCategoryCommand : Command<GetCategoryRequest, GetCategoryResponse>, ICommand
    {
    }
    public class UpdateCategoryCommand : Command<UpdateCategoryRequest, UpdateCategoryResponse>, ICommand
    {
    }
}
