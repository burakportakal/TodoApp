using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.TodoCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.TodoCommandHandlers
{
    public class DeleteTodoCommandHandler : CommandHandler<DeleteTodoCommand, DeleteTodoResponse>
    {
        private readonly TodoDBContext _context;
        public DeleteTodoCommandHandler(TodoDBContext context)
        {
            _context = context;
        }
        protected override Task<DeleteTodoResponse> ProcessCommand(DeleteTodoCommand command)
        {
            var response = new DeleteTodoResponse();

            var entity = _context.AcTasks.FirstOrDefault(e => e.TaskId == command.Data.TodoId && e.User.UserName == command.Data.UserName);
            entity.IsDeleted = true;
            
            _context.AcTasks.Attach(entity);

            var result =_context.SaveChanges();
            if (result == 0)
                throw new System.Exception("An error occured while deleting task.");
            return Task.FromResult(response);
        }
    }
}
