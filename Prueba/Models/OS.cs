using System.ComponentModel.DataAnnotations.Schema;

namespace Prueba.Models
{
    [Table("OS", Schema = "dbo")]
    public class OS : Documento
    {
        public OS(int codigo, string tipoDocumento, int valor, DateTime fechaRecaudo, int sucursalId, int clienteId,  int clienteEmpresarial) :  base(codigo, tipoDocumento, valor, fechaRecaudo, sucursalId, clienteId)
        {
            ClienteEmpresarial = clienteEmpresarial;
        }

        public int ClienteEmpresarial { get; private set; }
        
    }
}
