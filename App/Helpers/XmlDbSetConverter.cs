using App.Models;
using App.Serializers;

namespace App.Helpers {

    public class XmlDbSetConverter(DatabaseParser dbParser) {

        // Свойство для вытягивания строк из БД.
        private DatabaseParser DbParser { get; set; } = dbParser;

        // Списки данных для внесения в БД.
        private List<Order> NewOrders { get; set; } = [];
        private List<Product> NewProducts { get; set; } = [];
        private List<User> NewUsers { get; set; } = [];


        /// <summary>
        /// Конвертирует данные XML файла в объекты классов данных БД.
        /// </summary>
        /// <returns> <see cref="DbSet"/> из уникальных строк данных, не представленных в БД. </returns>
        public DbSet ConvertWithUniqueCheck(XmlOrderList xmlList) {

            foreach (var xmlOrder in xmlList.OrderList!) {

                // Создаём список элементов смежной таблицы
                List<OrderProduct> orderProducts = [];

                foreach (var xmlProduct in xmlOrder.ProductList!) {

                    // Инициализируем товар с проверкой на наличие в БД
                    Product product = CreateOrSelect(xmlProduct);

                    // Инициализируем заказ-товар и связываем с товаром
                    OrderProduct orderProduct = Create(xmlOrder, xmlProduct);
                    orderProduct.Product = product;
                    LinkOrderProduct(orderProduct, product);

                    orderProducts.Add(orderProduct);
                }

                // Инициализируем пользователя с проверкой на наличие в БД
                User user = CreateOrSelect(xmlOrder.User!);

                // Инициализируем заказ с проверкой на наличие в БД
                Order order = CreateOrSelect(xmlOrder);
                order.ProductList = orderProducts;
                order.User = user;
            }

            // Связываем пользователей и заказы
            NewUsers.ForEach(user => user.Orders!.AddRange(NewOrders.Where(order => order.User == user)));

            // Возвращаем набор данных, не содержащихся в БД
            return new DbSet() { Orders = NewOrders, Products = NewProducts, Users = NewUsers };
        }


        /// <summary>
        /// Проверяет наличие элемента в БД и текущем списке новых данных для избежания дублирования.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если элемент представлен в одном из списков,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private bool CheckExistance(Product product) {

            return NewProducts.Where(x => x.Name == product.Name).Any() ||
                   DbParser.Select(product).Any();
        }


        /// <summary>
        /// Проверяет наличие элемента в БД и текущем списке новых данных для избежания дублирования.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если элемент представлен в одном из списков,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private bool CheckExistance(User user) {

            return NewUsers.Where(x => x.Email == user.Email).Any() ||
                   DbParser.Select(user).Any();
        }


        /// <summary>
        /// Проверяет наличие элемента в БД и текущем списке новых данных во избежание дублирования.
        /// </summary>
        /// <returns>
        ///     <see langword="true"/> - если элемент представлен в одном из списков,
        ///     <see langword="false"/> - в ином случае.
        /// </returns>
        private bool CheckExistance(Order order) {

            return NewOrders.Where(x => x.Id == order.Id).Any() ||
                   DbParser.Select(order).Any();
        }


        /// <summary>
        /// Возвращает уже существующий элемент во избежание дублирования.
        /// </summary>
        /// <returns> Элемент <see cref="Product"/> из БД или списка новых. </returns>
        private Product SelectExisnting(Product product) {

            if (NewProducts.Where(x => x.Name == product.Name).Any()) {

                return NewProducts.Where(x => x.Name == product.Name).Single();
            }
            return DbParser.Select(product).Single();
        }


        /// <summary>
        /// Возвращает уже существующий элемент во избежание дублирования.
        /// </summary>
        /// <returns> Элемент <see cref="User"/> из БД или списка новых. </returns>
        private User SelectExisnting(User user) {

            if (NewUsers.Where(x => x.Email == user.Email).Any()) {

                return NewUsers.Where(x => x.Email == user.Email).Single();
            }
            return DbParser.Select(user).Single();
        }


        /// <summary>
        /// Возвращает уже существующий элемент во избежание дублирования.
        /// </summary>
        /// <returns> Элемент <see cref="Order"/> из БД или списка новых. </returns>
        private Order SelectExisnting(Order order) {

            if (NewOrders.Where(x => x.Id == order.Id).Any()) {

                return NewOrders.Where(x => x.Id == order.Id).Single();
            }
            return DbParser.Select(order).Single();
        }


        /// <summary>
        /// Инициализирует элемент, возвращая уже существующий или создавая новый, если такого не существует.
        /// </summary>
        /// <returns> Ранее существующий или созданный с нуля элемент <see cref="Product"/>. </returns>
        private Product CreateOrSelect(XmlProduct xmlProduct) {

            Product product = new() {
                Name = xmlProduct.Name,
                Price = xmlProduct.Price,
                OrderList = []
            };

            if (CheckExistance(product)) {
                product = SelectExisnting(product);
            }
            else {
                NewProducts.Add(product);
            }

            return product;
        }


        /// <summary>
        /// Инициализирует элемент, возвращая уже существующий или создавая новый, если такого не существует.
        /// </summary>
        /// <returns> Ранее существующий или созданный с нуля элемент <see cref="User"/>. </returns>
        private User CreateOrSelect(XmlUser xmlUser) {

            User user = new() {
                FullName = xmlUser.FullName,
                Email = xmlUser.Email,
                Orders = []
            };

            if (CheckExistance(user)) {
                user = SelectExisnting(user);
            }
            else {
                NewUsers.Add(user);
            }

            return user;
        }


        /// <summary>
        /// Инициализирует элемент, возвращая уже существующий или создавая новый, если такого не существует.
        /// </summary>
        /// <returns> Ранее существующий или созданный с нуля элемент <see cref="Order"/>. </returns>
        private Order CreateOrSelect(XmlOrder xmlOrder) {

            Order order = new() {
                Id = xmlOrder.Id,
                Created = DateOnly.Parse(xmlOrder.Created),
                Sum = xmlOrder.Sum,
                ProductList = []
            };

            if (CheckExistance(order)) {
                order = SelectExisnting(order);
            }
            else {
                NewOrders.Add(order);
            }

            return order;
        }


        /// <summary>
        /// Инициализирует новый объект класса <see cref="OrderProduct"/> 
        /// </summary>
        /// <returns> Новый объект класса <see cref="OrderProduct"/> </returns>
        private static OrderProduct Create(XmlOrder order, XmlProduct product) {

            return new() {
                OrderId = order.Id,
                Price = product.Price,
                Quantity = product.Quantity
            };
        }


        /// <summary>
        /// Добавляет указанный <see cref="OrderProduct"/> всем связанным с ним объектами <see cref="Product"/>
        /// </summary>
        private void LinkOrderProduct(OrderProduct orderProduct, Product product) {

            foreach (var entry in NewProducts) {

                if (entry.Name == product.Name) {

                    NewProducts.Find(x => x == product)!.OrderList!.Add(orderProduct);
                }
            }
        }
    }
}
