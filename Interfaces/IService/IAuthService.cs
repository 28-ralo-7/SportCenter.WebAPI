using SportCenter.WebAPI.Models.Auth;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Interfaces.IService;

public interface IAuthService
{
    bool Register(UserView newUser);
    UserView Login(Auth authData);
}