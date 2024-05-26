using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CartRepo : Repo, IRepo<Cart, int, bool>
    {
        public bool Create(Cart obj)
        {
            try
            {
                db.Carts.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating cart: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cartToDelete = Read(id);
                if (cartToDelete == null) return false;

                db.Carts.Remove(cartToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting cart: {ex.Message}");
                return false;
            }
        }

        public List<Cart> Read()
        {
            try
            {
                return db.Carts.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading carts: {ex.Message}");
                return new List<Cart>();
            }
        }

        public Cart Read(int id)
        {
            try
            {
                return db.Carts.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading cart with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(Cart obj)
        {
            try
            {
                var existingCart = Read(obj.Id);
                if (existingCart == null)
                {
                    Console.WriteLine($"Cart with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingCart).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating cart: {ex.Message}");
                return false;
            }
        }


    }
}
