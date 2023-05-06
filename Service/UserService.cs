using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.User;
using static SportCenter.WebAPI.Converters.UserConverter;

namespace SportCenter.WebAPI.Service;

public class UserService : IUserService
{    
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public List<UserDomain> Get()
    {
        List<UserDb> usersDb = _userRepository.Get();
        List<UserDomain> usersDomain = new List<UserDomain>();
        
        foreach (var userDb in usersDb)
            if (userDb.isDeleted == false)
            {
                usersDomain.Add(ConvertFromDb(userDb));
            }
        
        return usersDomain;
    }

    public bool Create(UserView user)
    {
        if (string.IsNullOrWhiteSpace(user.username) 
            || string.IsNullOrWhiteSpace(user.password) 
            || string.IsNullOrWhiteSpace(user.email ) 
            || string.IsNullOrWhiteSpace(user.phone ) 
            || string.IsNullOrWhiteSpace(user.name ))
        {
            return false;
        }

        var userDomain = ConvertToDomain(user);
        return _userRepository.Create(ConvertToDb(userDomain));
    }

    public UserDomain GetByEmail(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            return ConvertFromDb(_userRepository.GetByEmail(email));
        }
        return null;
    }

    public bool Delete(string email)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            return _userRepository.Delete(email);
        }
        return false;
    }

    public UserDomain Update(string email, UserView user)
    {
        if (!string.IsNullOrWhiteSpace(email))
        {
            var userDomain = ConvertToDomain(user);
            return ConvertFromDb(
                _userRepository
                    .Update(
                        (string)email,
                        ConvertToDb(userDomain)));
        }
        return null;
    }
}