﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IAuth<Ret, Role>
    {
        Ret Authenticate(string username, string password);

        Role UserRole(string username);
    }
}
