using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [Table("Cliente", Schema = "dbo")]
    public class Cliente
    {

        [Key]
        [Required]
        [Column("cliente_id")]
        public int Id { get;  set; }

        public string Nombre { get;  set; }

        public string Apellido {  get;  set; }

        public  string Telefono {  get;  set; }

        public string Direccion { get;  set; }

        public Cliente(int id, string nombre, string apellido, string telefono, string direccion)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Telefono = telefono;
            Direccion = direccion;
        }

        public Cliente()
        { 
        }
    }
}
