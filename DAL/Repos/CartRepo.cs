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
                Cart cart = new Cart
                {
                    CustomerId = obj.CustomerId
                };

                db.Carts.Add(cart);
                db.SaveChanges();

                foreach (var item in obj.CartItems)
                {
                    item.CartId = cart.Id;
                    db.CartItems.Add(item);
                }

                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating cart: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var cartToDelete = Read(id);
                if (cartToDelete == null) return false;

                // Remove all associated cart items
                foreach (var item in cartToDelete.CartItems.ToList())
                {
                    db.CartItems.Remove(item);
                }

                // Remove the cart itself
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
                var existingCart = db.Carts.Include("CartItems").FirstOrDefault(c => c.Id == obj.Id);
                if (existingCart == null)
                {
                    Console.WriteLine($"Cart with Id {obj.Id} does not exist.");
                    return false;
                }

                existingCart.CustomerId = obj.CustomerId;
                var existingCartItems = existingCart.CartItems.ToList();
                var updatedCartItems = obj.CartItems;

                foreach (var item in updatedCartItems)
                {
                    var existingItem = existingCartItems.FirstOrDefault(i => i.Id == item.Id);
                    if (existingItem != null)
                    {
                        existingItem.ProductId = item.ProductId;
                        existingItem.Quantity = item.Quantity;
                    }
                    else
                    {
                        CartItem newItem = new CartItem
                        {
                            CartId = existingCart.Id,
                            ProductId = item.ProductId,
                            Quantity = item.Quantity,
                        };
                        db.CartItems.Add(newItem);
                    }
                }

                foreach (var existingItem in existingCartItems)
                {
                    if (!updatedCartItems.Any(i => i.Id == existingItem.Id))
                    {
                        db.CartItems.Remove(existingItem);
                    }
                }

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
