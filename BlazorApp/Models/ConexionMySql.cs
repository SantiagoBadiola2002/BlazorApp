using MySql.Data.MySqlClient;

namespace BlazorApp.Models
{
    public class ConexionMySql
    {
        private MySqlConnection conexion;

        // Propiedad pública para acceder a la conexión
        public MySqlConnection Conexion
        {
            get { return conexion; }
        }

        public ConexionMySql()
        {
            string connectionString = "Server=localhost;Database=proyectoblazor;User ID=root;Password=;";
            try
            {
                conexion = new MySqlConnection(connectionString);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al inicializar la conexión: " + ex.Message);
            }
        }

        public void AbrirConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Closed)
                {
                    conexion.Open();
                    Console.WriteLine("Conexión abierta exitosamente.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al abrir la conexión: " + ex.Message);
            }
        }

        public void CerrarConexion()
        {
            try
            {
                if (conexion.State == System.Data.ConnectionState.Open)
                {
                    conexion.Close();
                    Console.WriteLine("Conexión cerrada exitosamente.");
                }
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al cerrar la conexión: " + ex.Message);
            }
        }

        public MySqlDataReader EjecutarConsulta(string consulta)
        {
            MySqlCommand comando = new MySqlCommand(consulta, conexion);

            try
            {
                AbrirConexion();
                MySqlDataReader lector = comando.ExecuteReader();
                return lector;  // El lector debe ser procesado en el código que lo llame.
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error al ejecutar la consulta: " + ex.Message);
                return null;
            }
        }
    }
}
