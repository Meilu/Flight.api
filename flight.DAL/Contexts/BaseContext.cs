using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;


namespace flight.DAL.Contexts
{
    public class BaseContext : DbContext, IBaseContext
    {
        public BaseContext(string connection) : base(connection)
        {
            
        }
    }
}