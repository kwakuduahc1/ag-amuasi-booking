using AgAmuasiBooking.Context;
using AgAmuasiBooking.Models;
using AgAmuasiBooking.Models.BookingVm;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace AgAmuasiBooking.Controllers
{
    [Route("api/[controller]/")]
    [ApiController]
    [EnableCors("bStudioApps")]
    [Authorize(Policy = "Manager")]
    [Authorize(Policy = "Administration")]
    public class BookingsController(DbContextOptions<ApplicationDbContext> dbContextOptions, CancellationToken token) : ControllerBase
    {
        private readonly ApplicationDbContext db = new(dbContextOptions);

        [HttpGet("MyBookings")]
        public async Task<IActionResult> MyBookings()
        {
            var user = User.Identity?.Name;
            if (string.IsNullOrEmpty(user))
                return Unauthorized(new { Message = "Invalid credentials" });
            
            using var con = db.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync(token);
            
            const string qry =
                """
                SELECT bookingsid, bookingdate, title, purpose, dates, guests, isreviewed, isapproved, haspaid, days, bookingservicesid, servicecostsid, "cost"
                FROM fn_user_bookings(@user)
                """;
            var bookings = await con.QueryAsync<UserBookingDto>(qry, new { user });
           
            return Ok(bookings);
        }

        [HttpPost]
        public async Task<IActionResult> Booking([FromBody] AddBookingDto booking)
        {
            var book = new Bookings
            {
                BookingDate = DateTime.UtcNow,
                Dates = booking.Dates,
                Days = (short)booking.Dates.Length,
                Guests = booking.Guests,
                Reviewer = "",
                Title = booking.Title,
                UserName = User.Identity.Name,
                Purpose = booking.Purpose,
                IsReviewed = false,
                HasPaid = false,
                AmountPaid = 0,
                Deleted = false,
                IsApproved = false,
                PaymentDate = null,
                ReviewedDate = null,
                BookingServices = []
            };
            for (int i = 0; i < booking.Services.Length; i++)
                book.BookingServices.Add(new BookingServices
                {
                    ServiceCostsID = i,
                    BookingsID = book.BookingsID
                });
            db.Bookings.Add(book);
            await db.SaveChangesAsync(token);
            return Ok(book.BookingsID);
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditBookingDto dto)
        {
            var bk = await db.Bookings.FindAsync(dto.BookingsID, token);
            if (bk is null)
                return NotFound(new { Message = "Booking not found" });
            db.Entry(bk).State = EntityState.Deleted;
            var book = new Bookings
            {
                BookingDate = DateTime.UtcNow,
                Dates = dto.Dates,
                Days = (short)dto.Dates.Length,
                Guests = dto.Guests,
                Reviewer = "",
                Title = dto.Title,
                UserName = User.Identity.Name,
                Purpose = dto.Purpose,
                IsReviewed = false,
                HasPaid = false,
                AmountPaid = 0,
                Deleted = false,
                IsApproved = false,
                PaymentDate = null,
                ReviewedDate = null,
                BookingServices = []
            };
            for (int i = 0; i < dto.Services.Length; i++)
                book.BookingServices.Add(new BookingServices
                {
                    ServiceCostsID = i,
                    BookingsID = book.BookingsID
                });
            db.Bookings.Add(book);
            await db.SaveChangesAsync(token);
            return Ok(book.BookingsID);
        }

        [HttpPut("Review")]
        public async Task<IActionResult> Review([FromBody] Guid id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking == null)
                return NotFound(new { Message = "Invalid booking" });
            booking.ReviewedDate = DateTime.UtcNow;
            booking.IsReviewed = true;
            booking.Reviewer = User.Identity.Name;

            db.Entry(booking).State = EntityState.Modified;
            await db.SaveChangesAsync(token);
            return Accepted();
        }


        [HttpPut("Cancel")]
        public async Task<IActionResult> Review([FromBody] int id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking == null)
                return NotFound(new { Message = "Invalid booking" });
            booking.DateCancelled = DateTime.UtcNow;
            booking.IsCancelled = true;
            db.Entry(booking).State = EntityState.Modified;
            await db.SaveChangesAsync(token);
            return Accepted();
        }


        [HttpPut("Approve")]
        public async Task<IActionResult> Approve([FromBody] Guid id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking == null)
                return NotFound(new { Message = "Invalid booking" });
            booking.DateApproved = DateTime.UtcNow;
            booking.IsApproved = true;
            booking.Approver = User.Identity!.Name;

            db.Entry(booking).State = EntityState.Modified;
            await db.SaveChangesAsync(token);
            return Accepted();
        }


        [HttpPut("Payment")]
        public async Task<IActionResult> Payment([FromBody] Guid id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking == null)
                return NotFound(new { Message = "Invalid booking" });
            booking.PaymentDate = DateTime.UtcNow;
            booking.HasPaid = true;
            booking.Receiver = User.Identity!.Name;

            db.Entry(booking).State = EntityState.Modified;
            await db.SaveChangesAsync(token);
            return Accepted();
        }

        [HttpPost("Close")]
        public async Task<IActionResult> Close([FromBody] Guid[] events)
        {
            const string qry = """
                SELECT bookingsid
                FROM bookings
                WHERE bookingsid = ANY(@events)
                """;
            using var con = db.Database.GetDbConnection();
            if (con.State != ConnectionState.Open)
                await con.OpenAsync(token);
            var evs = await con.QueryAsync<Guid>(qry, new { events });
            if (evs == null || !evs.Any())
                return NotFound(new { Messag = "No events are due" });
            const string closeQry =
                """
                    UPDATE bookings
                    SET isclosed = true
                    WHERE bookings = ANY(@evs)
                """;
            var res = await con.ExecuteAsync(closeQry, new { evs });
            return Accepted();
        }

        [HttpDelete("{id:required:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var booking = await db.Bookings.FindAsync(id, token);
            if (booking is null)
                return BadRequest(new { Message = "Booking not found" });
            db.Entry(booking).State = EntityState.Deleted;
            await db.SaveChangesAsync(token);
            return Ok();
        }
    }

    public record AddBookingDto(

         [Required, StringLength(50, MinimumLength = 3)] string Title,

         [StringLength(200, MinimumLength = 5)] string? Purpose,

         [Required] DateTime[] Dates,

         [Required] int[] Services,

         [Required, Range(1, 18)] short Guests
        );

    public record EditBookingDto(

        [Required] Guid BookingsID,

         [Required, StringLength(50, MinimumLength = 3)] string Title,

         [StringLength(200, MinimumLength = 5)] string? Purpose,

         [Required] DateTime[] Dates,

         [Required] int[] Services,

         [Required, Range(1, 18)] short Guests
        );
}