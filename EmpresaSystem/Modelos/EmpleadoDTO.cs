using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmpresaSystem.Modelos
{
    public class EmpleadoDTO
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public int IdDepartamento { get; set; }
        public DepartamentoDTO FK_DepartamentoEmpleado { get; set; }

        public string NombreCompleto => $"{ApellidoPaterno} {ApellidoMaterno}, {Nombre}";
    }
}
