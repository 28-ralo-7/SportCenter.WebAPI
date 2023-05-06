using SportCenter.WebAPI.Models.Booking;

namespace SportCenter.WebAPI.Converters;

public static class BookingConverter
{
    public static BookingDb ConvertToDb(BookingDomain domain)
    {
        return new BookingDb
        (
            0,
            domain.userId,
            domain.sessionId,
            domain.isDeleted 
        );
    }

    public static BookingDomain ConvertToDomain(BookingDb db)
    {
        return new BookingDomain
        (
            db.userId,
            db.sessionId,
            db.isDeleted 
        );
    }

    public static BookingDomain ConvertToDomain(BookingView view)
    {
        return new BookingDomain
        (
            view.userId,
            view.sessionId,
            false
        );
    }

    public static BookingView ConvertToView(BookingDomain domain)
    {
        return new BookingView(
                domain.userId,
                domain.sessionId
        );
    }
}