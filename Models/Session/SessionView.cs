namespace SportCenter.WebAPI.Models.Session;

public class SessionView
{
    public DateTime sessionStart { get; set; }
    public DateTime sessionEnd { get; set; }
    public Int32 sessionCapacity { get; set; }

    public SessionView(DateTime sessionStart, DateTime sessionEnd, int sessionCapacity)
    {
        this.sessionStart = sessionStart;
        this.sessionEnd = sessionEnd;
        this.sessionCapacity = sessionCapacity;
    }
}