using System;
using System.Collections.Generic;

using flight.DAL.Library;


namespace flight.BLL.Services
{
    public class BaseService
    {
        protected readonly IUnitOfWork _uow;

        public BaseService(IUnitOfWork uow)
        {
            _uow = uow;
        }
    }
}