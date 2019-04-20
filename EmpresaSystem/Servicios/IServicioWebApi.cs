using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpresaSystem.Modelos;

namespace EmpresaSystem.Servicios
{
    public interface IServicioWebApi
    {
        Task<List<T>> ObtenerItems<T>(string controller);
        Task<List<T>> ObtenerItems<T>(string controller, string metodo);
        Task<List<T>> ObtenerItems<T>(string controller, int id);
        Task<T> ObtenerItem<T>(string controller);
        Task<T> ObtenerItem<T>(string controller, string metodo);
        Task<T> ObtenerItem<T>(string controller, int id);
        Task<T> AgregarItem<T>(string controller, T item);
        Task<bool> ActualizarItem<T>(string controller, T item, int id);
        Task<bool> EliminarItem(string controller, int id);
    }
}
