using Database.Interface;
using MediaApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MediaApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        private readonly IAppDbContext context;
        public BuggyController(IAppDbContext context)
        {
            this.context = context;
        }

        [HttpGet("auth")]
        [Authorize]
        public ActionResult<string> GetSecret()
        {
            return "secret text";
        }

        [HttpGet("not-found")]
        public ActionResult<AppUser> GetNotFound()
        {
            var thing =  context.Users.Find(-1);
            if(thing == null)
            {
                return NotFound();
            }

            return Ok(thing);
        }

        [HttpGet("server-error")]
        public ActionResult<string> GetServerError()
        {
            var thing = context.Users.Find(-1);
            var thingToReturn = thing.ToString();

            return thingToReturn;

        }

        [HttpGet("bad-request")]
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("not a good request");
        }
    }
}
