using App.Models;

namespace App.Helpers {

    public class DatabaseParser(DatabaseContext databaseContext) {

        private DatabaseContext DatabaseContext { get; set; } = databaseContext;

        public IQueryable<Product> Select(Product product) {
            return DatabaseContext.Products.Where(x => x.Name == product.Name);
        }

        public IQueryable<User> Select(User user) {
            return DatabaseContext.Users.Where(x => x.Email == user.Email);
        }

        public IQueryable<Order> Select(Order order) {
            return DatabaseContext.Orders.Where(x => x.Id == order.Id);
        }
    }
}
