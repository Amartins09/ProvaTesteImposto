using System;
using System.Xml.Serialization;

namespace Imposto.Core.Domain
{
    [XmlRoot]
    public class NotaFiscalItem
    {
        [XmlIgnore]
        public int Id { get; set; }
        [XmlIgnore]
        public int IdNotaFiscal { get; set; }
        public string NomeProduto { get; set; }
        public string CodigoProduto { get; set; }
        public string Cfop { get; set; }
        public string TipoIcms { get; set; }
        public double BaseIcms { get; set; }
        public double AliquotaIcms { get; set; }
        public double ValorIcms { get; set; }
        public double BaseIpi { get; set; }
        public double AliquotaIpi { get; set; }
        public double ValorIpi { get; set; }
        public double Desconto { get; set; }
    }
}
