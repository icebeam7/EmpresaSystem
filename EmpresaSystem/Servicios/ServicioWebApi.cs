using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using EmpresaSystem.Modelos;
using EmpresaSystem.Helpers;
using Newtonsoft.Json;

namespace EmpresaSystem.Servicios
{
    public class ServicioWebApi : HttpClient, IServicioWebApi
    {
        static ServicioWebApi() { }

        private static readonly ServicioWebApi servicio = new ServicioWebApi();

        private ServicioWebApi() : base()
        {
            Timeout = TimeSpan.FromMilliseconds(15000);
            MaxResponseContentBufferSize = int.MaxValue;
            BaseAddress = new Uri(Constantes.WebApiUrl);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static ServicioWebApi Servicio => servicio;

        public async Task<bool> ActualizarItem<T>(string controller, T item, int id)
        {
            var body = JsonConvert.SerializeObject(item);
            var contenido = new StringContent(body, Encoding.UTF8, "application/json");
            var respuesta = await PutAsync($"{controller}/{id}", contenido);

            if (respuesta.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<T> AgregarItem<T>(string controller, T item)
        {
            var body = JsonConvert.SerializeObject(item);
            var contenido = new StringContent(body, Encoding.UTF8, "application/json");
            var respuesta = await PostAsync(controller, contenido);

            if (respuesta.IsSuccessStatusCode)
            {
                var json = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return default(T);
        }

        public async Task<bool> EliminarItem(string controller, int id)
        {
            var respuesta = await DeleteAsync($"{controller}/{id}");

            if (respuesta.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<T> ObtenerItem<T>(string controller)
        {
            var respuesta = await GetAsync(controller);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contenido);
            }

            return default(T);
        }

        public async Task<T> ObtenerItem<T>(string controller, string metodo)
        {
            var respuesta = await GetAsync($"{controller}/{metodo}");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contenido);
            }

            return default(T);
        }

        public async Task<T> ObtenerItem<T>(string controller, int id)
        {
            var respuesta = await GetAsync($"{controller}/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(contenido);
            }

            return default(T);
        }

        public async Task<List<T>> ObtenerItems<T>(string controller)
        {
            var respuesta = await GetAsync(controller);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(contenido);
            }

            return default(List<T>);
        }

        public async Task<List<T>> ObtenerItems<T>(string controller, string metodo)
        {
            var respuesta = await GetAsync($"{controller}/{metodo}");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(contenido);
            }

            return default(List<T>);
        }

        public async Task<List<T>> ObtenerItems<T>(string controller, int id)
        {
            var respuesta = await GetAsync($"{controller}/{id}");

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<T>>(contenido);
            }

            return default(List<T>);
        }
    }
}
