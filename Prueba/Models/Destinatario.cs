using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [NotMapped]
    public class Destinatario : Cliente
    {
        public IList<FCT> Giros { get; set; }
        public Destinatario(int id, string nombre, string apellido, string telefono, string direccion) : base(id, nombre, apellido, telefono, direccion)
        {
        }
        public Destinatario() { }
    }
}
