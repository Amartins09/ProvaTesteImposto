# Prova teste imposto

Reporte Técnico

# Alterações realizadas:
•	Ajuste na tela para utilizar comboBox no lugar dos text para selecionar os estados de origem e destino para gerar as notas.<br>
•	Tratamento nos itens do grid e campos da tela.<br>
•	Gravar as informações da nota fiscal em XML.<br>
•	Tratamento e correção nos cálculos dos impostos, inclusão do calculo de IPI e desconto para região sudeste.<br>
•	Incluir e salvar os dados da nota fiscal no banco de dados (MS SQLServe)<br>
•	Recuperar o endereço onde será salvo os arquivos XML da variável de ambiente ( “pathXml” )<br>
•	Inclusão dos testes unitários<br><br>

# Objetivo da tela:
•	Permitir a inclusão do pedido com seus itens, a partir desses dados e feito o calculo dos impostos (ICMS, IPI) e descontos de acordo com a região e/ou CFOP.<br>
•	Com os impostos calculados é gerado um XML com os dados da nota e gravado no banco de dados.<br><br>

# Observação:
•	Todas as alterações tiveram como objetivo atender as solicitações e facilitar futuras melhorias no aplicativo.<br>
•	Criação do serviço PedidoServico, para tratar o processo de emissão de nota fiscal, que é baseado no pedido digitado.<br>
•	Criado as entidades DadosFiscais e CategoriaFiscalEstado, para permitir um cadastro mais limpo dos CFOPs por estado de origem e destino.<br>
•	Novo script para a criação dos campos de IPI e alteração das procedures para cadastro os mesmos.<br>
•	Nova procedure para apresentar a somatória dos impostos ( ICMS e IPI ) de todas as notas salvas no banco de dados<br>
•	Inclusão do projeto para realização de testes unitários do projeto Imposto.Core.<br>
