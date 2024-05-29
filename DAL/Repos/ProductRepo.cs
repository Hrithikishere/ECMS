using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class ProductRepo : Repo, IRepo<Product, int, bool>
    {
        public bool Create(Product obj)
        {
            try
            {
                db.Products.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating product: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var productToDelete = Read(id);
                if (productToDelete == null)
                {
                    Console.WriteLine($"Product with Id {id} not found.");
                    return false;
                }

                db.Products.Remove(productToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting product: {ex.Message}");
                return false;
            }
        }

        public List<Product> Read()
        {
            try
            {
                return db.Products.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading products: {ex.Message}");
                return new List<Product>();
            }
        }

        public Product Read(int id)
        {
            try
            {
                return db.Products.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading product with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(Product obj)
        {
            try
            {
                var existingProduct = Read(obj.Id);
                if (existingProduct == null)
                {
                    Console.WriteLine($"Product with Id {obj.Id} not found.");
                    return false;
                }
                existingProduct.Name = obj.Name;
                existingProduct.Description = obj.Description;
                existingProduct.Specification = obj.Specification;
                existingProduct.CategoryId = obj.CategoryId;
                existingProduct.ModifiedTime = obj.ModifiedTime;
                existingProduct.Price = obj.Price;
                existingProduct.ImagePath = obj.ImagePath;
                existingProduct.Stock = obj.Stock;
                //db.Entry(existingProduct).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating product: {ex.Message}");
                return false;
            }
        }

    }
}
