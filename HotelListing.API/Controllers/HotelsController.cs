using HotelListing.API.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private static List<Hotel> hotels = new List<Hotel>
        {
            new Hotel { Id = 1, Name = "Hotel One", Address = "123 Main St", Rating = 4.5 },
            new Hotel { Id = 2, Name = "Hotel Two", Address = "456 Elm St", Rating = 4.0 },
            new Hotel { Id = 3, Name = "Hotel Three", Address = "789 Oak St", Rating = 3.5 }
        };
        // GET: api/<HotelsController>
        [HttpGet]
        public ActionResult<IEnumerable<Hotel>> Get()
        {
            return Ok(hotels);
        }

        // GET api/<HotelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Hotel> Get(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            return Ok(hotel);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public ActionResult<Hotel> Post([FromBody] Hotel hotel)
        {
            var existinghotel = hotels.FirstOrDefault(h => h.Id == hotel.Id);
            if (existinghotel != null) return BadRequest("Hotel already exists");
            
            hotels.Add(hotel);
            return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotel hotel)
        {
            var existingHotel = hotels.FirstOrDefault(h => h.Id == id);
            if (existingHotel == null)
            {
                hotels.Add(new Hotel { });
                return CreatedAtAction(nameof(Get), new { id = hotel.Id }, hotel);
            }
            else
            { 
                existingHotel.Name = hotel.Name;
                existingHotel.Address = hotel.Address;
                existingHotel.Rating = hotel.Rating;
                return NoContent();
            }
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var hotel = hotels.FirstOrDefault(h => h.Id == id);
            if(hotel == null) return NotFound();
            
            hotels.Remove(hotel);
            return NoContent();
        }
    }
}
