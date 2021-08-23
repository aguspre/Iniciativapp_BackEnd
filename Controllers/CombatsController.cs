using Backend;
using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CombatsController : ControllerBase
    {
        private readonly AplicationDbContext _context;

        public CombatsController(AplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Combats
        [HttpGet]
        public async Task<ActionResult<Combat>> GetCombatDB()
        {
            var combat = await _context.CombatDB.LastAsync();


            return combat;
        }

        // GET: api/Combats/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List<Character>>> GetCombat(int id)
        {
            var _combat = await _context.CombatDB.FindAsync(id);

             //ActionResult<List<Character>> _chars = await GetCharactersFromCombat(_combat.CombatID);
            ActionResult<List<Character>> _cantPc = await GetCantPC(_combat.CombatID);

            if (_combat == null)
            {
                return NotFound();
            }


            return _cantPc;
        }

        //private async Task<ActionResult<List<Character>>> GetCharactersFromCombat(int id)
        //{

        //    List<Character> combatCharacters = await _context.CombatCharacters
        //                                                 .Where(combat => combat.CombatID == id)
        //                                                 .Select(combat => combat.Character)
        //                                                 .ToListAsync();
                                                       
        //    return combatCharacters;
        //}

        private async Task<ActionResult<List<Character>>> GetCantPC(int id)
        {
            List<Character> cantPc = await _context.CombatCharacters
                                                    .Where(combat => combat.CombatID == id)
                                                    .Select(c => c.Character)
                                                    .ToListAsync();

            return cantPc;

        }

        // PUT: api/Combats/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCombat(int id, Combat combat)
        {
            if (id != combat.CombatID)
            {
                return BadRequest();
            }

            _context.Entry(combat).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CombatExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        // POST: api/Combats
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<int>> PostCombat([FromBody] List<CantPc> characters)
        {
            Combat combat;

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    combat = new Combat
                    {
                        DofCombat = DateTime.Now,
                        CombatCharacters = new List<CombatCharacters>()
                    };

                    characters.ForEach(x => 
                                        {
                                            for (int i = 0; i < x.cantidad; i++)
                                            {
                                                combat.CombatCharacters.Add(new CombatCharacters
                                                { 
                                                    Character = _context.CharacterDB.Find(x.characterID),
                                                    CharacterID = x.characterID,
                                                    Combat = combat,
                                                    CombatID = combat.CombatID,
                                                   
                                                   
                                                });
                                            }
                                        });

                    _context.CombatDB.Add(combat);

                    await _context.SaveChangesAsync();

                    transaction.Commit();
                }
                catch (Exception ex )
                {
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }

            int id = combat.CombatID;

            return id;
        }

        // DELETE: api/Combats/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombat(int id)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var combat = await _context.CombatDB.FindAsync(id);
                    if (combat == null)
                    {
                        return NotFound();
                    }
                    List<CombatCharacters> combatChar = _context.CombatCharacters.Where(x => x.CombatID == id).ToList();

                    _context.CombatDB.Remove(combat);

                    foreach (CombatCharacters comChar in combatChar)
                    {
                        if (comChar.CombatID == id)
                        {
                            _context.CombatCharacters.Remove(comChar);
                        }
                    }

                    await _context.SaveChangesAsync();
                    transaction.Commit();

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    return BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        private bool CombatExists(int id)
        {
            return _context.CombatDB.Any(e => e.CombatID == id);
        }
    }
}

public class CantPc
{
    public int characterID { get; set; }
    public int cantidad { get; set; }
}