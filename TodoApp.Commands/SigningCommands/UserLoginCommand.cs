using TodoApp.Models.BusinessModels;

namespace TodoApp.Commands.SigningCommands
{
    public class UserLoginCommand : Command<UserLoginRequest, UserLoginResponse>, ICommand
    {
    }
}
