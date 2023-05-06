using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Interfaces.IService;

public interface IUserService
{
    List<UserDomain> Get();
    bool Create(UserView user);
    
    UserDomain GetByEmail(string email);
    
    bool Delete(string email);
    UserDomain Update(string email, UserView userDomain);
}