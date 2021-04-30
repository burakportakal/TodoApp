using System.Linq;
using System.Threading.Tasks;
using TodoApp.CommandHandlers.SigingCommandHandlers;
using TodoApp.Commands.SigningCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;
using TodoApp.Test.Helpers;
using Xunit;
using Microsoft.Extensions.Options;

namespace TodoApp.Test
{
    public class CommandHandlerTest
    {
        [Fact]
        public async Task Create_User()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                
                using (var context = new TodoDBContext(options))
                {
                    var service = new RegisterUserCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new RegisterUserCommand();
                    command.Data = new RegisterUserRequest
                    {
                        Email = "test email",
                        FirstName = "test firstname",
                        LastName = "test lastname",
                        Password = "testpw",
                        UserName = "testusername"

                    };
                    var result = await service.Execute(command);
                    Assert.True(result.Result.IsSuccess);
                }

                using (var context = new TodoDBContext(options))
                {
                    var count = context.AcUsers.Where(e => e.FirstName == "test firstname");
                    Assert.Equal(1, count.Count());
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Create_User_UserName_Already_Exists()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
              
                // Run the test against one instance of the context
                using (var context = new TodoDBContext(options))
                {
                    TestHelper.EnsureCreated(context);
                    var service = new RegisterUserCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new RegisterUserCommand();
                    command.Data = new RegisterUserRequest
                    {
                        Email = "test email",
                        FirstName = "test firstname",
                        LastName = "test lastname",
                        Password = "testpw",
                        UserName = "burak"

                    };
                    var result = await service.Execute(command);
                    Assert.False(result.Result.IsSuccess);
                    Assert.Equal("UserName is already in use", result.Result.Error.ErrorText);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Create_User_Email_Already_Exists()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                // Run the test against one instance of the context
                using (var context = new TodoDBContext(options))
                {
                    TestHelper.EnsureCreated(context);
                    var service = new RegisterUserCommandHandler(context, TestHelper.GetMapperInstance());
                    var command = new RegisterUserCommand();
                    command.Data = new RegisterUserRequest
                    {
                        Email = "burak@outlook.com",
                        FirstName = "test firstname",
                        LastName = "test lastname",
                        Password = "testpw",
                        UserName = "test username"

                    };
                    var result = await service.Execute(command);
                    Assert.False(result.Result.IsSuccess);
                    Assert.Equal("Email is already in use", result.Result.Error.ErrorText);
                }
            }
            finally
            {
                connection.Close();
            }
        }
        [Fact]
        public async Task Login_User()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    TestHelper.EnsureCreated(context);

                    var service = new UserLoginCommandHandler(context, TestHelper.GetAppSettings());
                    var command = new UserLoginCommand();
                    command.Data = new UserLoginRequest
                    {
                        Password = "testtest",
                        UserName = "burak"

                    };
                    var result = await service.Execute(command);
                    if (result.GetType().IsAssignableTo(typeof(UserLoginResponse)))
                    {
                        var res = (UserLoginResponse)result;

                        Assert.NotEqual<string>(res.Token, string.Empty);
                    }
                    Assert.True(result.Result.IsSuccess);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Login_User_Wrong_Password()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    TestHelper.EnsureCreated(context);

                    var service = new UserLoginCommandHandler(context, TestHelper.GetAppSettings());
                    var command = new UserLoginCommand();
                    command.Data = new UserLoginRequest
                    {
                        Password = "testtest2",
                        UserName = "burak"

                    };
                    var result = await service.Execute(command);

                    Assert.False(result.Result.IsSuccess);
                    Assert.Equal("Incorrect username or password", result.Result.Error.ErrorText);
                }
            }
            finally
            {
                connection.Close();
            }
        }

        [Fact]
        public async Task Login_User_Wrong_UserName()
        {
            var connection = TestHelper.GetConnection();
            var options = TestHelper.GetMockDBOptions(connection);
            try
            {
                using (var context = new TodoDBContext(options))
                {
                    TestHelper.EnsureCreated(context);
                    var service = new UserLoginCommandHandler(context, TestHelper.GetAppSettings());
                    var command = new UserLoginCommand();
                    command.Data = new UserLoginRequest
                    {
                        Password = "testtest",
                        UserName = "burakyok"

                    };
                    var result = await service.Execute(command);
                    Assert.False(result.Result.IsSuccess);
                    Assert.Equal("Incorrect username or password", result.Result.Error.ErrorText);
                }
            }
            finally
            {
                connection.Close();
            }
        }


    }
}
