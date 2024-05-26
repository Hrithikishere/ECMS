using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class OrderItemRepo : Repo, IRepo<OrderItem, int, bool>
    {
        public bool Create(OrderItem obj)
        {
            try
            {
                db.OrderItems.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating order item: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var orderItemToDelete = Read(id);
                if (orderItemToDelete == null) return false;

                db.OrderItems.Remove(orderItemToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting order item: {ex.Message}");
                return false;
            }
        }

        public List<OrderItem> Read()
        {
            try
            {
                return db.OrderItems.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading order items: {ex.Message}");
                return new List<OrderItem>();
            }
        }

        public OrderItem Read(int id)
        {
            try
            {
                return db.OrderItems.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading order item with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(OrderItem obj)
        {
            try
            {
                var existingOrderItem = Read(obj.Id);
                if (existingOrderItem == null)
                {
                    Console.WriteLine($"Order item with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingOrderItem).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating order item: {ex.Message}");
                return false;
            }
        }


    }
}
