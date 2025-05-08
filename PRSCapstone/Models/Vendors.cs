using System.ComponentModel.DataAnnotations;

namespace PRSCapstone.Models
{
    public class Vendors
    {
        [Key]
        public int Id { get; set; }

        [StringLength(10)]
        public required string Code {  get; set; }

        [StringLength(255)]
        public required string Name { get; set; }

        [StringLength(255)]
        public required string Address {  get; set; }

        [StringLength(255)]
        public required string City { get; set; }

        [StringLength(2)]
        public required string State {  get; set; }

        [StringLength(5)]
        public required string Zip {  get; set; }

        [StringLength(12)]
        public required string PhoneNumber {  get; set; } 

        [StringLength(100)]
        public required string Email { get; set; }

        
    }
}
