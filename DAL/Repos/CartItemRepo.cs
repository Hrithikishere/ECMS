using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CartItemRepo : Repo, IRepo<CartItem, int, bool>
    {
        public bool Create(CartItem obj)
        {
            db.CartItems.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Read(id);
            db.CartItems.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<CartItem> Read()
        {
            return db.CartItems.ToList();
        }

        public CartItem Read(int id)
        {
            var data = db.CartItems.Find(id);
            return data;
        }

        public bool Update(CartItem obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}
