using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Data.Entity;
using TodoApp.Models.BusinessModels;

namespace TodoApp.CommandHandlers.CategoryCommandHandlers
{
    public class UpdateCategoryCommandHandler : CommandHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        private readonly TodoDBContext _context;
        private readonly IMapper _mapper;
        public UpdateCategoryCommandHandler(TodoDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        protected override Task<UpdateCategoryResponse> ProcessCommand(UpdateCategoryCommand command)
        {
            var response = new UpdateCategoryResponse();
            var request = command.Data;
            var user = _context.AcUsers.FirstOrDefault(e => e.UserName == request.UserName);
            var category = _context.AcCategories.FirstOrDefault(e => e.CategoryId == request.CategoryId);

            if(category != null)
            {
                category.CategoryName = request.Name;
                _context.Attach(category);
                var result = _context.SaveChanges();

                if (result == 0)
                    throw new Exception("An error occured while updating category");
                else
                    response.Category = _mapper.Map<Category>(category);
            }

            return Task.FromResult(response);
        }
    }
}
