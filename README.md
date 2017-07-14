# Prova teste imposto

Reporte Técnico

# Alterações realizadas:
•	Ajuste na tela para utilizar comboBox no lugar dos text para selecionar os estados de origem e destino para gerar as notas.<br>
•	Tratamento nos itens do grid e campos da tela.
•	Gravar as informações da nota fiscal em XML.
•	Tratamento e correção nos cálculos dos impostos, inclusão do calculo de IPI e desconto para região sudeste.
•	Incluir e salvar os dados da nota fiscal no banco de dados (MS SQLServe)
•	Recuperar o endereço onde será salvo os arquivos XML da variável de ambiente ( “pathXml” )
•	Inclusão dos testes unitários

# Objetivo da tela:
•	Permitir a inclusão do pedido com seus itens, a partir desses dados e feito o calculo dos impostos (ICMS, IPI) e descontos de acordo com a região e/ou CFOP. 
•	Com os impostos calculados é gerado um XML com os dados da nota e gravado no banco de dados.

# Observação:
•	Todas as alterações tiveram como objetivo atender as solicitações e facilitar futuras melhorias no aplicativo.
•	Criação do serviço PedidoServico, para tratar o processo de emissão de nota fiscal, que é baseado no pedido digitado.
•	Criado as entidades DadosFiscais e CategoriaFiscalEstado, para permitir um cadastro mais limpo dos CFOPs por estado de origem e destino.
•	Novo script para a criação dos campos de IPI e alteração das procedures para cadastro os mesmos.
•	Nova procedure para apresentar a somatória dos impostos ( ICMS e IPI ) de todas as notas salvas no banco de dados
•	Inclusão do projeto para realização de testes unitários do projeto Imposto.Core.
