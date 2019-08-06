using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcademicRecordAPI.Models;
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AcademicRecordAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly PlayerContext _context;

        public SubjectController(PlayerContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Subject>>> GetSubjects()
        {

            return await _context.Subjects.ToListAsync();
        }

        [HttpPost]
        [Route("BatchDelete")]
        public async Task<IActionResult> BatchDeleteSubjects([FromBody] Subject[] subjects)
        {
            foreach (Subject s in subjects)
            {
                _context.Remove(s);
            }
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("BatchSave")]
        public async Task<IActionResult> BatchSaveSubjects([FromBody] Subject[] subjects)
        {

            try
            {
                foreach (Subject s in subjects)
                {
                    if (_context.Subjects.Any(subj => subj.Id == s.Id))
                    {
                        continue;
                    }
                    _context.Subjects.Add(s);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Ok(e.ToString());
            }

            return Ok();

        }
    }
}
