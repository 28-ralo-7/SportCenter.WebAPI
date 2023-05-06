using SportCenter.WebAPI.Models.Booking;

namespace SportCenter.WebAPI.Interfaces.IService;

public interface IBookingService
{
    List<BookingDomain> Get();
    bool Create(BookingView bookingView);
    bool Delete(int id);
    BookingDomain Update(int id, BookingView bookingView);
}