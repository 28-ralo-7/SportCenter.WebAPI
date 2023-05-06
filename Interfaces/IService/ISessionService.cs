using SportCenter.WebAPI.Models.Session;

namespace SportCenter.WebAPI.Interfaces.IService;

public interface ISessionService
{
    List<SessionDomain> Get();
    bool Create(SessionView sessionView);
    bool Delete(int id);
    SessionDomain Update(int id, SessionView sessionView);
}