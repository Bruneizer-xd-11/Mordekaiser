using System.ComponentModel.DataAnnotations;

namespace Mordekaiser.Core
{
    public class Servidor
    {
        [Required(ErrorMessage = "Debe poner un nombre.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "Debe poner una abreviación.")]
        [StringLength(10, ErrorMessage = "La abreviación no puede superar los 10 caracteres.")]
        public string? Abreviado { get; set; }

        public byte idServidor { get; set; }
    }
}