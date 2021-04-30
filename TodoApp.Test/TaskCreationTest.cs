using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;
using TodoApp.Test.Helpers;
using Xunit;
using TodoApp.CommandHandlers.TodoCommandHandlers;
using TodoApp.Commands.TodoCommands;
using TodoApp.Models.EntityModels;

namespace TodoApp.Test
{
    public class TaskCreationTest
    {
        [Fact]
        public async Task Task_Create()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new AddTodoCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new AddTodoCommand();
                    command.Data = new AddTodoRequest
                    {
                        UserName = "burak",
                        Name = "Task test",
                        CategoryId = 1,
                        TaskPriority = TaskPriority.P1,
                        TaskStatus = Models.EntityModels.TaskStatus.Todo
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var count = context.AcTasks.Where(e => e.Name == "Task test");
                    Assert.Equal(1, count.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Task_Delete()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new DeleteTodoCommandHandler(context);
                    var command = new DeleteTodoCommand();
                    command.Data = new DeleteTodoRequest
                    {
                        TodoId = 2,
                        UserName ="burak"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var task = context.AcTasks.FirstOrDefault(e => e.TaskId == 2);
                    Assert.True(task.IsDeleted);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Task_Update()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new UpdateTodoCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new UpdateTodoCommand();
                    command.Data = new UpdateTodoRequest
                    {
                        TaskId = 1,
                        UserName = "burak",
                        Name = "Task test updated",
                        CategoryId = 1,
                        TaskPriority = TaskPriority.P1,
                        TaskStatus = Models.EntityModels.TaskStatus.Todo
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var count = context.AcTasks.Where(e => e.TaskId == 1 &&  e.Name == "Task test updated");
                    Assert.Equal(1, count.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Task_Get()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new GetTodoCommandHandler(context);
                    var command = new GetTodoCommand();
                    command.Data = new GetTodoRequest
                    {
                        TaskId = 1,
                        UserName = "burak"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                    if (result.GetType().IsAssignableTo(typeof(GetTodoResponse)))
                    {
                        var getTodoRes = (GetTodoResponse)result;
                        Assert.NotNull(getTodoRes.TodoObj);
                    }

                    command.Data = new GetTodoRequest
                    {
                        TaskId = 0,
                        UserName = "burak"
                    };
                    result = await service.Execute(command);

                    Assert.True(result.Result.IsSuccess);
                    if (result.GetType().IsAssignableTo(typeof(GetTodoResponse)))
                    {
                        var getTodoRes = (GetTodoResponse)result;
                        Assert.NotNull(getTodoRes.TodoList);
                        Assert.NotEmpty(getTodoRes.TodoList);
                    }
                }
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
