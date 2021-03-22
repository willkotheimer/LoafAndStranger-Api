using Microsoft.AspNetCore.Mvc;
using LoafAndStranger.Models;
using LoafAndStranger.Data;
using System.Linq;

namespace LoafAndStranger.Controllers
{
    [Route("api/Loaves")]
    [ApiController]
    public class LoavesController : ControllerBase
    {

        LoafRepository _repo;

        public LoavesController()
        {
            _repo = new LoafRepository();
        }

        //GET to /api/loaves
        [HttpGet]
        public IActionResult GetAllLoaves()
        {
            return Ok(_repo.GetAll());
        }
        //POST to /api/loaves
        [HttpPost]
        public IActionResult AddALoaf(Loaf loaf)
        {
            _repo.Add(loaf);
            return Created($"api/Loaves/{loaf.Id}", loaf);
        }

        //GET to /api/loaves/{id}
        //GET to /api/loaves/4
        [HttpGet("{id}")]
        public IActionResult GetByID(int id)
        {
            var loaf = _repo.Get(id);

            if (loaf == null)
            {
                return NotFound("This load id does not exist.");
            }
            return Ok(loaf);
        }

        //Idempotency => I should be able to do over and over and not change the result.

        //PUT to api/loaves/{id}/slice
        [HttpPut("{id}/slice")]
        public IActionResult SliceLoaf(int id)
        {
            var loaf = _repo.Get(id);
            loaf.Sliced = true;
            return NoContent();
        }

        [HttpDelete("{loafId}")]
        public IActionResult PurchaseLoaf(int loafId) {
            _repo.Remove(loafId);
            return Ok();

        }

    }
}
