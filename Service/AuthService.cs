using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Auth;
using SportCenter.WebAPI.Models.User;
using static SportCenter.WebAPI.Converters.UserConverter;

namespace SportCenter.WebAPI.Service;

public class AuthService : IAuthService
{
    private readonly IAuthRepository _authRepository;

    public AuthService(IAuthRepository authRepository)
    {
        _authRepository = authRepository;
    }
    public bool Register(UserView newUser)
    {
        if (string.IsNullOrWhiteSpace(newUser.username ) 
            || string.IsNullOrWhiteSpace(newUser.password ) 
            || string.IsNullOrWhiteSpace(newUser.email) 
            || string.IsNullOrWhiteSpace(newUser.phone) 
            || string.IsNullOrWhiteSpace(newUser.name))
        {
            return false;
        }
        return _authRepository.Register(new UserDb(
            0,
            newUser.username,
            newUser.password,
            newUser.email,
            newUser.phone,
            newUser.name,
            false,
            newUser.role
        ));
    }

    public UserView Login(Auth authData)
    {
        return ConvertFromDomain(ConvertFromDb(_authRepository.Login(authData)));
    }
}