using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnecoreTestWebApiRest.Models;
using OnecoreTestWebApiRest.Wrappers;

namespace OnecoreTestWebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly OnecoreProgrammingTestContext _context;
        /// <summary>
        /// Método ClientesController
        /// </summary>
        /// <param name="context"></param>
        public ClientesController(OnecoreProgrammingTestContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Método GetClientes
        /// </summary>
        /// <returns></returns>
        // GET: api/Clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes()
        {
            var response = await _context.Clientes.Where(b => !b.IsDeleted).ToListAsync();
            return Ok(new { data = response, message = "Listado de Clientes", ok = true });
        }
        /// <summary>
        /// Método GetClientes para obtener un cliente según su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Clientes>>> GetClientes(int id)
        //public async Task<ActionResult<Clientes>> GetClientes(int id)
        {
            var response = await _context.Clientes.Where(b => !b.IsDeleted && b.Id == id).FirstOrDefaultAsync();
            if (response == null)
            {
                //return NotFound();
                return Ok(new { data = "", message = "No se ha encontrado ningún cliente con el identificador suministrado", ok = false });
            }
            return Ok(new { data = response, message = "Cliente encontrado satisfactoriamente", ok = true });
        }
        /// <summary>
        /// Método para actualizar los datos de un cliente determinado
        /// </summary>
        /// <param name="id"></param>
        /// <param name="clientes"></param>
        /// <returns></returns>
        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClientes(int id, Clientes clientes)
        {
            if (id != clientes.Id)
            {
                //return BadRequest();
                return Ok(new { data = "", message = "No se ha encontrado ningún cliente con el identificador suministrado", ok = false });
            }

            _context.Entry(clientes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientesExists(id))
                {
                    //return NotFound();
                    return Ok(new { data = "", message = "No se ha encontrado ningún cliente con el identificador suministrado", ok = false });
                }
                else
                {
                    throw;
                }
            }

            //return NoContent();
            var response = await _context.Clientes.Where(b => !b.IsDeleted).ToListAsync();
            return Ok(new { data = response, message = "Cliente actualizado satisfactoriamente", ok = true });
        }
        /// <summary>
        /// Método PostClientes para adicionar un nuevo cliente
        /// </summary>
        /// <param name="clientes"></param>
        /// <returns></returns>
        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Clientes>> PostClientes(Clientes clientes)
        {
            _context.Clientes.Add(clientes);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetClientes", new { id = clientes.Id }, clientes);
            var response = await _context.Clientes.Where(b => !b.IsDeleted).ToListAsync();
            return Ok(new { data = response, message = "Cliente creado satisfactoriamente", ok = true });
        }
        /// <summary>
        /// Método para eliminar un determinado cliente
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Clientes>> DeleteClientes(int id)
        {
            var clientes = await _context.Clientes.FindAsync(id);
            if (clientes == null)
            {
                //return NotFound();
                return Ok(new { data = "", message = "No se ha encontrado ningún cliente con el identificador suministrado", ok = false });
            }
            //Actualizamos la propiedad IsDeleted con valor = true
            clientes.IsDeleted = true;
            //_context.Clientes.Remove(clientes);
            await _context.SaveChangesAsync();

            //return clientes;
            var response = await _context.Clientes.Where(b => !b.IsDeleted).ToListAsync();
            return Ok(new { data = response, message = "Cliente eliminado satisfactoriamente", ok = true });
        }
        /// <summary>
        /// Método para verificar la existencia de un determinado cliente por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ClientesExists(int id)
        {
            return _context.Clientes.Any(e => e.Id == id);
        }
    }
}
