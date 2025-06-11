using backend.Model;
using Backend.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]


    public class PeoplesController : ControllerBase
    {
        private readonly DataContext context;
        public PeoplesController(DataContext _context)
        {
            context = _context;
        }
        [HttpGet]
        public IActionResult GetPeoples()
        {
            var people = context.PeopleList!.AsQueryable();
            return Ok(people);
        }
        [HttpPost]
        public IActionResult Create([FromBody] People people) 
        { 
            var dbPeople = context.PeopleList!.Find(people.Id);
            if (dbPeople == null)
            {
                context.PeopleList?.Add(people);
                context.SaveChanges();
                return CreatedAtAction(nameof(GetPeoples), new { people.Id }, people);
            }
            return Conflict();
        }
        [HttpPut("{id}")]
        public IActionResult Update(int? id, [FromBody] People people)
        {
        var dbPeople = context.PeopleList!.AsNoTracking().FirstOrDefault(peopleInDB=>peopleInDB.Id == people.Id);
            if(id != people.Id || dbPeople == null) return NotFound();
            context.Update(people);
            context.SaveChanges();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var peopleToDelete = context.PeopleList?.Find(id);
            if (peopleToDelete == null) return NotFound();
            context.PeopleList?.Remove(peopleToDelete);
            context.SaveChanges();
            return NoContent();
        }
    }
}
