using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;
using TodoApp.Test.Helpers;
using Xunit;
using TodoApp.CommandHandlers.CategoryCommandHandlers;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Models.EntityModels;

namespace TodoApp.Test
{
    public class CategoryCreationTest
    {
        [Fact]
        public async Task Category_Create()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new AddCategoryCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new AddCategoryCommand();
                    command.Data = new AddCategoryRequest
                    {
                        UserName = "burak",
                        Category = "Task Category"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var count = context.AcCategories.Where(e => e.CategoryName == "Task Category");
                    Assert.Equal(1, count.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Category_Delete()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new DeleteCategoryCommandHandler(context);
                    var command = new DeleteCategoryCommand();
                    command.Data = new DeleteCategoryRequest
                    {
                        CategoryId = 1,
                        UserName ="burak"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var task = context.AcCategories.FirstOrDefault(e => e.CategoryId == 1);
                    Assert.True(task.IsDeleted);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Category_Update()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new UpdateCategoryCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new UpdateCategoryCommand();
                    command.Data = new UpdateCategoryRequest
                    {
                        CategoryId = 1,
                        UserName = "burak",
                        Name = "Task test updated"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var count = context.AcCategories.Where(e => e.CategoryId == 1 &&  e.CategoryName == "Task test updated");
                    Assert.Equal(1, count.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Category_Get()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    var service = new GetCategoryCommandHandler(context);
                    var command = new GetCategoryCommand();
                    command.Data = new GetCategoryRequest
                    {
                        CategoryId = 1,
                        UserName = "burak"
                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                    if (result.GetType().IsAssignableTo(typeof(GetCategoryResponse)))
                    {
                        var getCategoryRes = (GetCategoryResponse)result;
                        Assert.NotNull(getCategoryRes.CategoryObj);
                    }

                    command.Data = new GetCategoryRequest
                    {
                        CategoryId = 0,
                        UserName = "burak"
                    };
                    result = await service.Execute(command);

                    Assert.True(result.Result.IsSuccess);
                    if (result.GetType().IsAssignableTo(typeof(GetCategoryResponse)))
                    {
                        var getCategoryRes = (GetCategoryResponse)result;
                        Assert.NotNull(getCategoryRes.Categories);
                        Assert.NotEmpty(getCategoryRes.Categories);
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
