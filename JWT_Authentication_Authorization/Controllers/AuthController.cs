using JWT_Authentication_Authorization.Interface;
using JWT_Authentication_Authorization.Model;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_Authentication_Authorization.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
       
        [HttpPost("login")]
        public string login(LoginRequest obj)
        {
            var token = _authService.Login(obj);
            return token;
        }

        [HttpPost("assignRole")]
        public bool AssignRoleToUser([FromBody] AddUser userRole)
        {
            var addedUserRole = _authService.AssignRoleUser(userRole);
            return addedUserRole;
        }

        [HttpPost("AddUser")]
        public User AddUser([FromBody] User user)
        {
            var added = _authService.Adduser(user);
            return added;
        }

        [HttpPost("AddRole")]
        public Role AddRole([FromBody] Role role)
        {
            var addedRole = _authService.AddRole(role);
            return addedRole;
        }

    }
}
