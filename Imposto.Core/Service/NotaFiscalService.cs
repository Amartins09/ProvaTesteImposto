using System;
using System.IO;
using System.Xml.Serialization;
using Imposto.Core.Data;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public class NotaFiscalService
    {
        public void GerarNotaFiscal(Pedido pedido)
        {
            PedidoService pedidoService = new PedidoService();
            NotaFiscal notaFiscal = pedidoService.EmitirNotaFiscal(pedido);
            GravarXmlNotaFiscal(notaFiscal);
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(notaFiscal);
        }

        private void GravarXmlNotaFiscal(NotaFiscal notaFiscal)
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