using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService.WebAPIClient
{
    [Serializable]
    public class Cliente
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Rfc { get; set; }
        public string Direccion { get; set; }
        public string Cp { get; set; }
        public string Email { get; set; }
        public bool IsDeleted { get; set; }
    }
}
