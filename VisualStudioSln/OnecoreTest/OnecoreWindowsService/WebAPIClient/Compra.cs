using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService.WebAPIClient
{    
    [Serializable]
    public class Compra
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdDocumento { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }
}
