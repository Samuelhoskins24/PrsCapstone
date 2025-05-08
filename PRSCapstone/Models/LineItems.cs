using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRSCapstone.Models
{
    public class LineItems
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("RequestId")]
        public int RequestId {  get; set; }

        public Requests? Request {  get; set; }

        [ForeignKey("ProductId")]
        public int ProductId {  get; set; }

        public Products? Product { get; set; }


        public required int Quantity {  get; set; }
    }
}
