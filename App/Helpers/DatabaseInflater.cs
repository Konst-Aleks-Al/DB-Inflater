using App.Models;

namespace App.Helpers {

    public class DatabaseInflater(DatabaseContext databaseContext) {

        private DatabaseContext DatabaseContext { get; set; } = databaseContext;

        public void InsertData(DbSet dbSet) {

            DatabaseContext.AddRange(dbSet.Users!);
            DatabaseContext.AddRange(dbSet.Orders!);
            DatabaseContext.AddRange(dbSet.Products!);
        }

        public void SaveChanges() {
            DatabaseContext.SaveChanges();
        }
    }
}
