﻿namespace RinconGuatemaltecoApp.Models
{
    public class Cliente
    {
        public int ClienteID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Direccion { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}
