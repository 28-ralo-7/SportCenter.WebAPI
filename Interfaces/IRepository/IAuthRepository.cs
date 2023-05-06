using SportCenter.WebAPI.Models.Auth;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Interfaces;

public interface IAuthRepository
{
    bool Register(UserDb newUser);
    UserDb Login(Auth authData);
}