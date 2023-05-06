using System.ComponentModel.DataAnnotations;
using SportCenter.WebAPI.Models.Session;
using SportCenter.WebAPI.Models.User;

namespace SportCenter.WebAPI.Models.Booking;

public class BookingDb
{
    [Key]
    public Int32 bookingId { get; set; }
    [Required]
    public Int32 userId { get; set; }
    [Required]
    public Int32 sessionId { get; set; }
    
    public bool isDeleted { get; set; }
    
    public BookingDb(int bookingId, int userId, int sessionId, bool isDeleted)
    {
        this.bookingId = bookingId;
        this.userId = userId;
        this.sessionId = sessionId;
        this.isDeleted = isDeleted;
    }

    public BookingDb()
    {
    }
}