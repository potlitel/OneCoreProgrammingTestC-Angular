using OnecoreWindowsService.WebAPIClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService.Utils
{
    public class emailHandler
    {
        ClienteAPI _ClienteAPI = new ClienteAPI();
        configHandler _configHandler = new configHandler();
        /// <summary>
        /// Método para enviar por correo los registros de una compra a cada cliente del documento.
        /// </summary>
        /// <param name="Documento"></param>
        /// <returns></returns>
        public async void Notificar(Documento _Documento)
        {
            //Se recibe el identificador del Documento por parametro

            //Se busca las compras relacionadas a ese documento
            List<Compra> compras = _Documento.Compras.ToList();

            //Se cuentan el total de compras y se almacena en una variable
            int lineasDocumento = compras.Count();
            // Las compras que tienen ese id de documento significarian ser las lineas del documento

            // Si lineas_documento < 18
            //Enviar los registros en el cuerpo del correo (preferiblemente como tabla)
            if (lineasDocumento > 0)
            {
                //Lista de Id de clientes para no repetir el envío de correo
                List<int> listaIdsClientes = new List<int>();
                //Por cada cliente distinto de las compras se enviará un correo
                foreach (var item in compras)
                {
                    int idcliente = item.IdCliente;
                    bool existe = listaIdsClientes.Contains(idcliente); ;
                    if (!existe)
                    {
                        var iddocumento = item.IdDocumento;
                        var email = "";
                        var nombreCliente = "";
                        var nombreDocumento = "";
                        System.DateTime fechaCargaDoc;
                        var pathDoc = "";

                        Cliente _Cliente = _ClienteAPI.Get_Cliente(item.IdCliente);
                        email = _Cliente.Email;
                        nombreCliente = _Cliente.Nombre;
                        fechaCargaDoc = _Documento.Fecha;
                        pathDoc = _Documento.DirecFisica;
                        nombreDocumento = _Documento.Nombre;

                        //Se envia correo al cliente
                        //EnviarCorreo(false, "jorge.martinezr0824@gmail.com", "Jorge", System.DateTime.Now, "Doc1");   //PRUEBA

                        //Si el Excel es menor a 18 líneas se deberan enviar los registros en el cuerpo del correo
                        if (lineasDocumento < 18)
                        {
                            string mensajeTabla = getTablaHtml(compras);
                            //EnviarCorreo(si recibe adjunto (true/false), email, nombre del cliente, fecha hora de la carga, nombre del archivo de Excel).
                            // Adjunto = false
                            EnviarCorreo(false, email, nombreCliente, fechaCargaDoc, nombreDocumento, pathDoc, mensajeTabla);
                        }
                        //Si es igual o mayor a 18 líneas deberá de enviarlos como dato adjunto
                        if (lineasDocumento >= 18)
                        {
                            //EnviarCorreo(si recibe adjunto (true/false), email, nombre del cliente, fecha hora de la carga, nombre del archivo de Excel).
                            // Adjunto = true
                            EnviarCorreo(true, email, nombreCliente, fechaCargaDoc, nombreDocumento, pathDoc, "");
                        }
                        //Se agrega el idcliente para que no repita el envío del correo al mismo cliente
                        listaIdsClientes.Add(idcliente);
                    }
                }
                //Actualizar campo EstadoNotifi
                _Documento.EstadoNotifi = "v";
                await _ClienteAPI.Put_Documento(_Documento);
            }
            else
            {
                throw new Exception("Archivo vacío");
            }
        }
        /// <summary>
        /// Método para obtener una tabla html partiendo de una lista de registros de un documento.
        /// </summary>
        /// <param name="compras"></param>
        /// <returns></returns>
        public static string getTablaHtml(List<Compra> compras)
        {
            try
            {
                //Tipo de tabla y color de borde
                string mensajeTabla = "<table border='3' bordercolor='#005882'>";
                //Título
                mensajeTabla += "<caption><b>COMPRAS</b></caption>";
                //Encabezados
                mensajeTabla += "<tr><th BGCOLOR='#A0C3FF'>Nro.</th><th BGCOLOR='#A0C3FF'>IdCliente</th><th BGCOLOR='#A0C3FF'>Cantidad</th><th BGCOLOR='#A0C3FF'>Descripción</th><th BGCOLOR='#A0C3FF'>Precio Unitario</th><th BGCOLOR='#A0C3FF'>Total</th></tr>";
                int nrofilas = 1;
                foreach (var item in compras)
                {
                    //Llenado de la Tabla
                    mensajeTabla += " <tr><td>{Nro}</td><td>{IdCliente}</td><td>{Cantidad}</td><td>{Descripcion}</td><td>{PrecioUnitario}</td><td>{Total}</td></tr>";
                    mensajeTabla = mensajeTabla.Replace("{Nro}", nrofilas.ToString()).Replace("{IdCliente}", item.IdCliente.ToString()).Replace("{Cantidad}", item.Cantidad.ToString()).Replace("{Descripcion}", item.Descripcion).Replace("{PrecioUnitario}", item.PrecioUnitario.ToString()).Replace("{Total}", item.Total.ToString());
                    nrofilas += 1;
                }
                //Cierre de Tabla
                mensajeTabla += "</table>";

                return mensajeTabla;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// Método para enviar un correo a un cliente con adjunto o con tabla de registros
        /// en el cuerpo del mismo segun corresponda.
        /// </summary>
        /// <param name="adjunto"></param>
        /// <param name="email"></param>
        /// <param name="cliente"></param>
        /// <param name="fecha"></param>
        /// <param name="nombreArchivo"></param>
        /// <param name="pathDoc"></param>
        /// <param name="mensajeTabla"></param>
        /// <returns></returns>
        public void EnviarCorreo(bool adjunto, string email, string cliente, System.DateTime fecha, string nombreArchivo, string pathDoc, string mensajeTabla)
        {
            //El cuerpo del correo deberá contener (Nombre del cliente, fecha hora de la carga y nombre del archivo de Excel).
            string emailOrigen = _configHandler.emailOrigen();
            string pasword = _configHandler.emailPassword(); //Se especifica de este modo solo para pruebas (clave para aplicacion de terceros)
            string asunto = "Notificación de Compra";
            string mensaje = "Estimado cliente " + "<b>" + cliente + " </b>" + ", a continuación le mostramos los datos de la compra: " + "<br/>";
            string mensajeNombreCliente = "<b> Nombre del cliente: </b>" + cliente + "<br/>";
            string mensajeFecha = "<b> Fecha y hora de la carga: </b>" + fecha + "<br/>";
            string mensaNombreArchivo = "<b> Nombre del archivo: </b>" + nombreArchivo + "<br/>";
            // Instancia del Correo
            MailMessage oMailMessage = new MailMessage(emailOrigen, email, asunto, mensaje);
            // Formato
            oMailMessage.IsBodyHtml = true;
            // Cuerpo
            oMailMessage.Body = mensaje;
            oMailMessage.Body += mensajeNombreCliente;
            oMailMessage.Body += mensajeFecha;
            oMailMessage.Body += mensaNombreArchivo;

            //Si se valida que es con adjunto
            if (adjunto)
            {
                //Ruta del fichero a adjuntar  (Valorar aqui como llegar al fichero en cuestion)
                //string path = @"D:\WORK\Mis Proyectos\Proyectos C#\TestEmail\COntrol de Tareas de Examen con Mexico.xlsx"; //Ejemplo local
                oMailMessage.Attachments.Add(new Attachment(pathDoc));
            }
            else //Sino se muestran los registros en forma de tabla en el cuerpo del correo
            {
                //Se agregan 2 saltos de línea antes de pintar la tabla
                oMailMessage.Body += "<br/>";
                oMailMessage.Body += "<br/>";
                //Se agrega la tabla al cuerpo del correo
                oMailMessage.Body += mensajeTabla;
            }
            // Smtp
            SmtpClient smtpClient = new SmtpClient(_configHandler.emailSmtpClientURL());
            // SSL
            smtpClient.EnableSsl = _configHandler.emailSmtpClientSSL();
            // Si se usará las credenciales por defecto
            smtpClient.UseDefaultCredentials = false;
            // Piuerto
            smtpClient.Port = _configHandler.emailSmtpClientPort(); //25;
                                   //Credenciales otorgadas
                                   //smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, pasword);
            smtpClient.Credentials = new System.Net.NetworkCredential(emailOrigen, pasword);
            //Envio del correo con sus valores
            try
            {
                smtpClient.Send(oMailMessage);
            }
            catch (Exception ex)
            {
                throw new Exception("No se ha podido enviar el email", ex.InnerException);
            }
            finally
            {
                //Envía un mensaje QUIT al servidor SMTP, finaliza correctamente la conexión TCP,
                // y libera todos los recursos utilizados por la instancia actual de System.Net.Mail.SmtpClient.
                smtpClient.Dispose();
            }
        }
    }
}
