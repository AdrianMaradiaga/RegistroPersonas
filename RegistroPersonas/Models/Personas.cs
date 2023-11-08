using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace RegistroPersonas.Models
{
    public class Personas
    {
        [Key]
        public int ID { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public int Edad { get; set; }
        public string Correo { get; set; }
        public string Direccion { get; set; }
    }
}
