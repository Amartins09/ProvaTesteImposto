using System;
using System.IO;
using System.Xml.Serialization;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Domain.Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
            GravarNotaFiscal(notaFiscal);
        }

        private void GravarNotaFiscal(NotaFiscal notaFiscal)
        {
            String path = "" + notaFiscal.NumeroNotaFiscal + ".xml";

            XmlSerializer xmlNota = new XmlSerializer(typeof(NotaFiscal));
            StreamWriter arquivo = new StreamWriter(path);
            xmlNota.Serialize(arquivo, notaFiscal);
            arquivo.Close();
        }
    }
}
