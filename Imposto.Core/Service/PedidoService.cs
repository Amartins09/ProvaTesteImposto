using System;
using System.Collections.Generic;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    public class PedidoService
    {
        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal
            {
                NumeroNotaFiscal = 99999,
                Serie = new Random().Next(Int32.MaxValue),
                NomeCliente = pedido.NomeCliente,
                EstadoDestino = pedido.EstadoDestino,
                EstadoOrigem = pedido.EstadoOrigem
            };

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();

                //Recuperar o CFOP deacordo com os estados
                DadosFiscais dadosFiscais = new DadosFiscais();
                CategoriaFiscalEstado categoriaFiscal =
                    dadosFiscais.ValoresFiscais(notaFiscal.EstadoOrigem, notaFiscal.EstadoDestino);
                notaFiscalItem.Cfop = categoriaFiscal.Cfop;

                //Desconto
                double valorPedidoDesconto = itemPedido.ValorItemPedido;
                notaFiscalItem.Desconto = 0;
                if (ValidarRegiaoSudeste(notaFiscal.EstadoDestino))
                {
                    valorPedidoDesconto = itemPedido.ValorItemPedido * 0.90; //Desconto regiao Sudeste
                    notaFiscalItem.Desconto = 10;
                }

                //ICMS
                if (notaFiscal.EstadoDestino == notaFiscal.EstadoOrigem)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                }
                else
                {
                    notaFiscalItem.TipoIcms = "10";
                    notaFiscalItem.AliquotaIcms = 0.17;
                }

                notaFiscalItem.BaseIcms = notaFiscalItem.Cfop == "6.009"
                    ? valorPedidoDesconto * 0.90
                    : valorPedidoDesconto;

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;

                //IPI
                notaFiscalItem.BaseIpi = valorPedidoDesconto;
                notaFiscalItem.AliquotaIpi = 0.10;
                notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;

                //Brinde
                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                    notaFiscalItem.BaseIpi = 0;
                    notaFiscalItem.AliquotaIpi = 0;
                    notaFiscalItem.ValorIpi = 0;
                }

                //Produtos
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notaFiscal;
        }

        private Boolean ValidarRegiaoSudeste(string estadoDestino)
        {
            List<String> sudeste = new List<string> {"SP", "RJ", "ES", "MG"};

            return sudeste.Contains(estadoDestino);
        }
    }
}