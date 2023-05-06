using SportCenter.WebAPI.Models.Booking;

namespace SportCenter.WebAPI.Interfaces;

public interface IBookingRepository
{
    List<BookingDb> Get();
    bool Create(BookingDb entity);
    BookingDb Update(int id,BookingDb entity);
    bool Delete(int id);
}