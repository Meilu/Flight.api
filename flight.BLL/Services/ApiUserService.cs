using System.Linq;
using flight.DAL.Entities;
using flight.DAL.Library;
using flight.DAL.Repositories.User;
using flight.library.Attributes;
using flight.Library.Extensions;
using flight.Library.Helpers;


namespace flight.BLL.Services
{
    [UnityIoCTransientLifetimed]
    public class ApiUserService : BaseService, IApiUserService
    {
        private readonly IUserRepository _userRepository; 

        public ApiUserService(IUserRepository userRepository, IUnitOfWork uow) : base(uow)
        {
            _userRepository = userRepository;
        }

        public IQueryable<ApiUserEntity> GetUsers()
        {
            return _userRepository.GetUsers();
        }

        public IQueryable<ApiUserEntity> GetUser(int id)
        {
            return _userRepository.GetUser(id);
        }

        public IQueryable<ApiUserEntity> GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }


        public UserCreateResult RegisterUser(ApiUserEntity user)
        {
            if (user == null)
                return UserCreateResult.Failed;

            // check if the username doesnt exist already
            if (_userRepository.UserNameExists(user.Username))
                return UserCreateResult.NameAlreadyExists;

            // create password for this user.
            user.Salt = PasswordHash.CreateSalt(user.Username, NumberExtension.GetRandomUniqueCodeWithLength(10));
            user.Password = PasswordHash.HashPassword(user.Salt, user.Password);

            // add the user.
            _userRepository.Add(user);

            // save changes.
            _uow.SaveChanges();

            return UserCreateResult.Success;
        }


        public UserLoginResult LoginUser(string userName, string password)
        {
            // retrieve user from database
            var user = _userRepository.GetUser(userName).FirstOrDefault();

            if (user == null)
                return UserLoginResult.UserNotFound;

            var passwordHash = PasswordHash.HashPassword(user.Salt, password);

            // check if password matches with database.
            if (passwordHash != user.Password)
                return UserLoginResult.IncorrectPassword;

            return UserLoginResult.Success;
        }

    }
    public enum UserCreateResult
    {
        UserIsNull,
        NameAlreadyExists,
        Success,
        Failed
    }

    public enum UserLoginResult
    {
        UserNotFound,
        IncorrectPassword,
        Banned,
        Suspended,
        InvalidDomain,
        Success

    }
}