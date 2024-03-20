using JWT_Authentication_Authorization.Context;
using JWT_Authentication_Authorization.Interface;
using JWT_Authentication_Authorization.Model;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_Authentication_Authorization.Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtContext _jwtContext;
        private readonly IConfiguration _configuration;

        public AuthService(JwtContext jwtContext, IConfiguration configuration)
        {
            _jwtContext = jwtContext;
            _configuration = configuration;

        }

        public Role AddRole(Role role)
        {
            var addedRole = _jwtContext.roles.Add(role);    
            _jwtContext.SaveChanges();
            return addedRole.Entity;
        }

        public User Adduser(User user)
        {
            var addUser = _jwtContext.users.Add(user);
            _jwtContext.SaveChanges();
            return addUser.Entity;
        }

        public bool AssignRoleUser(AddUser obj)
        {
            try
            {
                var addRoles = new List<UserRole>();
                var users = _jwtContext.users.SingleOrDefault(s => s.Id == obj.UserId);
                if (users == null)
                    throw new Exception("user is not valid");
                foreach (int role in obj.RoleIds)
                {
                    var userRole = new UserRole();
                    userRole.RoleId = role;
                    userRole.UserId = users.Id;
                    addRoles.Add(userRole);
                }
                _jwtContext.userRoles.AddRange(addRoles);
                _jwtContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string Login(LoginRequest loginRequest)
        {
            if (loginRequest.UserName != null && loginRequest.Password != null)
            {
                var user = _jwtContext.users.SingleOrDefault(s => s.UserName == loginRequest.UserName);
                if(user != null)
                {
                    var claims = new List<Claim> {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim("Id", user.Id.ToString()),
                        new Claim("UserName", user.Name)
                    };
                    var userRoles = _jwtContext.userRoles.Where(u => u.Id == user.Id).ToList();
                    var roleIds = userRoles.Select(u => u.RoleId).ToList();
                    var roles = _jwtContext.roles.Where(r => roleIds.Contains(r.Id)).ToList();
                    foreach (var role in roles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, role.Name));
                    }
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn);

                    var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
                    return jwtToken;
                }
                else
                {
                    throw new Exception("user is not valid");
                }
            }
            else
            {
                throw new Exception("credentials are not valid");
            }
        }
            }
        }