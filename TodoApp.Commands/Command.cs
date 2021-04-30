using TodoApp.Models.BusinessModels;

namespace TodoApp.Commands
{
    public abstract class Command<TRequest,TResult> where TResult: ICommandResult 
    {
        public TRequest Data;
    }
}
