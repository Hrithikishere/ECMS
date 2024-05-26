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
            db.InventoryLogs.Add(obj);
            return db.SaveChanges() > 0;
        }

        public bool Delete(int id)
        {
            var ex = Read(id);
            db.InventoryLogs.Remove(ex);
            return db.SaveChanges() > 0;
        }

        public List<InventoryLog> Read()
        {
            return db.InventoryLogs.ToList();
        }

        public InventoryLog Read(int id)
        {
            var data = db.InventoryLogs.Find(id);
            return data;
        }

        public bool Update(InventoryLog obj)
        {
            var ex = Read(obj.Id);
            db.Entry(ex).CurrentValues.SetValues(obj);
            return db.SaveChanges() > 0;
        }
    }
}
