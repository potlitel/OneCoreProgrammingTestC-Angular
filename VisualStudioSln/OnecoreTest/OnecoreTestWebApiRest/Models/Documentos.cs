using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnecoreTestWebApiRest.Models
{
    /// <summary>
    /// Propiedades de la entidad Documentos
    /// </summary>
    public partial class Documentos
    {
        public Documentos()
        {
            Compras = new HashSet<Compras>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string DirecFisica { get; set; }
        public string EstadoNotifi { get; set; }
        public DateTime Fecha { get; set; }

        public virtual ICollection<Compras> Compras { get; set; }
    }
}
