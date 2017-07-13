using System;
using System.IO;
using System.Xml.Serialization;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();
            notaFiscal.EmitirNotaFiscal(pedido);
            GravarNotaFiscal(notaFiscal);
        }

        private void GravarNotaFiscal(NotaFiscal notaFiscal)
        {
            String path = Environment.GetEnvironmentVariable("pathXml");
            if (string.Equals(path, null))
                throw new Exception("Não foi configurado o path para gravar o XML da nota.");


            XmlSerializer xmlNota = new XmlSerializer(typeof(NotaFiscal));
            StreamWriter arquivo = new StreamWriter(path + notaFiscal.Serie + ".xml");
            xmlNota.Serialize(arquivo, notaFiscal);
            arquivo.Close();
        }
    }
}
