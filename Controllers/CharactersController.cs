using BackEnd.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharactersController : ControllerBase
    {
        private readonly AplicationDbContext dbContext;
        public CharactersController(AplicationDbContext _dbContext)
        {
            this.dbContext = _dbContext;
        }
        // GET: api/<CharactersController>
        [HttpGet]
        public async Task<ActionResult> GetCharacters()
        {
            try
            {
                var characterList = await dbContext.CharacterDB.ToListAsync();
                return Ok(characterList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // GET: api/<CharactersController>/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacterById(int id)
        {
            try
            {
                var character = await dbContext.CharacterDB.FindAsync(id);

                if (character == null)
                {
                    return NotFound();
                }
                return Ok(character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // POST api/<CharactersController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Character character)
        {
            try
            {
                dbContext.Add(character);
                await dbContext.SaveChangesAsync();
                return Ok(character);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CharactersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Character character)
        {
            try
            {
                if(id != character.CharacterID)
                {
                    return NotFound();
                }
                dbContext.Update(character);
                await dbContext.SaveChangesAsync();
                return Ok(new { message = "El personaje fue actualizado con exito" });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CharactersController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var character = await dbContext.CharacterDB.FindAsync(id);

                if (character == null)
                {
                    return NotFound();
                }
                dbContext.CharacterDB.Remove(character);
                await dbContext.SaveChangesAsync();
                return Ok(new { message = "Personaje eliminado" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
