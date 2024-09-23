using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models {

    [Table("products")]
    public class Product {

        [Column("id")]
        public int? Id { get; set; }

        [Column("name")]
        public string? Name { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("price")]
        public double? Price { get; set; }

        [Column("category")]
        public string? Category { get; set; }

        [Column("update_date")]
        public DateTime? Updated { get; set; }

        public List<OrderProduct>? OrderList { get; set; }

        public Product() { }
    }
}