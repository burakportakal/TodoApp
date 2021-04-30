using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.Commands.CategoryCommands;
using TodoApp.Helpers;
using TodoApp.Models.BusinessModels;
using TodoApp.Models.Dtos;

namespace TodoApp.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseApiController
    {
        private readonly IMapper _mapper;
        public CategoryController(IComponentContext icocontext, IMapper mapper) : base(icocontext)
        {
            _mapper = mapper;
        }

        [HttpPost(nameof(AddCategory))]
        public async Task<ActionResult> AddCategory([FromBody] AddCategoryDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<AddCategoryRequest>(viewRequest);
            request.UserName = HttpContext.User.Identity.Name;
            var command = new AddCategoryCommand {
                Data = request
            };
            return await Go(command);
        }

        [HttpPost(nameof(DeleteCategory))]
        public async Task<ActionResult> DeleteCategory([FromBody] DeleteCategoryDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<DeleteCategoryRequest>(viewRequest);
            request.UserName = HttpContext.User.Identity.Name;
            var command = new DeleteCategoryCommand
            {
                Data = request
            };
            return await Go(command);
        }

        [HttpPost(nameof(UpdateCategory))]
        public async Task<ActionResult> UpdateCategory([FromBody] UpdateCategoryDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<UpdateCategoryRequest>(viewRequest);
            request.UserName = HttpContext.User.Identity.Name;
            var command = new UpdateCategoryCommand
            {
                Data = request
            };
            return await Go(command);
        }

        [HttpPost(nameof(GetCategory))]
        public async Task<ActionResult> GetCategory([FromBody] GetCategoryDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<GetCategoryRequest>(viewRequest);
            request.UserName = HttpContext.User.Identity.Name;
            var command = new GetCategoryCommand
            {
                Data = request
            };
            return await Go(command);
        }
    }
}
