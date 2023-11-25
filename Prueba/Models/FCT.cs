using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [Table("FCT", Schema ="dbo")]
    public class FCT : Documento
    {

        [ForeignKey("Cliente")]
        [Column("destinatario_id")]
        public int DestinatarioId { get;  set; }

        [ForeignKey("Sucursal")]
        [Column("sucursal_destino_id")]
        public int IdSucursalDestino { get;  set; }

        public virtual Sucursal? SucursalDestino { get; set; }

        public virtual Cliente? Destinatario { get; set; }

        

        public FCT(int codigo, string tipoDocumento, int valor, DateTime fechaRecaudo, int sucursalId, int clienteId, int destinatarioId, int idSucursalDestino) : base(codigo, tipoDocumento, valor, fechaRecaudo, sucursalId, clienteId)
        {
            DestinatarioId = destinatarioId;
            IdSucursalDestino = idSucursalDestino;
        }

        public FCT()
        {

        }

    }
}
