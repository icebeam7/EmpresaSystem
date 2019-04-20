using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmpresaSystem.Modelos
{
    public class DepartamentoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        [JsonIgnore]
        public ICollection<EmpleadoDTO> Empleados { get; set; }
    }
}
