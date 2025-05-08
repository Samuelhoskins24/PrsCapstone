using System.ComponentModel.DataAnnotations;

namespace PRSCapstone.Models
{
    public class Users
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20)]
        public required string Username { get; set; }

        [StringLength(10)]
        public required string Password { get; set; }

        [StringLength(20)]
        public required string FirstName {  get; set; }

        [StringLength(20)]
        public required string LastName { get; set; }

        [StringLength(12)]
        public required string PhoneNumber {  get; set; }

        [StringLength(75)]
        public required string Email { get; set; }

        public bool? Reviewer { get; set; }

        public bool? Admin {  get; set; }

    }
}
