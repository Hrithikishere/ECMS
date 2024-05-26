using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class InventoryLogRepo : Repo, IRepo<InventoryLog, int, bool>
    {
        public bool Create(InventoryLog obj)
        {
            try
            {
                db.InventoryLogs.Add(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred in Database while creating inventory log: {ex.Message}");
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var logToDelete = Read(id);
                if (logToDelete == null) return false;

                db.InventoryLogs.Remove(logToDelete);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while deleting inventory log: {ex.Message}");
                return false;
            }
        }

        public List<InventoryLog> Read()
        {
            try
            {
                return db.InventoryLogs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading inventory logs: {ex.Message}");
                return new List<InventoryLog>();
            }
        }

        public InventoryLog Read(int id)
        {
            try
            {
                return db.InventoryLogs.Find(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while reading inventory log with Id {id}: {ex.Message}");
                return null;
            }
        }

        public bool Update(InventoryLog obj)
        {
            try
            {
                var existingLog = Read(obj.Id);
                if (existingLog == null)
                {
                    Console.WriteLine($"Inventory log with Id {obj.Id} does not exist.");
                    return false;
                }

                db.Entry(existingLog).CurrentValues.SetValues(obj);
                return db.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while updating inventory log: {ex.Message}");
                return false;
            }
        }


    }
}
