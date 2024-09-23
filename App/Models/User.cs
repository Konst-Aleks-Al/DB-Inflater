using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models {

    [Table("users")]
    public class User {

        [Column("id")]
        public Guid Id { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("phone_number")]
        public string? Phone { get; set; }

        [Column("password_hash")]
        public string? PasswordHash { get; set; }

        [Column("password_salt")]
        public string? PasswordSalt { get; set; }

        [Column("full_name")]
        public string? FullName { get; set; }

        [Column("balance")]
        public double? Balance { get; set; }

        public List<Order>? Orders { get; set; }

        public User() { }
    }
}
