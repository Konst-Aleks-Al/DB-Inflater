using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models {

    [Table("orders")]
    public class Order {

        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        public User? User { get; set; }

        [Column("sum")]
        public double? Sum { get; set; }

        [Column("cheque")]
        public string? Cheque { get; set; }

        [Column("created")]
        public DateOnly Created {  get; set; }

        public List<OrderProduct>? ProductList { get; set; }

        public Order() { }
    }

}
