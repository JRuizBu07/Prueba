using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [Table("Documento", Schema = "dbo")]
    public class Documento
    {


        [Key]
        [Required]
        public int Codigo { get; set; }

        public string? TipoDocumento { get; set; }

        public int Valor { get; set; }

        public DateTime FechaRecaudo { get; set; }

        [Column("sucursal_id")]
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }

        public virtual Sucursal? SucursalOrigen { get; set; }

        [Column("cliente_id")]
        [ForeignKey("Cliente")]
        public int ClienteId { get; set; }

        public virtual Cliente? Remitente { get; set; }



        public Documento(int codigo, string tipoDocumento, int valor, DateTime fechaRecaudo, int sucursalId, int clienteId )
        {
            Codigo = codigo;
            TipoDocumento = tipoDocumento;
            Valor = valor;
            FechaRecaudo = fechaRecaudo;
            SucursalId = sucursalId;
            ClienteId = clienteId;

        }

        public Documento() { }
    }
}
