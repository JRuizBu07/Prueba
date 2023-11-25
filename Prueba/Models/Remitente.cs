using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [NotMapped]
    public class Remitente : Cliente
    {
     
        public Remitente(int id, string nombre, string apellido, string telefono, string direccion) : base(id, nombre, apellido, telefono, direccion)
        {
        }
        public Remitente() { }
    }
}
