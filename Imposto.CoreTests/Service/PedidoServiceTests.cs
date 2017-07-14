using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Imposto.Core.Domain;

namespace Imposto.Core.Service.Tests
{
    [TestClass()]
    public class PedidoServiceTests
    {
        [TestMethod()]
        public void EmitirNotaFiscalTest()
        {
            given:
            Pedido pedido = FactoryPedido();

            when:
            PedidoService pedidoService = new PedidoService();
            NotaFiscal nota = pedidoService.EmitirNotaFiscal(pedido);

            then:
            Assert.AreEqual(pedido.EstadoDestino, nota.EstadoDestino);
            Assert.AreEqual(pedido.EstadoOrigem, nota.EstadoOrigem);
            Assert.AreEqual(pedido.NomeCliente, nota.NomeCliente);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void EmitirNotaFiscalTest_CfopInvalid()
        {
            given:
            Pedido pedido = FactoryPedido();
            pedido.EstadoOrigem = "AM";

            when:
            PedidoService pedidoService = new PedidoService();
            pedidoService.EmitirNotaFiscal(pedido);
        }

        [TestMethod()]
        public void EmitirNotaFiscalTest_Ipi()
        {
            given:
            Pedido pedido = FactoryPedido();
            pedido.ItensDoPedido[0].Brinde = true;
            pedido.ItensDoPedido[1].Brinde = false;

            when:
            PedidoService pedidoService = new PedidoService();
            NotaFiscal nota = pedidoService.EmitirNotaFiscal(pedido);

            then:
            Assert.AreEqual(pedido.EstadoDestino, nota.EstadoDestino);
            Assert.AreEqual(pedido.EstadoOrigem, nota.EstadoOrigem);
            Assert.AreEqual(pedido.NomeCliente, nota.NomeCliente);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);

            Assert.AreEqual(0, nota.ItensDaNotaFiscal[0].BaseIpi);
            Assert.AreEqual(0, nota.ItensDaNotaFiscal[0].AliquotaIpi);
            Assert.AreEqual(0, nota.ItensDaNotaFiscal[0].ValorIpi);

            Assert.IsTrue(nota.ItensDaNotaFiscal[1].BaseIpi > 0);
            Assert.IsTrue(nota.ItensDaNotaFiscal[1].AliquotaIpi > 0);
            Assert.IsTrue(nota.ItensDaNotaFiscal[1].ValorIpi > 0);
        }

        [TestMethod()]
        public void EmitirNotaFiscalTest_SemDesconto()
        {
            given:
            Pedido pedido = FactoryPedido();
            pedido.EstadoDestino = "PB";

            when:
            PedidoService pedidoService = new PedidoService();
            NotaFiscal nota = pedidoService.EmitirNotaFiscal(pedido);

            then:
            Assert.AreEqual(pedido.EstadoDestino, nota.EstadoDestino);
            Assert.AreEqual(pedido.EstadoOrigem, nota.EstadoOrigem);
            Assert.AreEqual(pedido.NomeCliente, nota.NomeCliente);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);

            Assert.AreEqual(pedido.ItensDoPedido[0].ValorItemPedido, nota.ItensDaNotaFiscal[0].BaseIcms);
            Assert.AreEqual(pedido.ItensDoPedido[0].ValorItemPedido, nota.ItensDaNotaFiscal[0].BaseIpi);

            Assert.AreEqual(pedido.ItensDoPedido[1].ValorItemPedido, nota.ItensDaNotaFiscal[1].BaseIcms);
            Assert.AreEqual(pedido.ItensDoPedido[1].ValorItemPedido, nota.ItensDaNotaFiscal[1].BaseIpi);
        }

        [TestMethod()]
        public void EmitirNotaFiscalTest_ComDesconto()
        {
            given:
            Pedido pedido = FactoryPedido();
            pedido.EstadoDestino = "RJ";

            when:
            PedidoService pedidoService = new PedidoService();
            NotaFiscal nota = pedidoService.EmitirNotaFiscal(pedido);

            then:
            Assert.AreEqual(pedido.EstadoDestino, nota.EstadoDestino);
            Assert.AreEqual(pedido.EstadoOrigem, nota.EstadoOrigem);
            Assert.AreEqual(pedido.NomeCliente, nota.NomeCliente);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);

            Assert.AreEqual((pedido.ItensDoPedido[0].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[0].BaseIcms);
            Assert.AreEqual((pedido.ItensDoPedido[0].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[0].BaseIpi);

            Assert.AreEqual((pedido.ItensDoPedido[1].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[1].BaseIcms);
            Assert.AreEqual((pedido.ItensDoPedido[1].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[1].BaseIpi);
        }

        [TestMethod()]
        public void EmitirNotaFiscalTest_DescontoCfop_6009()
        {
            given:
            Pedido pedido = FactoryPedido();
            pedido.EstadoDestino = "SP";

            when:
            PedidoService pedidoService = new PedidoService();
            NotaFiscal nota = pedidoService.EmitirNotaFiscal(pedido);

            then:
            Assert.AreEqual(pedido.EstadoDestino, nota.EstadoDestino);
            Assert.AreEqual(pedido.EstadoOrigem, nota.EstadoOrigem);
            Assert.AreEqual(pedido.NomeCliente, nota.NomeCliente);
            Assert.AreEqual(pedido.ItensDoPedido.Count, nota.ItensDaNotaFiscal.Count);

            Assert.AreEqual(((pedido.ItensDoPedido[0].ValorItemPedido * 0.90) * 0.90), nota.ItensDaNotaFiscal[0].BaseIcms);
            Assert.AreEqual((pedido.ItensDoPedido[0].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[0].BaseIpi);

            Assert.AreEqual(((pedido.ItensDoPedido[1].ValorItemPedido * 0.90) * 0.90), nota.ItensDaNotaFiscal[1].BaseIcms);
            Assert.AreEqual((pedido.ItensDoPedido[1].ValorItemPedido * 0.90), nota.ItensDaNotaFiscal[1].BaseIpi);
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
    }
}