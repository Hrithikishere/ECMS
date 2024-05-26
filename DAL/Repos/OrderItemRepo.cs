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
            db.OrderItems.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Read(id);
            db.OrderItems.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<OrderItem> Read()
        {
            return db.OrderItems.ToList();
        }

        public OrderItem Read(int id)
        {
            var data = db.OrderItems.Find(id);
            return data;
        }

        public bool Update(OrderItem obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}
