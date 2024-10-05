namespace BlazorApp.Models
{
    public class Administrador
    {
        public int Id { get; set; } 
        public string Nombre { get; set; } 

        public void AgregarOficina(Oficina oficina)
        {
            // Lógica para agregar una nueva oficina al sistema
        }

        public void EliminarOficina(int oficinaId)
        {
            // Lógica para eliminar una oficina del sistema
        }

        public void ActualizarOficina(Oficina oficina)
        {
            // Lógica para actualizar los datos de una oficina
        }
    }

}
