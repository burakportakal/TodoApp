using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.TodoCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.TodoCommandHandlers
{
    public class GetTodoCommandHandler : CommandHandler<GetTodoCommand, GetTodoResponse>
    {
        private readonly TodoDBContext _context;
        public GetTodoCommandHandler(TodoDBContext context)
        {
            _context = context;
        }
        protected override Task<GetTodoResponse> ProcessCommand(GetTodoCommand command)
        {
            var response = new GetTodoResponse();

            var request = command.Data;
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == request.UserName);
            if(request.TaskId != 0)
            {
                var todo = _context.AcTasks.FirstOrDefault(e => e.TaskId == request.TaskId && e.User == user && e.IsDeleted == false);
                if (todo != null)
                {
                    response.TodoObj = new Todo
                    {
                        TaskId = todo.TaskId,
                        Name = todo.Name,
                        CreateDate = todo.CreateDate,
                        TaskPriority = (Models.EntityModels.TaskPriority)todo.TaskPriorityId.Value,
                        Status = (Models.EntityModels.TaskStatus)todo.Status.Value,
                        RootTaskId = todo.RootTaskId != null ? todo.RootTaskId.Value : 0
                    };
                }
            }
            else
            {
                response.TodoList = _context.AcTasks.Where(e => e.User == user && e.IsDeleted == false).Select(e => new Todo 
                { 
                    TaskId =e.TaskId,
                    Name = e.Name,
                    CreateDate = e.CreateDate,
                    TaskPriority =(Models.EntityModels.TaskPriority)e.TaskPriorityId.Value,
                    Status = (Models.EntityModels.TaskStatus)e.Status.Value,
                    RootTaskId = e.RootTaskId != null ? e.RootTaskId.Value : 0
                }).ToList();
                response.IsResponseTypeList = true;
            }
            return Task.FromResult(response);
        }
    }
}
