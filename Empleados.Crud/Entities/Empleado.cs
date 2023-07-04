using System.ComponentModel.DataAnnotations;

namespace Empleados.Crud.Entities
{
    public class Empleado
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(255)]
        public string Nombre { get; set; }

        [MaxLength(50)]
        public string Correo { get; set; }
    }
}
