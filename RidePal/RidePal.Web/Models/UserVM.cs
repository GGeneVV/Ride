namespace RidePal.Web.Models
{
    public class UserVM
    {
        public string UserName { get; set; }
        public virtual string Email { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Image { get; set; }
    }
}
