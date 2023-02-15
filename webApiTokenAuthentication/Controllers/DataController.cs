using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace webApiTokenAuthentication.Controllers
{
    public class DataController : ApiController
    {
        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/salahi")]
        public IHttpActionResult Get()
        {
            return Ok("Salahi free API");
        }

        [Authorize]
        [HttpGet]
        [Route("api/data/salahi/authenticate")]
        public IHttpActionResult GetForAuthenticate() 
        { 
            var identity = (ClaimsIdentity)User.Identity;
            return Ok(identity.Name + " is authenticated successfully");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        [Route("api/data/salahi/authorize")]
        public IHttpActionResult GetForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims.Where(c=>c.Type == ClaimTypes.Role).Select(c=>c.Value);
            return Ok(identity.Name + " is authenticated successfully" + " Role: " + string.Join(", ", roles.ToList()));
        }
    }
}
