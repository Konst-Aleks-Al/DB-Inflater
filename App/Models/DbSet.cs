namespace App.Models {

    public class DbSet {

        public List<Order>? Orders { get; set; }
        public List<Product>? Products { get; set;}
        public List<User>? Users { get; set; }

        public DbSet() { }

        public bool IsEmpty() {
            return (Orders!.Count == 0) &&
                   (Products!.Count == 0) &&
                   (Users!.Count == 0);
        }
    }
}
