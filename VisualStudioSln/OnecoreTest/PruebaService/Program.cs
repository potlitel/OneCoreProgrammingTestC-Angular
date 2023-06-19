using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnecoreWindowsService.Utils;
using OnecoreWindowsService.WebAPIClient;
using OnecoreWindowsService;



namespace PruebaService
{
    public class Program
    {

        static void Main(string[] args)
        {
            DERJAYAService ser = new DERJAYAService();
            emailHandler OCservice = new emailHandler();
            Documento _Documento = new Documento();


            _Documento.Id = 153;
            _Documento.Nombre = "";
            _Documento.DirecFisica = "";
            _Documento.EstadoNotifi = "F";



            //ser.();
            ser.Servicetest();

            OCservice.Notificar(_Documento);
        }
    }
}
