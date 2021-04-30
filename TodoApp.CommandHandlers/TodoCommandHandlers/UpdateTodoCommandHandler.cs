using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.TodoCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.TodoCommandHandlers
{
    public class UpdateTodoCommandHandler : CommandHandler<UpdateTodoCommand, UpdateTodoResponse>
    {
        private readonly TodoDBContext _context;
        private readonly IMapper _mapper;
        public UpdateTodoCommandHandler(TodoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        protected override Task<UpdateTodoResponse> ProcessCommand(UpdateTodoCommand command)
        {
            var response = new UpdateTodoResponse();
            var request = command.Data;
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == request.UserName);
            var task = _context.AcTasks.FirstOrDefault(e => e.TaskId == request.TaskId);

            if (task != null)
            {
                var category = _context.AcCategories.FirstOrDefault(e => e.UserId == user.UserId && e.CategoryId == request.CategoryId && e.IsDeleted == false);

                if (category != null)
                {
                    task.Name = request.Name;
                    task.Category = category;
                    task.RootTaskId = request.RootTaskId == 0 ? null : request.RootTaskId;
                    task.Status = (short)request.TaskStatus;
                    task.TaskPriorityId = (short)request.TaskPriority;

                    using (var scope = _context.Database.BeginTransaction())
                    {
                        try
                        {
                            _context.AcTasks.Attach(task);
                            var activity = new AcTaskActivity
                            {
                                Activity = "Task Updated",
                                ActivityDate = DateTime.Now,
                                Task = task
                            };
                            _context.Add(activity);
                            _context.SaveChanges();
                            response.Todo = _mapper.Map<Todo>(task);
                            scope.Commit();
                        }
                        catch (Exception ex)
                        {
                            scope.Rollback();
                            throw new Exception("An error occured while creating task.");
                        }
                    }
                }
                else
                {
                    throw new Exception("An error occured while creating task.");
                }
            }
            else
            {
                throw new Exception("An error occured while creating task.");
            }

            return Task.FromResult(response);
        }
    }
}
