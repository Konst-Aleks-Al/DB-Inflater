using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models {

    [Table("order-product")]
    public class OrderProduct {

        [Column("order_id")]
        public int? OrderId { get; set; }

        [Column("product_id")]
        public int? ProductId { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("quantity")]
        public int? Quantity { get; set; }

        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
