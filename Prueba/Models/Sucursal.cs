using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [Table("Sucursal", Schema = "dbo")]
    public class Sucursal
    {
        [Key]
        [Column("sucursal_id")]
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }

        public virtual IList<FCT> Giros { get; set; }
        //Constructor

        public Sucursal()
        {

        }
        public Sucursal(int id, string nombre, string direccion)
        { 
            Id = id;
            Nombre = nombre;
            Direccion = direccion;
        }
    }
}
