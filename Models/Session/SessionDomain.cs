namespace SportCenter.WebAPI.Models.Session;

public class SessionDomain
{
    public DateTime session_start { get; set; }
    public DateTime session_end { get; set; }
    public Int32 session_capacity { get; set; }
    public bool isDeleted { get; set; }

    public SessionDomain(DateTime sessionStart, DateTime sessionEnd, int sessionCapacity, bool isDeleted)
    {
        session_start = sessionStart;
        session_end = sessionEnd;
        session_capacity = sessionCapacity;
        this.isDeleted = isDeleted;
    }
}