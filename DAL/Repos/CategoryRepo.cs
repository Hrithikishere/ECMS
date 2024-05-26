using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class CategoryRepo : Repo, IRepo<Category, int, bool>
    {
        public bool Create(Category obj)
        {
            try
            {
                db.Categories.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating category: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var categoryToDelete = Read(id);
                if (categoryToDelete == null) return false;

                db.Categories.Remove(categoryToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting category: {ex.Message}");
                return false;
            }
        }

        public List<Category> Read()
        {
            try
            {
                return db.Categories.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading categories: {ex.Message}");
                return new List<Category>();
            }
        }

        public Category Read(int id)
        {
            try
            {
                return db.Categories.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading category with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(Category obj)
        {
            try
            {
                var existingCategory = Read(obj.Id);
                if (existingCategory == null)
                {
                    Console.WriteLine($"Category with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingCategory).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating category: {ex.Message}");
                return false;
            }
        }


    }
}
