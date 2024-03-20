using JWT_Authentication_Authorization.Model;

namespace JWT_Authentication_Authorization.Interface
{
    public interface IAuthService
    {
        User Adduser(User user);
        string Login (LoginRequest loginRequest);
        Role AddRole(Role role);
        bool AssignRoleUser (AddUser addUser);

    }
}
