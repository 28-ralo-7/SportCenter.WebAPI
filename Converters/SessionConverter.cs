using SportCenter.WebAPI.Models.Session;

namespace SportCenter.WebAPI.Converters;

public static class SessionConverter
{
    public static SessionDomain ConvertToDomain(SessionDb session)
    {
        return new SessionDomain(
            sessionStart: session.session_start,
            sessionEnd: session.session_end,
            sessionCapacity: session.session_capacity,
            isDeleted: session.isDeleted
        );
    }
    public static SessionDomain ConvertFromView(SessionView session)
    {
        return new SessionDomain(
            sessionStart: session.sessionStart,
            sessionEnd: session.sessionEnd,
            sessionCapacity: session.sessionCapacity,
            isDeleted: false
        );
    }
    public static SessionDb ConvertToDb(SessionDomain session)
    {
        return new SessionDb(
            sessionId: 0, // Необходимо для генерации нового значения ID при добавлении в базу данных
            sessionStart: session.session_start,
            sessionEnd: session.session_end,
            sessionCapacity: session.session_capacity,
            isDeleted: session.isDeleted
        );
    }

    public static SessionView ConvertToView(SessionDomain session)
    {
        return new SessionView(
            sessionStart: session.session_start,
            sessionEnd: session.session_end,
            sessionCapacity: session.session_capacity
        );
    }
}