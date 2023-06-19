using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnecoreTestWebApiRest.Models
{
    /// <summary>
    /// Propiedades de la entidad Clientes
    /// </summary>
    public partial class Clientes
    {
        public Clientes()
        {
            Compras = new HashSet<Compras>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Cp { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Compras> Compras { get; set; }
    }
}
