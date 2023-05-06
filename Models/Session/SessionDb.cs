using System.ComponentModel.DataAnnotations;

namespace SportCenter.WebAPI.Models.Session;

public class SessionDb
{
    [Key]
    public Int32 session_id { get; set; }
    [Required]
    public DateTime session_start { get; set; }
    [Required]
    public DateTime session_end { get; set; }
    [Required]
    public Int32 session_capacity { get; set; }
    public bool isDeleted { get; set; }

    public SessionDb(int sessionId, DateTime sessionStart, DateTime sessionEnd, int sessionCapacity, bool isDeleted)
    {
        session_id = sessionId;
        session_start = sessionStart;
        session_end = sessionEnd;
        session_capacity = sessionCapacity;
        this.isDeleted = isDeleted;
    }

    public SessionDb()
    {
    }
}