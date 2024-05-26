using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IRepo<User, int, bool>
    {
        public bool Create(User obj)
        {
            try
            {
                db.Users.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating user: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var ex = Read(id);
                if (ex == null) return false;

                db.Users.Remove(ex);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting user: {ex.Message}");
                return false;
            }
        }

        public List<User> Read()
        {
            try
            {
                return db.Users.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading users: {ex.Message}");
                return new List<User>();
            }
        }

        public User Read(int id)
        {
            try
            {
                return db.Users.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading user with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(User obj)
        {
            try
            {
                var existingUser = Read(obj.Id);
                if (existingUser == null)
                {
                    Console.WriteLine($"User with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingUser).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating user: {ex.Message}");
                return false;
            }
        }

    }
}
