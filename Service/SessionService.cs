using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Session;
using static SportCenter.WebAPI.Converters.SessionConverter;

namespace SportCenter.WebAPI.Service;

public class SessionService : ISessionService
{
    private readonly ISessionRepository _sessionRepository;

    public SessionService(ISessionRepository sessionRepository)
    {
        _sessionRepository = sessionRepository;
    }
    public List<SessionDomain> Get()
    {
        List<SessionDb> sessionDbs = _sessionRepository.Get();
        List<SessionDomain> sessionDomains = new List<SessionDomain>();
        
        foreach (var sessionDb in sessionDbs)
            if (sessionDb.isDeleted == false)
            {
                sessionDomains.Add(ConvertToDomain(sessionDb));
            }
        
        return sessionDomains;
    }

    public bool Create(SessionView session)
    {
        if (session.sessionStart < DateTime.Now
            || session.sessionEnd < DateTime.Now 
            || session.sessionCapacity <= 0)
        {
            return false;
        }

        var sessionDomain = ConvertFromView(session);
        return _sessionRepository.Create(ConvertToDb(sessionDomain));
    }

    public bool Delete(int id)
    {
        return _sessionRepository.Delete(id);
    }

    public SessionDomain Update(int id, SessionView sessionView)
    {
        var sessionDomain = ConvertFromView(sessionView);
        return ConvertToDomain(
            _sessionRepository
                .Update(
                    (int)id,
                    ConvertToDb(sessionDomain)));
    }
}