using TodoApp.Models.BusinessModels;

namespace TodoApp.Commands.TodoCommands
{
    public class AddTodoCommand: Command<AddTodoRequest,AddTodoResponse>, ICommand
    {
    }
    public class DeleteTodoCommand : Command<DeleteTodoRequest, DeleteTodoResponse>, ICommand
    {
    }
    public class GetTodoCommand : Command<GetTodoRequest, GetTodoResponse>, ICommand
    {
    }
    public class UpdateTodoCommand : Command<UpdateTodoRequest, UpdateTodoResponse>, ICommand
    {
    }
}
