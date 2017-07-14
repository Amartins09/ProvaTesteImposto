using Imposto.Core.Service;
using System;
using System.Data;
using System.Windows.Forms;
using Imposto.Core.Domain;

namespace TesteImposto
{
    public partial class FormImposto : Form
    {
        private Pedido _pedido = new Pedido();

        public FormImposto()
        {
            InitializeComponent();
            dataGridViewPedidos.AutoGenerateColumns = true;                       
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }

        private void ResizeColumns()
        {
            double mediaWidth = dataGridViewPedidos.Width / dataGridViewPedidos.Columns.GetColumnCount(DataGridViewElementStates.Visible);

            for (int i = dataGridViewPedidos.Columns.Count - 1; i >= 0; i--)
            {
                var coluna = dataGridViewPedidos.Columns[i];
                coluna.Width = Convert.ToInt32(mediaWidth);
            }   
        }

        private object GetTablePedidos()
        {
            DataTable table = new DataTable("pedidos");
            table.Columns.Add(new DataColumn("Nome do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Codigo do produto", typeof(string)));
            table.Columns.Add(new DataColumn("Valor", typeof(decimal)) {DefaultValue = 0.0});
            table.Columns.Add(new DataColumn("Brinde", typeof(bool)) {DefaultValue = false});

            return table;
        }

        private void buttonGerarNotaFiscal_Click(object sender, EventArgs e)
        {            
            NotaFiscalService service = new NotaFiscalService();
            if (cmbEstadoOrigem.SelectedIndex == -1 || cmbEstadoDestino.SelectedIndex == -1)
            {
                MessageBox.Show("Selecione os estados");
            }
            else
            {
                _pedido.EstadoOrigem = cmbEstadoOrigem.Text;
                _pedido.EstadoDestino = cmbEstadoDestino.Text;
                _pedido.NomeCliente = textBoxNomeCliente.Text;

                DataTable table = (DataTable) dataGridViewPedidos.DataSource;

                foreach (DataRow row in table.Rows)
                {
                    _pedido.ItensDoPedido.Add(
                        new PedidoItem()
                        {
                            Brinde = Convert.ToBoolean(row["Brinde"]),
                            CodigoProduto = row["Codigo do produto"].ToString(),
                            NomeProduto = row["Nome do produto"].ToString(),
                            ValorItemPedido = Convert.ToDouble(row["Valor"].ToString())
                        });
                }

                try
                {
                    service.GerarNotaFiscal(_pedido);
                    MessageBox.Show("Operação efetuada com sucesso");
                    LimparTela();
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message);
                }
            }
        }

        private void LimparTela()
        {
            textBoxNomeCliente.Text = String.Empty;
            cmbEstadoOrigem.SelectedIndex = -1;
            cmbEstadoDestino.SelectedIndex = -1;
            dataGridViewPedidos.DataSource = GetTablePedidos();
            ResizeColumns();
        }
    }
}
