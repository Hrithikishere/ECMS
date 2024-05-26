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
            try
            {
                db.CartItems.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating cart item: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cartItemToDelete = Read(id);
                if (cartItemToDelete == null) return false;

                db.CartItems.Remove(cartItemToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting cart item: {ex.Message}");
                return false;
            }
        }

        public List<CartItem> Read()
        {
            try
            {
                return db.CartItems.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading cart items: {ex.Message}");
                return new List<CartItem>();
            }
        }

        public CartItem Read(int id)
        {
            try
            {
                return db.CartItems.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading cart item with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(CartItem obj)
        {
            try
            {
                var existingCartItem = Read(obj.Id);
                if (existingCartItem == null)
                {
                    Console.WriteLine($"Cart item with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingCartItem).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating cart item: {ex.Message}");
                return false;
            }
        }


    }
}
