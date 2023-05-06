using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportCenter.WebAPI.Interfaces.IService;
using SportCenter.WebAPI.Models.Booking;
using static SportCenter.WebAPI.Converters.BookingConverter;

namespace SportCenter.WebAPI.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class BookingController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }
    
    [HttpGet("getAll")]
    public ActionResult Get()
    {
        try
        {
            List<BookingView> users = new List<BookingView>();
            foreach (var userDomain in _bookingService.Get())
            {
                users.Add(ConvertToView(userDomain));
            }
            return Ok(users);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }

    [HttpPost("create")]
    public ActionResult Create([FromBody] BookingView bookingView)
    {
        return Ok(_bookingService.Create(bookingView));
    }
    
    [HttpDelete("delete/{id}")]
    public ActionResult<bool> DeleteUser(int id)
    {
        return Ok(_bookingService.Delete(id));
    }
    
    [HttpPut("update/{id}")]
    public ActionResult UpdateUser(int id, [FromBody] BookingView bookingView)
    {
        return Ok(ConvertToView(_bookingService.Update(id, bookingView)));   
    }
    
}