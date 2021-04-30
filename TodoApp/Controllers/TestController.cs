using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace TodoApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TestController : BaseApiController
    {
        public TestController(IComponentContext icocontext): base(icocontext)
        {
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult> GetAsync()
        {
            try
            {
                return Ok("OK");
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
