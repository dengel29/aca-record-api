using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using AcademicRecordAPI.Models;
using System;

namespace AcademicRecordAPI.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerContext _context;

        public PlayerController(PlayerContext context)
        {
            _context = context;
            
            if (_context.Players.Count() > 0)
            {
                _context.Players.Remove(_context.Players.Last());
            }
            if (_context.Players.Count() == 0)
            {
                var players = new Player[]
                {
                    new Player {Name = "Bobby Burger", Subjects = new List<Subject>(),  CanPlay = true },
                    new Player {Name = "Tina Fay", Subjects = new List<Subject>(), CanPlay = false },
                    new Player {Name = "Andy Bourdain", Subjects = new List<Subject>(), CanPlay = false },
                    new Player {Name = "Bobby Flay", Subjects = new List<Subject>(), CanPlay = false },
                    new Player {Name = "Hungry Thomas", Subjects = new List<Subject>(), CanPlay = true },
                    new Player {Name = "Jenna Marbles", Subjects = new List<Subject>(), CanPlay = false },
                    new Player {Name = "Belle Delphine", Subjects = new List<Subject>(), CanPlay = false },
                    new Player {Name = "Jay Jay", Subjects = new List<Subject>(), CanPlay = false }
                };

                foreach (Player p in players)
                {
                    _context.Players.Add(p);
                }
 
                _context.SaveChangesAsync();
            }
        }

        // GET api/Player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            
            return await _context.Players.Include(p => p.Subjects).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }
            return player;
        }

        [HttpPost]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(Player), new { id = player.Id }, player);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Player>> PutPlayer(int id, Player player)
        {
            if (id != player.Id)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }


}
