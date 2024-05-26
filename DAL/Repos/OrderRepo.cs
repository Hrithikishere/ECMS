using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
            var existingOrder = db.Orders.Include("OrderItems").FirstOrDefault(o => o.Id == obj.Id);
            if (existingOrder == null)
            {
                throw new Exception($"Order with Id {obj.Id} does not exist.");
            }

            existingOrder.CustomerId = obj.CustomerId;
            existingOrder.OrderDate = obj.OrderDate;
            existingOrder.TotalAmount = obj.TotalAmount;
            existingOrder.Status = obj.Status;
            existingOrder.ShippingAddress = obj.ShippingAddress;
            existingOrder.BillingAddress = obj.BillingAddress;


            var existingOrderItems = existingOrder.OrderItems.ToList();
            var updatedOrderItems = obj.OrderItems;
 
            foreach (var item in updatedOrderItems)
            {
                var existingItem = existingOrderItems.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.ProductId = item.ProductId;
                    existingItem.Quantity = item.Quantity;
                    existingItem.UnitPrice = item.UnitPrice;
                    existingItem.TotalPrice = item.TotalPrice;
                }
                else
                {
                    OrderItem newItem = new OrderItem
                    {
                        OrderId = existingOrder.Id,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                        TotalPrice = item.TotalPrice
                    };
                    db.OrderItems.Add(newItem);
                }
            }

            foreach (var existingItem in existingOrderItems)
            {
                if (!updatedOrderItems.Any(i => i.Id == existingItem.Id))
                {
                    db.OrderItems.Remove(existingItem);
                }
            }

            return db.SaveChanges() > 0;
        }
 
    }
}
