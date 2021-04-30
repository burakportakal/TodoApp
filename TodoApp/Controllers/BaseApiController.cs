using System.Threading.Tasks;
using Autofac;
using Microsoft.AspNetCore.Mvc;
using TodoApp.CommandHandlers;
using TodoApp.Commands;

namespace TodoApp.Controllers
{
    public class BaseApiController : ControllerBase
    {
        private IComponentContext _icoContext;
        public BaseApiController(IComponentContext icocontext)
        {
            _icoContext = icocontext;
        }
        protected async Task<ActionResult> Go(ICommand command)
        {
            var handler = GetCommandHandler(command);
            var response = await handler.Execute(command);
            return Ok(response);
        }
        private ICommandHandler GetCommandHandler(ICommand command)
        {
            return _icoContext.ResolveNamed<ICommandHandler>(command.GetType().FullName);
        }
    }
}