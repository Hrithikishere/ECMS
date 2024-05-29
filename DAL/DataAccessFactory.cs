using DAL.Interfaces;
using DAL.Models;
using DAL.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessFactory
    {
        public static IRepo<Category, int, bool> CategoryData()
        {
            return new CategoryRepo();
        }
        public static IRepo<Product, int, bool> ProductData()
        {
            return new ProductRepo();
        }
        public static IRepo<InventoryLog, int, bool> InventoryLogData()
        {
            return new InventoryLogRepo();
        }
        public static IRepo<User, int, bool> UserData()
        {
            return new UserRepo();
        }
        public static IRepo<Order, int, bool> OrderData()
        {
            return new OrderRepo();
        }
        public static IRepo<OrderItem, int, bool> OrderItemData()
        {
            return new OrderItemRepo();
        }
        public static IRepo<Cart, int, bool> CartData()
        {
            return new CartRepo();
        }
        public static IRepo<CartItem, int, bool> CartItemData()
        {
            return new CartItemRepo();
        }
        public static IRepo<Token, string, Token> TokenData()
        {
            return new TokenRepo();
        }
        public static IAuth<User, string> AuthData()
        {
            return new UserRepo();
        }
    }
}
