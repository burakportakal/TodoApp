using TodoApp.Models.BusinessModels;

namespace TodoApp.Commands.SigningCommands
{
    public class RegisterUserCommand: Command<RegisterUserRequest,RegisterUserResponse>, ICommand
    {
    }
}
