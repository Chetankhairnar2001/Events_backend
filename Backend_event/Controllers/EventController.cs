using Backend_event.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend_event.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {

        EventDbContext dbContext = new EventDbContext();

        [HttpGet()]
        public IActionResult GetAll(string? type = null, string? eventname = null)
        {
            List<Event> result = dbContext.Events.OrderBy(t=>t.Type).ThenBy(d => d.EventDate).ToList();

            if(type != null)
            {
                result = result.Where(e => e.Type.ToLower().Contains(type.ToLower())).ToList();
            }
            if(eventname != null)
            {
                result = result.Where(x => x.Eventname.ToLower().Contains(eventname.ToLower())).ToList();
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id) { 
            Event result = dbContext.Events.Find(id);
            if (result == null) { 
                return NoContent();
            }   
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddEvent([FromBody] Event newEvent)
        {
            newEvent.Id = 0;
            newEvent.Timestamp = DateTime.Now;
            dbContext.Events.Add(newEvent);
            dbContext.SaveChanges();
            return Created($"api/Event/{newEvent.Id}", newEvent);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEvent(int id)
        {
            Event result = dbContext.Events.Find(id);
            if (result == null)
            {
                return NotFound();
            }

            List<Favorite> favoriteList = dbContext.Favorites.Where(x => x.Eventid == id).ToList();
            dbContext.Favorites.RemoveRange(favoriteList);

            dbContext.Events.Remove(result);
            dbContext.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateEvent([FromBody] Event targetEvent, int id)
        {
            if (targetEvent.Id != id)
            {
                return BadRequest();
            }
            if (!dbContext.Events.Any(e => e.Id == id))
            {
                return NotFound();
            }
            dbContext.Events.Update(targetEvent);
            dbContext.SaveChanges();
            return NoContent();

        }

    }
}
