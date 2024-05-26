using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repos
{
    internal class Repo
    {
        internal ECMSContext db;

        internal Repo()
        {
            db = new ECMSContext();
        }
    }
}


//For every entity/table we need one repo to define the functions for database interaction
