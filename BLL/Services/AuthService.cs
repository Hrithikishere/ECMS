using BLL.DTOs;
using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AuthService
    {
        public static TokenDTO Authenticate(string username, string password)
        {
            bool result = DataAccessFactory.AuthData().Authenticate(username, password);
            if(result)
            {
                var token = new Token();
                token.UserId = username;
                token.CreatedAt = DateTime.Now;
                token.TKey = Guid.NewGuid().ToString();
                var ret = DataAccessFactory.TokenData().Create(token);
                if(ret != null)
                {
                    TokenDTO tokenDTO = new TokenDTO();
                    tokenDTO.UserId = token.UserId;
                    tokenDTO.CreatedAt = token.CreatedAt;
                    tokenDTO.TKey = token.TKey;
                    return tokenDTO;

                }
            }

            return null;
        }

        public static bool IsTokenValid(string tkey)
        {
            var existingToken = DataAccessFactory.TokenData().Read(tkey);
            if (existingToken != null && existingToken.DeletedAt == null)
            {

                return true;
            }
            return false;
        }


        public static bool Logout(string tkey)
        {
            var existingToken = DataAccessFactory.TokenData().Read(tkey);
            existingToken.DeletedAt = DateTime.Now;
            if (DataAccessFactory.TokenData().Update(existingToken) != null)
            {
                return true;
            }
            return false;

        }

        public static bool IsUserAdmin(string username)
        {
            var user = DataAccessFactory.AuthData().UserRole(username);
            if (username=="Admin")
            {
                return true;
            }
            return false;
        }

    }
}
