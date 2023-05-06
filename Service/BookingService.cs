using SportCenter.WebAPI.Interfaces;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Booking;
using SportCenter.WebAPI.Models.Session;
using static SportCenter.WebAPI.Converters.BookingConverter;

namespace SportCenter.WebAPI.Service;

public class BookingService : IBookingService
{
    private readonly IBookingRepository _bookingRepository;

    public BookingService(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
    }
    public List<BookingDomain> Get()
    {
        List<BookingDb> bookingDbs = _bookingRepository.Get();
        List<BookingDomain> bookingDomains = new List<BookingDomain>();
        
        foreach (var bookingDb in bookingDbs)
            if (bookingDb.isDeleted == false)
            {
                bookingDomains.Add(ConvertToDomain(bookingDb));
            }
        
        return bookingDomains;
    }

    public bool Create(BookingView bookingView)
    {
        var bookingDomain = ConvertToDomain(bookingView);
        return _bookingRepository.Create(ConvertToDb(bookingDomain));
    }

    public bool Delete(int id)
    {
        return _bookingRepository.Delete(id);
    }

    public BookingDomain Update(int id, BookingView bookingView)
    {
        var bookingDomain = ConvertToDomain(bookingView);
        return ConvertToDomain(
            _bookingRepository
                .Update(
                    (int)id,
                    ConvertToDb(bookingDomain)));
    }
}