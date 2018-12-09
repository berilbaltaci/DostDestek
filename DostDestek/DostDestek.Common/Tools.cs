using DostDestek.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DostDestek.Common
{
    public class Tools
    {
        public static HayvanDestekEntities db = null;
        public static HayvanDestekEntities GetConnection()
        {
            if (db == null)
            {
                db = new HayvanDestekEntities();
            }
            return db;
        }
    }
}
