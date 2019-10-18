using Microsoft.AspNetCore.Mvc;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Helpers;
using System.Linq;

namespace TodoApi.Controllers
{
    [Route("/token")]
    public class TokenController : Controller
    {
        private IRepository<User> repository;
        private IAuthenticationHelper authenticationHelper;

        public TokenController(IRepository<User> repos, IAuthenticationHelper authService)
        {
            repository = repos;
            authenticationHelper = authService;
        }


        [HttpPost]
        public IActionResult Login([FromBody]LoginInputModel model)
        {
            var user = repository.GetAll().FirstOrDefault(u => u.Username == model.Username);

            // check if username exists
            if (user == null)
                return Unauthorized();

            // check if password is correct
            if (!model.Password.Equals(user.Password))
                return Unauthorized();

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = authenticationHelper.GenerateToken(user)
            });
        }

    }
}
