using Backend_event.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend_event.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        EventDbContext dbContext = new EventDbContext();

        [HttpGet("{id}")]
        public IActionResult GetFavoriteById(int id)
        {
            var result = dbContext.Favorites.Where(f => f.Currentuserid == id).Include(f => f.Event).ToList();
            return Ok(result);
            /*
                var favorites = dbContext.Favorites
                                         .Where(f => f.Currentuserid == id)
                                         .Include(f => f.Event)
                                         .ToList();

                var result = favorites.Select(f => new
                {
                    f.Currentuserid,
                    Event = new
                    {
                        f.Event.Eventname,
                        f.Event.Description,
                        f.Event.Type
                    }
                }).ToList();

                return Ok(result);
            */
        }


        [HttpPost()]
        public IActionResult PostFavorite([FromBody] Favorite newFavorite)
        {
            newFavorite.Id = 0;
            dbContext.Favorites.Add(newFavorite);
            dbContext.SaveChanges();
            return Created($"api/Favorite/{newFavorite.Id}", newFavorite);
        }


    }
}
