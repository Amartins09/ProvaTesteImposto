namespace Imposto.Core.Domain
{
    class CategoriaFiscalEstado
    {
        public string EstadoOrigen;
        public string EstadoDestino;
        public string Cfop;

        public CategoriaFiscalEstado(string estadoOrigen, string estadoDestino, string cfop)
        {
            EstadoDestino = estadoDestino;
            EstadoOrigen = estadoOrigen;
            Cfop = cfop;
        }
    }
}