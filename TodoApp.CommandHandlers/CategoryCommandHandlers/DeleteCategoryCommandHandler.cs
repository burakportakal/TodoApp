using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Commands.TodoCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.CategoryCommandHandlers
{
    public class DeleteCategoryCommandHandler : CommandHandler<DeleteCategoryCommand, DeleteCategoryResponse>
    {
        private readonly TodoDBContext _context;
        public DeleteCategoryCommandHandler(TodoDBContext context)
        {
            _context = context;
        }
        protected override Task<DeleteCategoryResponse> ProcessCommand(DeleteCategoryCommand command)
        {
            var response = new DeleteCategoryResponse();

            var entity = _context.AcCategories.FirstOrDefault(e => e.CategoryId == command.Data.CategoryId && e.User.UserName == command.Data.UserName);
            entity.IsDeleted = true;
            
            _context.AcCategories.Attach(entity);

            var result =_context.SaveChanges();
            if (result == 0)
                throw new System.Exception("An error occured while deleting task.");
            return Task.FromResult(response);
        }
    }
}
