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
        public async Task<IActionResult> GetAllUslugi()
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
            usluga.CreatedAt = DateTimeOffset.Now;
            usluga.ModifiedAt = DateTimeOffset.Now;
            usluga.Status = Usluga.UslugaEnum.Published;
            return Ok(usluga);
        }

    }
}