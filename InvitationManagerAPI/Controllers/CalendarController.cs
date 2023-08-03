using InvitationManagerAPI.Data;
using InvitationManagerAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace InvitationManagerAPI.Controllers
{

    [Authorize(Roles = "Registered,Admin")]
    public class CalendarController : Controller
    {
        public CalendarController(DataContext invitationManagerDbContext)
        {
            _invitationManagerDbContext = invitationManagerDbContext;
        }

        private readonly DataContext _invitationManagerDbContext;

        [HttpGet]
        [Route("events")]
        public async Task<IActionResult> GetAllEvents()
        {
            var events = await _invitationManagerDbContext.Bookings.ToListAsync();

            return Ok(events);
        }

        [HttpPost]
        [Route("registerevent")]
        public async Task<IActionResult> AddEvent([FromBody] CalendarBookings eventRequest)
        {
            eventRequest.Id = Guid.NewGuid();

            await _invitationManagerDbContext.Bookings.AddAsync(eventRequest);
            await _invitationManagerDbContext.SaveChangesAsync();

            return Ok(eventRequest);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetEvent([FromRoute] Guid id)
        {
            var _event = await _invitationManagerDbContext.Bookings.FirstOrDefaultAsync(x => x.Id == id);

            if (_event == null)
            {
                return NotFound();
            }

            return Ok(_event);
        }

        [HttpPost]
        [Route("update-event")]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid id, CalendarBookings updateCalendar)
        {
            var events = await _invitationManagerDbContext.Bookings.FindAsync(id);

            if (events == null)
            {
                return NotFound();
            }

            events.Title = updateCalendar.Title;
            events.Description = updateCalendar.Description;
            events.Start_Date = updateCalendar.Start_Date;
            events.End_Date = updateCalendar.End_Date;
            events.AllDay = updateCalendar.AllDay;

            return Ok(events);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid id)
        {
            var events = await _invitationManagerDbContext.Bookings.FindAsync(id);

            if (events == null)
            {
                return NotFound(events);
            }

            _invitationManagerDbContext.Bookings.Remove(events);
            await _invitationManagerDbContext.SaveChangesAsync();
            return Ok(events);
        }

    }
}

