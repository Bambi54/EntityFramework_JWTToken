using CW8.Models;
using CW8.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CW8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : Controller
    {
        public readonly Cw8DBContext _context;

        public DoctorController(Cw8DBContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        [Authorize]
        public IActionResult GetDoctor(string id)
        {
            return Ok(_context.Doctors.Where(e => e.IdDoctor == Int32.Parse(id)));
        }

        [HttpPost]
        [Authorize]

        public async Task<IActionResult> PostDoctor(DoctorPOST doctorPOST)
        {

            if (await _context.Doctors.AnyAsync(e => e.IdDoctor == doctorPOST.IdDoctor)) return Conflict();


            await _context.Doctors.AddAsync(new Doctor
            {
                IdDoctor = doctorPOST.IdDoctor,
                FirstName = doctorPOST.FirstName,
                LastName = doctorPOST.LastName,
                Email = doctorPOST.Email
            });

            await _context.SaveChangesAsync();
            return Created("", "");
            
        }

        [HttpPut("{id}")]
        [Authorize]

        public async Task<IActionResult> UpdateDoctor(string id, DoctorPUT doctorPUT)
        {

            var doc = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == Int32.Parse(id));

            if (doc == null)
            {
                return NotFound();
            }

            doc.FirstName = doctorPUT.FirstName;
            doc.LastName = doctorPUT.LastName;
            doc.Email = doctorPUT.Email;

            await _context.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete("{id}")]
        [Authorize]

        public async Task<IActionResult> DeleteDoctor (string id)
        {

            var doc = await _context.Doctors.FirstOrDefaultAsync(e => e.IdDoctor == Int32.Parse(id));
            if (doc == null)
            {
                return NotFound();
            }

            _context.Doctors.Remove(doc);
            await _context.SaveChangesAsync();
            return Ok();

        }


    }
}
