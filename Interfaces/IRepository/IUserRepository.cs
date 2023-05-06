using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Interfaces;

public interface IUserRepository
{    
    List<UserDb> Get();
    bool Create(UserDb entity);
    UserDb Update(string email,UserDb entity);
    bool Delete(string id);
    UserDb GetByEmail(string email);
}