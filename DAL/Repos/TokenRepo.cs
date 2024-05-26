using DAL.Interfaces;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class TokenRepo : Repo, IRepo<Token, string, Token>
    {
        public Token Create(Token obj)
        {
            try
            {
                db.Tokens.Add(obj);
                if (db.SaveChanges() > 0) return obj;
                return null;
            }
            catch(Exception ex) {

                Console.WriteLine($"Error occurred while creating token: {ex.Message}");
                return null;
            }
        }

        public bool Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Token> Read()
        {
            throw new NotImplementedException();
        }

        public Token Read(string id)
        {
            return db.Tokens.FirstOrDefault(token => token.TKey.Equals(id));
        }

        public Token Update(Token obj)
        {
            var token = Read(obj.TKey);
            db.Entry(token).CurrentValues.SetValues(obj);
            if (db.SaveChanges() > 0) return token;
            return null;
        }
    }
}
