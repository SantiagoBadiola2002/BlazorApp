﻿namespace BlazorApp.Models
{
    public class PuestoAtencion
    {
        public int Id { get; set; } 
        public int Numero { get; set; } 
        public bool EstaLibre { get; set; } // Estado del puesto (libre o ocupado)
        public Operario OperarioAsignado { get; set; }
        public String OficinaId { get; set; }

        public void Liberar()
        {
            EstaLibre = true;
        }

        public void Ocupar(Operario operario)
        {
            OperarioAsignado = operario;
            EstaLibre = false;
        }
    }

}
