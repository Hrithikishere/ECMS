using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class UserRepo : Repo, IRepo<User, int, bool>, IAuth<User, string>
    {
        public User Authenticate(string username, string password)
        {
            var data = db.Users.FirstOrDefault(user=> user.Email.Equals(username) && user.Password == password);
            if (data != null)
            {
                return data;
            }
            return null;
        }

        public bool Create(User obj)
        {
            try
            {
                db.Users.Add(obj);
                db.SaveChanges();

                Cart cart = new Cart
                {
                    CustomerId = obj.Id,
                };

                db.Carts.Add(cart);
                db.SaveChanges();

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
        
        public User Read(string username)
        {
            try
            {
                return db.Users.FirstOrDefault(user => user.Email.Equals(username));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in db while reading user with username {username}: {ex.Message}");
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

        public string UserRole(string username)
        {
            var role = db.Users.FirstOrDefault(u => u.Email.Equals(username)).Role;
            return role;
        }
    }
}
