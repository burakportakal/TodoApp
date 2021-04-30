using AutoMapper;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using TodoApp.Data.Entity;
using TodoApp.Helpers;
using TodoApp.Models.BusinessModels;

namespace TodoApp.Test.Helpers
{
    public static class TestHelper
    {
        public static IMapper GetMapperInstance()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            return mapper;
        }

        public static SqliteConnection GetConnection()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            using (var context = new TodoDBContext(GetMockDBOptions(connection)))
            {
                EnsureCreated(context);
            }
            return connection;
        }

        public static DbContextOptions<TodoDBContext> GetMockDBOptions(SqliteConnection connection)
        {
            var options = new DbContextOptionsBuilder<TodoDBContext>()
                   .UseSqlite(connection)
                   .Options;
            return options;
        }

        public static IOptions<AppSettings> GetAppSettings()
        {
            AppSettings appSettings = new AppSettings() { Secret = "VeryVerySecretKey++++!!!!" };
            return Options.Create(appSettings);

        }

        public static void EnsureCreated(TodoDBContext context)
        {
            if (context.Database.EnsureCreated())
            {
                var user = new AcUser
                {

                    Email = "burak@outlook.com",
                    FirstName = "Burak",
                    LastName = "Portakal",
                    Password = "F8Xk9gxUyv81JZb/CsRS8h0j+yeDYigh+xNNwYWWNfc=",//testtest
                    UserName = "burak",
                    Salt = "tOoByYVHjUQ4Ue+SWZPmEQ==",
                    CreateDate = DateTime.Now
                };
                context.AcUsers.Add(user);


                context.AcTaskStatuses.Add(new AcTaskStatus { Status = "Todo" });
                context.AcTaskStatuses.Add(new AcTaskStatus { Status = "InProgress" });
                context.AcTaskStatuses.Add(new AcTaskStatus { Status = "Completed" });

                context.AcTaskPriorities.Add(new AcTaskPriority { Priority = "P1" });
                context.AcTaskPriorities.Add(new AcTaskPriority { Priority = "P2" });
                context.AcTaskPriorities.Add(new AcTaskPriority { Priority = "P3" });

                context.AcCategories.Add(new AcCategory { CategoryName = "Project", User = user  });

                context.AcTasks.Add(new AcTask
                {
                    Name ="Test task",
                    CategoryId = 1,
                    Status = 1,
                    TaskPriorityId = 1,
                    User = user,
                    IsDeleted = false,
                    CreateDate = DateTime.Now
                });

                context.AcTasks.Add(new AcTask
                {
                    Name = "Test task 2",
                    CategoryId = 1,
                    Status = 1,
                    TaskPriorityId = 1,
                    User = user,
                    IsDeleted = false,
                    CreateDate = DateTime.Now
                });
                context.SaveChanges();
            }
        }
    }
}
