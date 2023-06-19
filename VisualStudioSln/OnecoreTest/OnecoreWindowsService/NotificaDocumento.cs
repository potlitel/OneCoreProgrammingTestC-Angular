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
    partial class NotificaDocumento : ServiceBase
    {
        ClienteAPI _ClienteAPI = new ClienteAPI();
        emailHandler _emailHandler = new emailHandler();
        private bool running = false;
        public NotificaDocumento()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: Add code here to start your service.
            tmrChecker.Start();
        }

        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
            tmrChecker.Stop();
        }

        private void tmrChecker_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (running) return;

            try
            {
                foreach (Documento d in _ClienteAPI.Get_Documentos()) 
                {
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
