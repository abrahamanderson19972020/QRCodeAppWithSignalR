using Business.Abstract;
using Business.Constants;
using EntitiesLayer.DTOs.BookingDto;
using EntitiesLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SignalRAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }
        [HttpGet("getallbookings")]
        public ActionResult<List<Booking>> GetBookings()
        {
            var bookings = _bookingService.TGetListAll();
            if (bookings != null)
            {
                return Ok(bookings);
            }
            return NotFound(ErrorMessages<Booking>.NoItemFound);
        }
        [HttpGet("getbooking/{id}")]
        public ActionResult<Booking> GetBooking(int id)
        {
            var booking = _bookingService.TGetByID(id);
            if (booking != null)
            {
                return Ok(booking);
            }
            return NotFound(ErrorMessages<Booking>.NoItemFound+ "with id= " + id);
        }
        [HttpPost("addnewbooking")]
        public ActionResult AddNewBooking(CreateBookingDto createBookingDto)
        {
            Booking newBooking = new Booking()
            {
                Name = createBookingDto.Name,
                Date = DateTime.Now,    
                Description = createBookingDto.Description,
                Mail = createBookingDto.Mail,
                Phone = createBookingDto.Phone,
                PersonCount = createBookingDto.PersonCount,
            };
            _bookingService.TAdd(newBooking);
            return Ok(SucccessMessages<Booking>.ItemAdded);
        }
        [HttpPut("update")]
        public ActionResult UpdateBooking(UpdateBookingDto updateBookingDto)
        {
            var bookingToUpdate = _bookingService.TGetByID(updateBookingDto.BookingID);
            if(bookingToUpdate != null)
            {
                bookingToUpdate.Description = updateBookingDto.Description;
                bookingToUpdate.Date = updateBookingDto.Date;
                bookingToUpdate.Mail = updateBookingDto.Mail;
                bookingToUpdate.PersonCount = updateBookingDto.PersonCount;
                bookingToUpdate.Phone = updateBookingDto.Phone; 
                bookingToUpdate.Name = updateBookingDto.Name;
                _bookingService.TUpdate(bookingToUpdate);
                return Ok(SucccessMessages<Booking>.ItemUpdated);
            }
            return NotFound(ErrorMessages<Booking>.NoItemFound + " with id= " + updateBookingDto.BookingID);
        }
        [HttpDelete("delete/{id}")]
        public ActionResult DeleteBooking(int id)
        {
            var bookingToDelete = _bookingService.TGetByID(id);
            if(bookingToDelete != null)
            {
                _bookingService.TDelete(bookingToDelete);
                Ok(SucccessMessages<Booking>.ItemDeleted);
            }
            return NotFound(ErrorMessages<Booking>.NoItemFound + " with id= " + id);
        }
    }
}
