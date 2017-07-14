using System;
using System.Collections.Generic;
using System.Linq;

namespace Imposto.Core.Domain
{
    class DadosFiscais
    {
        private readonly List<CategoriaFiscalEstado> _dados;

        public DadosFiscais()
        {
            _dados = new List<CategoriaFiscalEstado>
            {
                //Origem SP
                new CategoriaFiscalEstado("SP", "RJ", "6.000"),
                new CategoriaFiscalEstado("SP", "PE", "6.001"),
                new CategoriaFiscalEstado("SP", "MG", "6.002"),
                new CategoriaFiscalEstado("SP", "PB", "6.003"),
                new CategoriaFiscalEstado("SP", "PR", "6.004"),
                new CategoriaFiscalEstado("SP", "PI", "6.005"),
                new CategoriaFiscalEstado("SP", "RO", "6.006"),
                new CategoriaFiscalEstado("SP", "SE", "6.007"),
                new CategoriaFiscalEstado("SP", "TO", "6.008"),
                new CategoriaFiscalEstado("SP", "SP", "6.009"),
                new CategoriaFiscalEstado("SP", "PA", "6.010"),

                //Origem MG
                new CategoriaFiscalEstado("MG", "RJ", "6.000"),
                new CategoriaFiscalEstado("MG", "PE", "6.001"),
                new CategoriaFiscalEstado("MG", "MG", "6.002"),
                new CategoriaFiscalEstado("MG", "PB", "6.003"),
                new CategoriaFiscalEstado("MG", "PR", "6.004"),
                new CategoriaFiscalEstado("MG", "PI", "6.005"),
                new CategoriaFiscalEstado("MG", "RO", "6.006"),
                new CategoriaFiscalEstado("MG", "SE", "6.007"),
                new CategoriaFiscalEstado("MG", "TO", "6.008"),
                new CategoriaFiscalEstado("MG", "SP", "6.009"),
                new CategoriaFiscalEstado("MG", "PA", "6.010")
            };
        }

        public CategoriaFiscalEstado ValoresFiscais(string estadoOrigem, string estadoDestino)
        {
            CategoriaFiscalEstado categoria =
                _dados.FirstOrDefault(x => x.EstadoDestino.Equals(estadoDestino) && x.EstadoOrigen.Equals(estadoOrigem));

            if (categoria == null)
                throw new Exception("Não foi localizado uma categoria fiscal para esses estados");

            return categoria;
        }
    }
}
