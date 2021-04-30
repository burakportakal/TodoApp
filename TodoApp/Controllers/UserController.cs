using Autofac;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoApp.Commands;
using TodoApp.Commands.SigningCommands;
using TodoApp.Helpers;
using TodoApp.Models.BusinessModels;
using TodoApp.Models.Dtos;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseApiController
    {
        private readonly IMapper _mapper;
        public UserController(IComponentContext icocontext, IMapper mapper) : base(icocontext)
        {
            _mapper = mapper;
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<RegisterUserRequest>(viewRequest);
            var command = new RegisterUserCommand {
                Data = request
            };
            return await Go(command);
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult> Login([FromBody] UserLoginDto viewRequest)
        {
            if (!TryValidateModel(viewRequest))
            {
                return BadRequest(ValidationHelper.GetModelErrors(ModelState));
            }

            var request = this._mapper.Map<UserLoginRequest>(viewRequest);
            var command = new UserLoginCommand
            {
                Data = request
            };
            return await Go(command);
        }
    }
}
