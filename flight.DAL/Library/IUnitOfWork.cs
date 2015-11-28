
using flight.DAL.Repositories;

namespace flight.DAL.Library
{
    public interface IUnitOfWork
    {
        void Register(IRepository repository);
        void SaveChanges();
    }
}