
using System.Collections.Generic;
using System.Linq;
using flight.DAL.Entities;

namespace flight.BLL.Services
{
    public interface IApiUserService : IBaseService
    {

        IQueryable<ApiUserEntity> GetUsers();
        IQueryable<ApiUserEntity> GetUser(int id);
        IQueryable<ApiUserEntity> GetUser(string username);
        UserCreateResult RegisterUser(ApiUserEntity userForm);
        UserLoginResult LoginUser(string userName, string password);
    }
}