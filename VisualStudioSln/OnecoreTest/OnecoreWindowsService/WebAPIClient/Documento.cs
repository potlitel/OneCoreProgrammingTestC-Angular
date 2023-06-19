using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService.WebAPIClient
{
    [Serializable]
    public class Documento
    {
        public Documento()
        {
            Compras = new HashSet<Compra>();
        }
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DirecFisica { get; set; }
        public string EstadoNotifi { get; set; }
        public DateTime Fecha { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
    }
}
