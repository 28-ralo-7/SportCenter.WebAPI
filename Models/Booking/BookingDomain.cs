using SportCenter.WebAPI.Models.Session;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Models.Booking;

public class BookingDomain
{
    public Int32 userId { get; set; }
    public Int32 sessionId { get; set; }
    public bool isDeleted { get; set; }

    public BookingDomain(int userId, int sessionId, bool isDeleted)
    {
        this.userId = userId;
        this.sessionId = sessionId;
        this.isDeleted = isDeleted;
    }
}