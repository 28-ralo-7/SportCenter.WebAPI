namespace SportCenter.WebAPI.Models.Booking;

public class BookingView
{
    public Int32 userId { get; set; }
    public Int32 sessionId { get; set; }

    public BookingView(int userId, int sessionId)
    {
        this.userId = userId;
        this.sessionId = sessionId;
    }
}