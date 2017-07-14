using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Imposto.Core.Domain;

namespace Imposto.Core.Data
{
    public class NotaFiscalRepository
    {
        public void SalvarNotaFiscal(NotaFiscal notaFiscal)
        {
            String connStr = ConfigurationManager.ConnectionStrings["Base"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                if (conn.State != ConnectionState.Open)
                    conn.Open();

                SqlTransaction transaction = conn.BeginTransaction("Nota");

                try
                {
                    SqlCommand command =
                        new SqlCommand("dbo.P_NOTA_FISCAL", conn)
                        {
                            CommandType = CommandType.StoredProcedure,
                            Transaction = transaction
                        };
                    command.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int) {Direction = ParameterDirection.Output, Value = 0});
                    command.Parameters.Add(new SqlParameter("@pNumeroNotaFiscal", SqlDbType.Int)).Value =notaFiscal.NumeroNotaFiscal;
                    command.Parameters.Add(new SqlParameter("@pSerie", SqlDbType.Int)).Value = notaFiscal.Serie;
                    command.Parameters.Add(new SqlParameter("@pNomeCliente", SqlDbType.VarChar)).Value =notaFiscal.NomeCliente;
                    command.Parameters.Add(new SqlParameter("@pEstadoDestino", SqlDbType.VarChar)).Value =notaFiscal.EstadoDestino;
                    command.Parameters.Add(new SqlParameter("@pEstadoOrigem", SqlDbType.VarChar)).Value = notaFiscal.EstadoOrigem;
                    command.ExecuteNonQuery();

                    notaFiscal.Id = (int) command.Parameters["@pId"].Value;

                    foreach (NotaFiscalItem item in notaFiscal.ItensDaNotaFiscal)
                    {
                        SqlCommand itemComm =
                            new SqlCommand("dbo.P_NOTA_FISCAL_ITEM", conn)
                            {
                                CommandType = CommandType.StoredProcedure,
                                Transaction = transaction
                            };
                        itemComm.Parameters.Add(new SqlParameter("@pId", SqlDbType.Int) { Direction = ParameterDirection.Output, Value = 0 });
                        itemComm.Parameters.Add(new SqlParameter("@pIdNotaFiscal", SqlDbType.Int)).Value = notaFiscal.Id;
                        itemComm.Parameters.Add(new SqlParameter("@pCfop", SqlDbType.VarChar)).Value = item.Cfop;
                        itemComm.Parameters.Add(new SqlParameter("@pTipoIcms", SqlDbType.VarChar)).Value = item.TipoIcms;
                        itemComm.Parameters.Add(new SqlParameter("@pBaseIcms", SqlDbType.Decimal)).Value = item.BaseIcms;
                        itemComm.Parameters.Add(new SqlParameter("@pAliquotaIcms", SqlDbType.Decimal)).Value = item.AliquotaIcms;
                        itemComm.Parameters.Add(new SqlParameter("@pValorIcms", SqlDbType.Decimal)).Value = item.ValorIcms;
                        itemComm.Parameters.Add(new SqlParameter("@pBaseIpi", SqlDbType.Decimal)).Value = item.BaseIpi;
                        itemComm.Parameters.Add(new SqlParameter("@pAliquotaIpi", SqlDbType.Decimal)).Value = item.AliquotaIpi;
                        itemComm.Parameters.Add(new SqlParameter("@pValorIpi", SqlDbType.Decimal)).Value = item.ValorIpi;
                        itemComm.Parameters.Add(new SqlParameter("@pNomeProduto", SqlDbType.VarChar)).Value = item.NomeProduto;
                        itemComm.Parameters.Add(new SqlParameter("@pCodigoProduto", SqlDbType.VarChar)).Value = item.CodigoProduto;

                        itemComm.ExecuteNonQuery();

                        item.Id = (int)command.Parameters["@pId"].Value;
                    }

                    transaction.Commit();
                }
                catch (Exception e)
                {
                    transaction.Rollback("Nota");
                    throw new Exception(e.Message, e);
                }
            }
        }
    }
}
