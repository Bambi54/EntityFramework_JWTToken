using CW8.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace CW8.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : Controller
    {
        private readonly Cw8DBContext _context;

        public PrescriptionController(Cw8DBContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        [Authorize]

        public async Task<IActionResult> GetPrescription(string id)
        {

            var IdPerscription = Int32.Parse(id);

            if (!await _context.Prescriptions.AnyAsync(e => e.IdPrescription == IdPerscription))
            {
                return NotFound();
            }

            return Ok(await _context.Prescriptions.Where(e => e.IdDoctor == IdPerscription).Select(e => new
            {
                IdPrescription = e.IdPrescription,
                Date = e.Date,
                DueDate = e.DueDate,
                Doctor = new
                {
                    IdDoctor = e.Doctor.IdDoctor,
                    FirstName = e.Doctor.FirstName,
                    LastName = e.Doctor.LastName,
                    Email = e.Doctor.Email
                },
                Patient = new
                {
                    IdPatient = e.Patient.IdPatient,
                    FirstName = e.Patient.FirstName,
                    LastName = e.Patient.LastName,
                    BirthDate = e.Patient.Birthdate
                }


            }).ToListAsync());

        }

    }
}
