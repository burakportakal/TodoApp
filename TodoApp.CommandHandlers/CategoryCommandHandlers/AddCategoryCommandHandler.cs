using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.CategoryCommandHandlers
{
    public class AddCategoryCommandHandler : CommandHandler<AddCategoryCommand, AddCategoryResponse>
    {
        private readonly TodoDBContext _context;
        private readonly IMapper _mapper;
        public AddCategoryCommandHandler(TodoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        protected override Task<AddCategoryResponse> ProcessCommand(AddCategoryCommand command)
        {
            var response = new AddCategoryResponse();
            var request = command.Data;
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == request.UserName);

            var category = new AcCategory
            {
                CategoryName = request.Category,
                User = user,
                IsDeleted = false
            };

            _context.AcCategories.Add(category);
            var result  =_context.SaveChanges();
            if(result > 0)
            {
                response.Category = _mapper.Map<Category>(category);
            }
            else
            {
                throw new Exception("An error occur while adding category.");
            }


            return Task.FromResult(response);
        }
    }
}
