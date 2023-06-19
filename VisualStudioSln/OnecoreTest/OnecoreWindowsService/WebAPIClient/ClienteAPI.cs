using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnecoreWindowsService.Utils;

namespace OnecoreWindowsService.WebAPIClient
{
    public class ClienteAPI
    {
        private static readonly HttpClient client = new HttpClient();
        private configHandler _configHandler = new configHandler();
        public List<Documento> Get_Documentos()
        {
            List<Documento> result = new List<Documento>();
            string url = _configHandler.apiUrl() + String.Format("/Documentos/Notif/{0}", "f");
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = JsonConvert.DeserializeObject<List<Documento>>(reader.ToString());
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return result;
        }
        public Cliente Get_Cliente(int Id)
        {
            Cliente result = new Cliente();
            string url = _configHandler.apiUrl() + String.Format("/Clientes/{0}", Id);
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.ContentType = "application/json";
            request.Accept = "application/json";
            try
            {
                WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = JsonConvert.DeserializeObject<Cliente>(reader.ToString());
            }
            catch (WebException ex)
            {
                // Handle error
            }
            return result;
        }
        public async Task<bool> Put_Documento(Documento d)
        {
            bool result = false;
            string url = _configHandler.apiUrl() + String.Format("/Documentos/{0}", d.Id);

            var stringDocumento = JsonConvert.SerializeObject(d);

            var httpContent = new StringContent(stringDocumento, Encoding.UTF8, "application/json");

            var httpClient = new HttpClient();

            // Do the actual request and await the response
            var httpResponse = await httpClient.PostAsync("url", httpContent);

            // If the response contains content we want to read it!
            if (httpResponse.Content != null)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                result = true;
                // From here on you could deserialize the ResponseContent back again to a concrete C# type using Json.Net
            }
            return result;
        }

    }
}
