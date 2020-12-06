using System.Web.Http;
using Authentication.Services.Intefaces;

namespace MicroserviceAuthentication.Autenticate.Controllers
{
	public class ValidationController : ApiController
    {
        private readonly IAuthenticationService _service;
        
        public ValidationController(IAuthenticationService service)
        {
            _service = service;
		}

        // Post: api/Test
        [HttpPost]
        public bool Validate([FromBody] string token)
        {
            return _service.Validate(token);
        }
    }
}
