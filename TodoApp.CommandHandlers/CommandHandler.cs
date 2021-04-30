using System;
using System.Threading.Tasks;
using TodoApp.Commands;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers
{
    public abstract class CommandHandler<TCommand, TResult> : ICommandHandler
        where TCommand : ICommand
        where TResult : ResponseBase
    {
        protected abstract Task<TResult> ProcessCommand(TCommand command);
        public virtual async Task<ResponseBase> Execute(ICommand command)
        {
            try
            {
                var operationResult =  await ProcessCommand((TCommand)command);
                operationResult.Result = new ResultModel { IsSuccess = true };
                return operationResult;
            }
            catch (Exception ex)
            {
                return new ResponseBase { Result = new ResultModel { IsSuccess = false, Error = new ErrorModel { ErrorText = ex.Message.ToString(), Exception = ex.StackTrace } } };
            }
        }
    }
}
