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
            try
            {
                Order order = new Order
                {
                    CustomerId = obj.CustomerId,
                    OrderDate = obj.OrderDate,
                    TotalAmount = obj.TotalAmount,
                    Status = obj.Status,
                    ShippingAddress = obj.ShippingAddress,
                    BillingAddress = obj.BillingAddress
                };

                db.Orders.Add(order);
                db.SaveChanges();

                foreach (var item in obj.OrderItems)
                {
                    item.OrderId = order.Id;
                    db.OrderItems.Add(item);
                }

                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating order: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var ex = Read(id);
                if (ex == null) return false;

                db.Orders.Remove(ex);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting order: {ex.Message}");
                return false;
            }
        }

        public List<Order> Read()
        {
            try
            {
                return db.Orders.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading orders: {ex.Message}");
                return new List<Order>();
            }
        }

        public Order Read(int id)
        {
            try
            {
                return db.Orders.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading order with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(Order obj)
        {
            try
            {
                var existingOrder = db.Orders.Include("OrderItems").FirstOrDefault(o => o.Id == obj.Id);
                if (existingOrder == null)
                {
                    Console.WriteLine($"Order with Id {obj.Id} does not exist.");
                    return false;
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

                if (existingOrder.Status == "Delivered")
                {
                    foreach (var existingItem in existingOrderItems)
                    {
                        var products = db.Products.Find(existingItem.ProductId);
                        products.Stock = products.Stock - existingItem.Quantity;
                        db.SaveChanges();
                    }
                }

                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating order: {ex.Message}");
                return false;
            }
        }


    }
}
