using AppCRUDSerfinsa.Models;

namespace AppCRUDSerfinsa.Models
{
    public class Empleado
    {
        public int idEmpleado { get; set; }
        public string nombreEmpleado { get; set; }
        public string apellidoEmpleado { get; set; }
        public int edadEmpleado { get; set; }
        public string direccionEmp { get; set; }
        public string telefonoEmp { get; set; }
        public string emailEmpleado { get; set; }
    }
}