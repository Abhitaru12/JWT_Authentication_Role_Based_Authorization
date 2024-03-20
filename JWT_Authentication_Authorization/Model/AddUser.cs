namespace JWT_Authentication_Authorization.Model
{
    public class AddUser
    {
        public int UserId { get; set; }
        public List<int> RoleIds { get; set; }  

    }
}
