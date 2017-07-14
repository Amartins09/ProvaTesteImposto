using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Imposto.Core.Domain;

namespace Imposto.Core.Data.Tests
{
    [TestClass()]
    public class NotaFiscalRepositoryTests
    {
        [TestMethod()]
        public void SalvarNotaFiscalTest()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            var result = repository.SalvarNotaFiscal(nota);

            then:
            Assert.IsNotNull(result.Id);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SalvarNotaFiscalTest_Destino_Null()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();
            nota.EstadoDestino = null;

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(nota);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SalvarNotaFiscalTest_Origem_Null()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();
            nota.EstadoOrigem = null;

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(nota);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SalvarNotaFiscalTest_NomeCliente_Null()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();
            nota.NomeCliente = null;

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(nota);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SalvarNotaFiscalTest_ItemCfop_Null()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();
            nota.ItensDaNotaFiscal[0].Cfop = null;

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(nota);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exception))]
        public void SalvarNotaFiscalTest_ItemTipoIcms_Null()
        {
            given:
            NotaFiscal nota = FactoryNotaFiscal();
            nota.ItensDaNotaFiscal[0].TipoIcms = null;

            when:
            NotaFiscalRepository repository = new NotaFiscalRepository();
            repository.SalvarNotaFiscal(nota);
        }

        private NotaFiscal FactoryNotaFiscal()
        {
            List<NotaFiscalItem> listItem = new List<NotaFiscalItem>();
            NotaFiscalItem item1 = new NotaFiscalItem
            {
                NomeProduto = "Produto Teste",
                CodigoProduto = "123456",
                Cfop = "1.001",
                TipoIcms = "10",
                BaseIcms = 10,
                AliquotaIcms = 10,
                ValorIcms = 1,
                BaseIpi = 10,
                AliquotaIpi = 0,
                ValorIpi = 0,
                Desconto = 10
            };
            NotaFiscalItem item2 = new NotaFiscalItem
            {
                NomeProduto = "Produto Teste",
                CodigoProduto = "123456",
                Cfop = "1.001",
                TipoIcms = "10",
                BaseIcms = 20,
                AliquotaIcms = 10,
                ValorIcms = 2,
                BaseIpi = 20,
                AliquotaIpi = 0,
                ValorIpi = 0,
                Desconto = 10
            };

            listItem.Add(item1);
            listItem.Add(item2);

            NotaFiscal notaFiscal = new NotaFiscal
            {
                NomeCliente = "Cliente Teste",
                EstadoOrigem = "SP",
                EstadoDestino = "MG",
                NumeroNotaFiscal = 88888,
                Serie = new Random().Next(Int32.MaxValue),
                ItensDaNotaFiscal = listItem
            };

            return notaFiscal;
        }
    }
}