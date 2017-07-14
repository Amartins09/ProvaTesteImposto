using System;
using System.Collections.Generic;
using Imposto.Core.Domain;

namespace Imposto.Core.Service
{
    class PedidoService
    {
        public NotaFiscal EmitirNotaFiscal(Pedido pedido)
        {
            NotaFiscal notaFiscal = new NotaFiscal();

            notaFiscal.NumeroNotaFiscal = 99999;
            notaFiscal.Serie = new Random().Next(Int32.MaxValue);
            notaFiscal.NomeCliente = pedido.NomeCliente;

            notaFiscal.EstadoDestino = pedido.EstadoDestino;
            notaFiscal.EstadoOrigem = pedido.EstadoOrigem;

            foreach (PedidoItem itemPedido in pedido.ItensDoPedido)
            {
                NotaFiscalItem notaFiscalItem = new NotaFiscalItem();
                if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "RJ")
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "PE")
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "MG")
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "PB")
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "PR")
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "PI")
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "RO")
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "SE")
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "TO")
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "SE")
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if (notaFiscal.EstadoOrigem == "SP" && notaFiscal.EstadoDestino == "PA")
                {
                    notaFiscalItem.Cfop = "6.010";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "RJ")
                {
                    notaFiscalItem.Cfop = "6.000";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "PE")
                {
                    notaFiscalItem.Cfop = "6.001";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "MG")
                {
                    notaFiscalItem.Cfop = "6.002";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "PB")
                {
                    notaFiscalItem.Cfop = "6.003";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "PR")
                {
                    notaFiscalItem.Cfop = "6.004";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "PI")
                {
                    notaFiscalItem.Cfop = "6.005";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "RO")
                {
                    notaFiscalItem.Cfop = "6.006";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "SE")
                {
                    notaFiscalItem.Cfop = "6.007";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "TO")
                {
                    notaFiscalItem.Cfop = "6.008";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "SE")
                {
                    notaFiscalItem.Cfop = "6.009";
                }
                else if (notaFiscal.EstadoOrigem == "MG" && notaFiscal.EstadoDestino == "PA")
                {
                    notaFiscalItem.Cfop = "6.010";
                }

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

                double valorPedidoDesconto = itemPedido.ValorItemPedido;
                notaFiscalItem.Desconto = 0;
                if (ValidarRegiaoSudeste(notaFiscal.EstadoDestino))
                {
                    valorPedidoDesconto = itemPedido.ValorItemPedido * 0.10; //Desconto regiao Sudeste
                    notaFiscalItem.Desconto = 10;
                }

                if (notaFiscalItem.Cfop == "6.009")
                {
                    notaFiscalItem.BaseIcms = valorPedidoDesconto * 0.90; //redução de base
                }
                else
                {
                    notaFiscalItem.BaseIcms = valorPedidoDesconto;
                }

                notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                notaFiscalItem.BaseIpi = valorPedidoDesconto;

                if (itemPedido.Brinde)
                {
                    notaFiscalItem.TipoIcms = "60";
                    notaFiscalItem.AliquotaIcms = 0.18;
                    notaFiscalItem.ValorIcms = notaFiscalItem.BaseIcms * notaFiscalItem.AliquotaIcms;
                    notaFiscalItem.AliquotaIpi = 0;
                    notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
                }
                else
                {
                    notaFiscalItem.AliquotaIpi = 0.10;
                    notaFiscalItem.ValorIpi = notaFiscalItem.BaseIpi * notaFiscalItem.AliquotaIpi;
                }
                notaFiscalItem.NomeProduto = itemPedido.NomeProduto;
                notaFiscalItem.CodigoProduto = itemPedido.CodigoProduto;

                notaFiscal.ItensDaNotaFiscal.Add(notaFiscalItem);
            }

            return notaFiscal;
        }

        public Boolean ValidarRegiaoSudeste(string EstadoDestino)
        {
            List<String> sudeste = new List<string> { "SP", "SP", "RJ", "ES", "MG" };

            return sudeste.Contains(EstadoDestino);
        }
    }
}
