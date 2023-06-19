using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnecoreWindowsService.Utils
{
    public class configHandler
    {
        public string apiUrl() 
        {
            try 
            { 
                return ConfigurationManager.AppSettings["apiUrl"];
            }
            catch 
            {
                return string.Empty;
            }            
        }
        public string emailOrigen()
        {
            try
            {
                return ConfigurationManager.AppSettings["emailOrigen"];
            }
            catch
            {
                return string.Empty;
            }
        }
        public string emailPassword()
        {
            try
            {
                return ConfigurationManager.AppSettings["emailPassword"];
            }
            catch
            {
                return string.Empty;
            }
        }
        public string emailSmtpClientURL()
        {
            try
            {
                return ConfigurationManager.AppSettings["emailSmtpClientURL"];
            }
            catch
            {
                return string.Empty;
            }
        }
        public int emailSmtpClientPort()
        {
            try
            {
                return Int32.Parse(ConfigurationManager.AppSettings["emailSmtpClientPort"]);
            }
            catch
            {
                return 0;
            }

        }
        public bool emailSmtpClientSSL()
        {
            try
            {
                return ConfigurationManager.AppSettings["emailSmtpClientSSL"]=="1";
            }
            catch
            {
                return false;
            }
        }       
        public string docsFolder()
        {
            try
            {
                return ConfigurationManager.AppSettings["docsFolder"];
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
