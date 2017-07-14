using System.Collections.Generic;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    [XmlRoot]
    public class NotaFiscal
    {
        [XmlIgnore]
        public int Id { get; set; }
        public int NumeroNotaFiscal { get; set; }
        public int Serie { get; set; }
        public string NomeCliente { get; set; }

        public string EstadoDestino { get; set; }
        public string EstadoOrigem { get; set; }

        [XmlElement]
        public List<NotaFiscalItem> ItensDaNotaFiscal { get; set; }

        public NotaFiscal()
        {
            ItensDaNotaFiscal = new List<NotaFiscalItem>();
        }
    }
}
