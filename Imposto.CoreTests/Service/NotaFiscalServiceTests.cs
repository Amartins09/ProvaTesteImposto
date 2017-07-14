using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using Imposto.Core.Domain;

namespace Imposto.Core.Service.Tests
{
    [TestClass()]
    public class NotaFiscalServiceTests
    {
        [TestMethod()]
        public void GerarNotaFiscalTest()
        {
            given:
            Pedido pedido = FactoryPedido();

            when:
            NotaFiscalService notaFiscalService = new NotaFiscalService();
            NotaFiscal nota = notaFiscalService.GerarNotaFiscal(pedido);

            then:
            Assert.IsTrue(File.Exists(PathFileXml(nota)));
            Assert.IsTrue(nota.Id > 0);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);
        }

        private Pedido FactoryPedido()
        {
            List<PedidoItem> listItens = new List<PedidoItem>();
            PedidoItem item1 = new PedidoItem
            {
                NomeProduto = "Produto Teste",
                CodigoProduto = "123456",
                Brinde = false,
                ValorItemPedido = 10
            };
            PedidoItem item2 = new PedidoItem
            {
                NomeProduto = "Produto Teste",
                CodigoProduto = "123456",
                Brinde = false,
                ValorItemPedido = 10
            };

            listItens.Add(item1);
            listItens.Add(item2);

            Pedido pedido = new Pedido
            {
                EstadoDestino = "MG",
                EstadoOrigem = "SP",
                NomeCliente = "Cliente Teste",
                ItensDoPedido = listItens
            };

            return pedido;
        }

        private string PathFileXml(NotaFiscal notaFiscal)
        {
            String path = Environment.GetEnvironmentVariable("pathXml");
            return path + notaFiscal.Serie + ".xml";
        }
    }
}