using System.ComponentModel.DataAnnotations;

namespace BTL_SchoolManager.Models.Account
{
    public class Account
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}
