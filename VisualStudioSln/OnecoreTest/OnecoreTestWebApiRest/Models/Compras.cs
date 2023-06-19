using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace OnecoreTestWebApiRest.Models
{
    /// <summary>
    /// Propiedades de la entidad Compras
    /// </summary>
    public partial class Compras
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdDocumento { get; set; }
        public int Cantidad { get; set; }
        public string Descripcion { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }

        [JsonIgnore]
        public virtual Clientes IdClienteNavigation { get; set; }
        [JsonIgnore]
        public virtual Documentos IdDocumentoNavigation { get; set; }
    }
}
