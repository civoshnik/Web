using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Web
{
    [Route("api/[controller]")]
    [ApiController]
    public class UslugaController : ControllerBase
    {
        private readonly AppDbContext _context;
        public UslugaController (AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUslugi(int? id)
        {
            var list = await _context.Uslugi.ToListAsync();
            return Ok(list);
        }
        [HttpPost]
        public async Task<IActionResult> PostUslug([FromBody] Usluga usluga)
        {
            if (usluga == null)
            {
                return BadRequest("Услуга ноу");
            }
            usluga.CreatedAt = DateTimeOffset.UtcNow;
            usluga.ModifiedAt = DateTimeOffset.UtcNow;
            usluga.Status = Usluga.UslugaEnum.Published;
            _context.Uslugi.Add(usluga);
            await _context.SaveChangesAsync();
            return Ok(usluga);
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateStatus(int id)
        {
            var target_usluga = await _context.Uslugi.FirstOrDefaultAsync(u => u.Id == id);
            if (target_usluga == null)
            {
                return NotFound();
            }
            if (target_usluga.Status == Usluga.UslugaEnum.Published)
            {
                target_usluga.Status = Usluga.UslugaEnum.Unpublished;
            }
            else if (target_usluga.Status == Usluga.UslugaEnum.Unpublished)
            {
                target_usluga.Status = Usluga.UslugaEnum.Published;
            }
            target_usluga.ModifiedAt = DateTimeOffset.UtcNow;
            await _context.SaveChangesAsync();

            return Ok(target_usluga);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsluga(int id, [FromBody] Usluga updatedUsluga)
        {
            var usluga = await _context.Uslugi.FirstOrDefaultAsync(u => u.Id == id);
            if (usluga == null)
            {
                return NotFound();
            }
            usluga.Name = updatedUsluga.Name;
            usluga.Type = updatedUsluga.Type;
            usluga.Price = updatedUsluga.Price;
            usluga.ModifiedAt = DateTimeOffset.UtcNow;
            _context.Uslugi.Update(usluga);
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsluga(int id)
        {
            var usluga = await _context.Uslugi.FirstOrDefaultAsync(u => u.Id == id);
            if (usluga == null)
            {
                return NotFound();
            }
            return Ok(usluga);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsluga(int id)
        {
            var target_usluga = await _context.Uslugi.FirstOrDefaultAsync(u => u.Id == id);
            if (target_usluga == null)
            {
                return NotFound();
            }
            _context.Uslugi.Remove(target_usluga);
            await _context.SaveChangesAsync();
            return Ok(target_usluga);
        }
    }
}