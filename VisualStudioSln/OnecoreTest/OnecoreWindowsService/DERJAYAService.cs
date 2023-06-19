using OnecoreWindowsService.Utils;
using OnecoreWindowsService.WebAPIClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService
{
    public partial class DERJAYAService : ServiceBase
    {
        ClienteAPI _ClienteAPI = new ClienteAPI();
        emailHandler _emailHandler = new emailHandler();
        private bool running = false;
        public DERJAYAService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            timer1.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            timer1.Stop();
        }

        private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (running) return;

            try
            {
                foreach (Documento d in _ClienteAPI.Get_Documentos())
                {
                    EventLog.WriteEntry(String.Format("Se inicia el envio de correos para el documento ({0})", d.Nombre));
                    _emailHandler.Notificar(d);
                }
            }
            catch
            {
            }
            running = false;
        }

        public void Servicetest()
        {
            if (running) return;

            try
            {
                foreach (Documento d in _ClienteAPI.Get_Documentos())
                {
                    EventLog.WriteEntry(String.Format("Se inicia el envio de correos para el documento ({0})", d.Nombre));
                    _emailHandler.Notificar(d);
                }
            }
            catch
            {
            }
            running = false;
        }

    }
}
