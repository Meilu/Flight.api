using System.Collections.Generic;
using System.Linq;

using flight.DAL.Repositories;
using flight.library.Attributes;


namespace flight.DAL.Library
{
    [UnityIoCSingletonLifetimed]
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<string, IRepository> _repositories;

        // unit of work class is responsible for creating the repository and then dispossing it when no longer needed.
        public UnitOfWork()
        {
            _repositories = new Dictionary<string, IRepository>();
        }

        /// <summary>
        /// Register a repository with the unit of work
        /// </summary>
        /// <param name="repository">A repository derived from irepository</param>
        public void Register (IRepository repository)
        {
            _repositories.Add(repository.GetType().Name, repository);
        }

        public void SaveChanges()
        {
            _repositories.ToList().ForEach(x => x.Value.Submit());
        }
    }

}