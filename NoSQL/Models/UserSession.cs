namespace NoSQL.Models
{
    public class UserSession
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public Cart personalCart { get; set; }
    }
}
