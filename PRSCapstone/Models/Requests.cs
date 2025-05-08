using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices.Marshalling;

namespace PRSCapstone.Models
{
    public class Requests
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("UserID")]
        public int UserID {  get; set; }

        [StringLength(20)]
        public required string RequestNumber { get; set; }

        [StringLength(100)]
        public required string Description { get; set; }

        [StringLength(255)]
        public required string Justification {  get; set; }
        
        public required DateTime DateNeeded { get; set; }

        [StringLength(25)]
        public required string DeliveryMode {  get; set; }

        [StringLength(20)]
        public required string Status {  get; set; }

        public required decimal Total { get; set; }

        public required DateTime SubmittedDate {  get; set; }

        [StringLength(100)]
        public required string ReasonForRejection { get; set; }

        //public ICollection<LineItems> LineItemR { get; set; }
    }

}
