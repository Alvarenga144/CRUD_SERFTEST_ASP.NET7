using AppCRUDSerfinsa.Models;
using AppCRUDSerfinsa.Repositorios.Contrato;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

// Acá están todos las acciones que van a interactuar con el Model Empleado

namespace AppCRUDSerfinsa.Repositorios.Implementacion
{
    public class EmpleadoRepository : IGenericRepository<Empleado>
    {
        private readonly string _cadenaSQL = "";

        public EmpleadoRepository(IConfiguration configuration)
        {
            _cadenaSQL = configuration.GetConnectionString("cadenaSQL");
        }


        // Lista para leer data desde el procedimiento almacenado en DB
        public async Task<List<Empleado>> Lista()
        {
            List<Empleado> _lista = new List<Empleado>();

            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_ListaEmpleados", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Empleado
                        {
                            idEmpleado = Convert.ToInt32(dr["idEmpleado"]),
                            nombreEmpleado = dr["nombreEmpleado"].ToString(),
                            apellidoEmpleado = dr["apellidoEmpleado"].ToString(),
                            edadEmpleado = Convert.ToInt32(dr["edadEmpleado"]),
                            direccionEmp = dr["direccionEmp"].ToString(),
                            telefonoEmp = dr["telefonoEmp"].ToString(),
                            emailEmpleado = dr["emailEmpleado"].ToString(),
                        });
                    }
                }
            }
            return _lista;
        }

        // Metodo para guardar data con el procedimiento almacenado en DB
        public async Task<bool> Guardar(Empleado model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_GuardarEmpleado", conexion);
                cmd.Parameters.AddWithValue("nombreEmpleado", model.nombreEmpleado);
                cmd.Parameters.AddWithValue("apellidoEmpleado", model.apellidoEmpleado);
                cmd.Parameters.AddWithValue("edadEmpleado", model.edadEmpleado);
                cmd.Parameters.AddWithValue("direccionEmp", model.direccionEmp);
                cmd.Parameters.AddWithValue("telefonoEmp", model.telefonoEmp);
                cmd.Parameters.AddWithValue("emailEmpleado", model.emailEmpleado);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Metodo para editar data con el procedimiento almacenado en DB
        public async Task<bool> Editar(Empleado model)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EditarEmpleado", conexion);

                cmd.Parameters.AddWithValue("idEmpleado", model.idEmpleado);
                cmd.Parameters.AddWithValue("nombreEmpleado", model.nombreEmpleado);
                cmd.Parameters.AddWithValue("apellidoEmpleado", model.apellidoEmpleado);
                cmd.Parameters.AddWithValue("edadEmpleado", model.edadEmpleado);
                cmd.Parameters.AddWithValue("direccionEmp", model.direccionEmp);
                cmd.Parameters.AddWithValue("telefonoEmp", model.telefonoEmp);
                cmd.Parameters.AddWithValue("emailEmpleado", model.emailEmpleado);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        // Metodo para eliminar data con el procedimiento almacenado en DB
        public async Task<bool> Eliminar(int id)
        {
            using (var conexion = new SqlConnection(_cadenaSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("sp_EliminarEmpleado", conexion);
                cmd.Parameters.AddWithValue("idEmpleado", id);

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_afectadas = await cmd.ExecuteNonQueryAsync();

                if (filas_afectadas > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        
    }
}
