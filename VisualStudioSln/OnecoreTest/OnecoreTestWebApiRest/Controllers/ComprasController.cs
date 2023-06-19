using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnecoreTestWebApiRest.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace OnecoreTestWebApiRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : ControllerBase
    {
        private readonly OnecoreProgrammingTestContext _context;
        /// <summary>
        /// Método ComprasController
        /// </summary>
        /// <param name="context"></param>
        public ComprasController(OnecoreProgrammingTestContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Método para obtener todas las compras
        /// </summary>
        /// <returns></returns>
        // GET: api/Compras
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Compras>>> GetCompras()
        {
            return await _context.Compras.ToListAsync();
        }
        /// <summary>
        /// Método para obtener los datos de una compra por su identificador
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Compras>> GetCompras(int id)
        {
            var compras = await _context.Compras.FindAsync(id);

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }
        /// <summary>
        /// Método para actualizar los datos de una compra
        /// </summary>
        /// <param name="id"></param>
        /// <param name="compras"></param>
        /// <returns></returns>
        // PUT: api/Compras/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompras(int id, Compras compras)
        {
            if (id != compras.Id)
            {
                return BadRequest();
            }

            _context.Entry(compras).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComprasExists(id))
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
        /// <summary>
        /// Método para crear una nueva compra
        /// </summary>
        /// <param name="compras"></param>
        /// <returns></returns>
        // POST: api/Compras
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Compras>> PostCompras(Compras compras)
        {
            _context.Compras.Add(compras);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompras", new { id = compras.Id }, compras);
        }
        /// <summary>
        /// Método para eliminar los datos de una entidad tipo Compras
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Compras>> DeleteCompras(int id)
        {
            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound();
            }

            _context.Compras.Remove(compras);
            await _context.SaveChangesAsync();

            return compras;
        }
        /// <summary>
        /// Método para verificar la existencia de una entidad de tipo Compras
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool ComprasExists(int id)
        {
            return _context.Compras.Any(e => e.Id == id);
        }


        /// <summary>
        /// Método para obtener los datos de una compra por su identificador del documento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: api/Compras/5
        [HttpPost("documento/{id}")]
        public async Task<ActionResult<IEnumerable<Compras>>> GetComprasporDocumentos(int id)
        {
            var compras = await _context.Compras.Where(c => c.IdDocumento == id).ToListAsync();

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }


        [HttpPost("/file")]
        public async Task<ActionResult<IEnumerable<Compras>>> GuardarDatosExcel(IFormFile file, [FromServices] IHostingEnvironment hostingEnvironment)
        {
            string directorio = $"{hostingEnvironment.WebRootPath}\\files";
            string fileName = $"{hostingEnvironment.WebRootPath}\\files\\{file.FileName}";
            if (!Directory.Exists(directorio))
            {
                Directory.CreateDirectory(directorio);
            }
            int idDoc;
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
                Documentos aux = new Documentos();
                aux.Nombre = file.FileName;
                aux.DirecFisica = fileName;
                aux.Fecha = DateTime.Now;
                aux.EstadoNotifi = "f";
                _context.Documentos.Add(aux);
                _context.SaveChanges();
                idDoc = aux.Id;

            }
            var compras = this.getComprasList(file.FileName, idDoc);
            foreach (var item in compras)
            {
                try
                {
                    Compras comp = new Compras();
                    comp.IdCliente = item.IdCliente;
                    comp.IdDocumento = idDoc;
                    comp.PrecioUnitario = item.PrecioUnitario;
                    comp.Total = item.Total;
                    comp.Cantidad = item.Cantidad;
                    comp.Descripcion = item.Descripcion;
                    _context.Compras.Add(comp);
                    _context.SaveChanges();
                }
                catch (Exception)
                {
                    var doc = _context.Documentos.FindAsync(idDoc);
                    doc.Result.Compras.Clear();
                    _context.Documentos.Remove(doc.Result);
                    _context.SaveChanges();
                    /*var documento = await _context.Documentos.FindAsync(idDoc);
                    _context.Documentos.Remove(documento);*/
                    throw new Exception("No se pudo guardar los archivos de la compra");
                }


            }
            return await Task.FromResult(compras);
        }



        private List<Compras> getComprasList(string fName, int IdDoc)
        {
            List<Compras> compras = new List<Compras>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        try
                        {
                            String IdCliente_ = reader.GetValue(0).ToString();
                            String Cantidad_ = reader.GetValue(1).ToString();
                            String Descripcion_ = reader.GetValue(2).ToString();
                            String PrecioUnitario_ = reader.GetValue(3).ToString();
                            String Total_ = reader.GetValue(4).ToString();
                            if (reader.GetValue(0).ToString() != "Id Cliente")
                            {
                                compras.Add(new Compras()
                                {
                                    IdCliente = int.Parse(IdCliente_),
                                    Cantidad = int.Parse(Cantidad_),
                                    Descripcion = Descripcion_,
                                    PrecioUnitario = decimal.Parse(PrecioUnitario_),
                                    Total = decimal.Parse(Total_)
                                });
                            }
                        }
                        catch (Exception)
                        {
                            reader.Close();
                            System.IO.File.Delete(fileName);
                            var doc = _context.Documentos.FindAsync(IdDoc);
                            _context.Documentos.Remove(doc.Result);
                            _context.SaveChangesAsync();
                            throw new Exception("No se pudo leer el archivo seleccionado por favor corrija el orden o nombre de las tablas por el siguiente: " + "IdCliente|Cantidad|Descripcion|PrecioUnitario|Total");
                        }


                    }

                }
            }
            return compras;
        }
        /// <summary>
        /// Método para obtener los datos de las compras por documento. 
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <returns></returns>
        // GET: api/Compras/ComprasXDocumento/5
        [HttpGet("/ComprasXDocumento/{idDocumento}")]
        public async Task<ActionResult<IEnumerable<Compras>>> GetDocumentoCompras(int idDocumento)
        {
            var compras = await _context.Compras.Where(e => e.IdDocumento == idDocumento).ToListAsync();

            if (compras == null)
            {
                return NotFound();
            }

            return compras;
        }
    }
}
