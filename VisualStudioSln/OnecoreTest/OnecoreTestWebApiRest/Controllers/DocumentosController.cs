using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnecoreTestWebApiRest.Models;

namespace OnecoreTestWebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController : ControllerBase
    {
        private readonly OnecoreProgrammingTestContext _context;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context"></param>
        public DocumentosController(OnecoreProgrammingTestContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Método para obtener el listado de documentos de compras
        /// </summary>
        /// <returns></returns>
        // GET: api/Documentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documentos>>> GetDocumentos()
        {
            return await _context.Documentos.ToListAsync();
        }
        /// <summary>
        /// Método para obtener un documento de compras por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Documentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documentos>> GetDocumentos(int id)
        {
            var documentos = await _context.Documentos.FindAsync(id);

            if (documentos == null)
            {
                return NotFound();
            }

            return documentos;
        }
        /// <summary>
        /// Método para obtener los documentos de compras por su Estado de Notificacion
        /// </summary>
        /// <param name="EstadoNotifi"></param>
        /// <returns></returns>
        // GET: api/Documentos/Notif/V
        [HttpGet("/Notif/{EstadoNotifi}")]
        public async Task<ActionResult<IEnumerable<Documentos>>> GetDocumentosXNotificar(string EstadoNotifi)
        {
            string[] arr = { "V", "F" };
            bool exists = Array.Exists(arr, element => element == EstadoNotifi);
            if (!exists)
            {
                return BadRequest();
            }
            return await _context.Documentos.Where(x => x.EstadoNotifi == EstadoNotifi).ToListAsync();
        }
        /// <summary>
        /// Método para actualizar la información de un documento de compras
        /// </summary>
        /// <param name="id"></param>
        /// <param name="documentos"></param>
        /// <returns></returns>
        // PUT: api/Documentos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutDocumentos(int id, Documentos documentos)
        //{
        //    if (id != documentos.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(documentos).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!DocumentosExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
        /// <summary>
        /// Método para adicionar nuevos documentos de compras
        /// </summary>
        /// <param name="documentos"></param>
        /// <returns></returns>
        // POST: api/Documentos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Documentos>> PostDocumentos(Documentos documentos)
        {
            _context.Documentos.Add(documentos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocumentos", new { id = documentos.Id }, documentos);
        }
        /// <summary>
        /// Método para eliminar un determinado documento de compras mediante su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Documentos/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Documentos>> DeleteDocumentos(int id)
        //{
        //    var documentos = await _context.Documentos.FindAsync(id);
        //    if (documentos == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Documentos.Remove(documentos);
        //    await _context.SaveChangesAsync();

        //    return documentos;
        //}
        /// <summary>
        /// Método para verificar la existencia de un determinado documento de compras por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool DocumentosExists(int id)
        {
            return _context.Documentos.Any(e => e.Id == id);
        }
    }
}
