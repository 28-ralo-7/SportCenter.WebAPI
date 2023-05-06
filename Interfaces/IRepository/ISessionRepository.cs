using SportCenter.WebAPI.Models.Session;

namespace SportCenter.WebAPI.Interfaces;

public interface ISessionRepository
{
    List<SessionDb> Get();
    bool Create(SessionDb entity);
    SessionDb Update(int id,SessionDb entity);
    bool Delete(int id);

}