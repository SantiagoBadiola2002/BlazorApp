using BlazorApp.Models;
using MySql.Data.MySqlClient;

namespace BlazorApp.Infraestructure.Data
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly ConexionMySql _conexion;

        public ClienteRepository()
        {
            _conexion = new ConexionMySql();
        }

        public void AgregarCliente(Cliente cliente)
        {
            string query = $"INSERT INTO Clientes (Cedula, FechaRegistro, Estado) VALUES ('{cliente.Cedula}', '{cliente.FechaRegistro:yyyy-MM-dd}', '{cliente.Estado}')";
            try
            {
                _conexion.AbrirConexion();
                MySqlCommand comando = new MySqlCommand(query, _conexion.Conexion);
                comando.ExecuteNonQuery();
                Console.WriteLine("Cliente agregado exitosamente.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al agregar el cliente: " + ex.Message);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }

        public Cliente ObtenerClientePorCedula(string cedula)
        {
            string query = $"SELECT Cedula, FechaRegistro, Estado FROM Clientes WHERE Cedula = '{cedula}'";
            Cliente cliente = null;

            try
            {
                _conexion.AbrirConexion();
                MySqlCommand comando = new MySqlCommand(query, _conexion.Conexion);
                MySqlDataReader lector = comando.ExecuteReader();

                if (lector.Read())
                {
                    cliente = new Cliente(
                        lector["Cedula"].ToString(),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        (EstadoCliente)Enum.Parse(typeof(EstadoCliente), lector["Estado"].ToString())
                    );
                }

                lector.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener el cliente: " + ex.Message);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return cliente;
        }

        public IList<Cliente> ObtenerClientesEnEspera()
        {
            string query = "SELECT Cedula, FechaRegistro, Estado FROM Clientes WHERE Estado = 'Esperando'";
            List<Cliente> clientes = new List<Cliente>();

            try
            {
                _conexion.AbrirConexion();
                MySqlCommand comando = new MySqlCommand(query, _conexion.Conexion);
                MySqlDataReader lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    var cliente = new Cliente(
                        lector["Cedula"].ToString(),
                        Convert.ToDateTime(lector["FechaRegistro"]),
                        (EstadoCliente)Enum.Parse(typeof(EstadoCliente), lector["Estado"].ToString())
                    );
                    clientes.Add(cliente);
                }

                lector.Close();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al obtener los clientes: " + ex.Message);
            }
            finally
            {
                _conexion.CerrarConexion();
            }

            return clientes;
        }

        public void ActualizarEstadoCliente(string cedula, EstadoCliente nuevoEstado)
        {
            string query = $"UPDATE Clientes SET Estado = '{nuevoEstado}' WHERE Cedula = '{cedula}'";

            try
            {
                _conexion.AbrirConexion();
                MySqlCommand comando = new MySqlCommand(query, _conexion.Conexion);
                comando.ExecuteNonQuery();
                Console.WriteLine("Estado del cliente actualizado exitosamente.");
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al actualizar el estado del cliente: " + ex.Message);
            }
            finally
            {
                _conexion.CerrarConexion();
            }
        }
    }
}
