using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.CategoryCommandHandlers
{
    public class GetCategoryCommandHandler : CommandHandler<GetCategoryCommand, GetCategoryResponse>
    {
        private readonly TodoDBContext _context;
        public GetCategoryCommandHandler(TodoDBContext context)
        {
            _context = context;
        }
        protected override Task<GetCategoryResponse> ProcessCommand(GetCategoryCommand command)
        {
            var response = new GetCategoryResponse();

            var request = command.Data;
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == request.UserName);
            if (request.CategoryId != 0)
            {
                var category = _context.AcCategories.Include(e => e.AcTasks)
                    .FirstOrDefault(e => e.CategoryId == request.CategoryId && e.User == user && e.IsDeleted == false);
                if (category != null)
                {
                    response.CategoryObj = new Category
                    {
                        CategoryId = category.CategoryId,
                        Name = category.CategoryName,
                        TodoList = category.AcTasks.Where(e => e.IsDeleted == false).Select((e) =>
                        {
                            return new Todo
                            {
                                TaskId = e.TaskId,
                                CreateDate = e.CreateDate,
                                Name = e.Name,
                                RootTaskId = e.RootTaskId != null ? e.RootTaskId.Value : 0,
                                Status = (Models.EntityModels.TaskStatus)e.Status.Value,
                                TaskPriority = (Models.EntityModels.TaskPriority)e.TaskPriorityId.Value
                            };
                        })
                    };
                }
            }
            else
            {
                response.Categories = _context.AcCategories.Where(e => e.User == user && e.IsDeleted == false).Select(category => new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.CategoryName,
                    TodoList = category.AcTasks.Where(e => e.IsDeleted == false).Select(e => new Todo
                    {
                        TaskId = e.TaskId,
                        Name = e.Name,
                        CreateDate = e.CreateDate,
                        TaskPriority = (Models.EntityModels.TaskPriority)e.TaskPriorityId.Value,
                        Status = (Models.EntityModels.TaskStatus)e.Status.Value,
                        RootTaskId = e.RootTaskId != null ? e.RootTaskId.Value : 0
                    })
                }).ToList();
                response.IsResponseTypeList = true;
            }
            return Task.FromResult(response);
        }
    }
}
