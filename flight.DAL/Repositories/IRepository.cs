using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace flight.DAL.Repositories
{
    public interface IRepository : IDisposable
    {
        void Submit();
    }
    public interface IRepository<T> : IRepository where T : class { }
}