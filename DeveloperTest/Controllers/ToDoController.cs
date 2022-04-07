using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DeveloperTest.Models;
using DeveloperTest.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperTest.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    [ApiKeyAuthAttribute]
    public class ToDoController : ControllerBase
    {
        private readonly ToDoDbContext _context;


        public ToDoController(ToDoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<Invoice>> GetInvoices()
        {
            return await _context.Inovieces.Where(n => n.IsPaid == false && n.DateDeleted == null).ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> PayInvoice(int? ID, decimal Amount)
        {
            if (ID == null)
                return NotFound("Unexpected Error Occured");


            var entity = await _context.Inovieces.FirstOrDefaultAsync(n => n.ID == ID && n.DateDeleted == null);

            if (entity == null)
                return NotFound("Invoice Could not be found");

            if (entity.IsPaid)
                return BadRequest("It is already paid");


            if (entity.Amount > Amount)
                return BadRequest("Amount Is not Enough");


            entity.IsPaid = true;
            await _context.SaveChangesAsync();

            return Ok(entity);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateInvoice(int ID, JsonPatchDocument entity)
        {
            var result = await _context.Inovieces.FirstOrDefaultAsync(n => n.ID == ID && n.DateDeleted == null);

            if (result == null)
                return NotFound();


            if (entity == null)
                return BadRequest("Invoice Can not be null");

            entity.ApplyTo(result);
            await _context.SaveChangesAsync();

            return Ok(result);
        }
    }
}