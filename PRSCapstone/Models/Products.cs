using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstone.Models
{
    public class Products
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("VendorId")]
        public int VendorId {  get; set; }

        [StringLength(50)]
        public required string PartNumber {  get; set; }

        [StringLength(150)]
        public required string Name {  get; set; }

        public required decimal Price { get; set; }

        [StringLength(255)]
        public string? Unit { get; set; }

        [StringLength(255)]
        public string? PhotoPath {  get; set; }

        public ICollection<LineItems> LineItemP { get; set; }

    }
}
