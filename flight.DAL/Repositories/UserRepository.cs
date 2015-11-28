
using System.Collections.Generic;
using System.Linq;
using flight.DAL.Contexts;
using flight.DAL.Entities;
using flight.DAL.Library;
using flight.DAL.Repositories.User;
using flight.library.Attributes;

namespace flight.DAL.Repositories
{
    [UnityIoCTransientLifetimed]
    public class UserRepository :  GenericRepository<ApiUserEntity>, IUserRepository
    {
        public UserRepository(IUnitOfWork unitOfWork, IDomainContext context) : base(unitOfWork, context)
        {  }

        public IQueryable<ApiUserEntity> GetUsers()
        {
            return Get();
        }

        public IQueryable<ApiUserEntity> GetUser(int id)
        {
            return Get(x => x.Id == id);
        }

        public IQueryable<ApiUserEntity> GetUser(string username)
        {
            return Get(x => x.Username == username).AsQueryable();
        }

        public bool UserNameExists(string username)
        {
            return Get(x => x.Username == username).Any();
        }
    }
}