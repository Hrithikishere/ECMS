using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OrderRepo : Repo, IRepo<Order, int, bool>
    {
        public bool Create(Order obj)
        {
            Order order = new Order();
            order.CustomerId = obj.CustomerId;
            order.OrderDate = obj.OrderDate;
            order.TotalAmount = obj.TotalAmount;
            order.Status = obj.Status;
            order.ShippingAddress = obj.ShippingAddress;
            order.BillingAddress = obj.BillingAddress;

            db.Orders.Add(order);
            db.SaveChanges();

            foreach (var item in obj.OrderItems)
            {
                item.OrderId = order.Id;
                db.OrderItems.Add(item);
                db.SaveChanges();
            }

            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Read(id);
            db.Orders.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<Order> Read()
        {
            return db.Orders.ToList();
        }

        public Order Read(int id)
        {
            var data = db.Orders.Find(id);
            return data;
        }

        public bool Update(Order obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}
