namespace TheatreProject.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }

    public class AdminDTO //Data Transfer Object, to transfer data without the password, while still being clear (by avoiding using "object")
    {
        public int AdminId { get; set; }
        public required string UserName { get; set; }
        public required string Email { get; set; }
    }
}