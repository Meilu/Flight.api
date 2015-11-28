using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using flight.DAL.Entities;


namespace flight.DAL.Repositories.User
{
    public interface IUserRepository : IGenericRepository<ApiUserEntity>
    {
        IQueryable<ApiUserEntity> GetUsers();
        IQueryable<ApiUserEntity> GetUser(int id);
        IQueryable<ApiUserEntity> GetUser(string username);
        bool UserNameExists(string username);

    }
}