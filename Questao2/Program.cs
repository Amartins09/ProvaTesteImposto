using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidarStream
{
    class Program
    {
        static void Main(string[] args)
        {
            string retorno = "";

            Console.WriteLine("Entrar com a string para a validação se existe uma vogal após uma consoante que não se repete.");
            string linha = Console.ReadLine();

            Console.WriteLine("--------------------------------------------");
            ValidarString validar = new ValidarString(linha);
            char result = FirstChar(validar);
            if (result == ' ')
                retorno = "Não foi localizada nenhuma vogal após uma consoante.";
            else
                retorno = "Output: " + result;
            Console.WriteLine(retorno);

            Console.WriteLine("--------------------------------------------");
            Console.WriteLine("Pressione uma tecla para sair ...");
            Console.ReadKey();
        }

        public static char FirstChar(ValidarString input)
        {
            if (input.hasNext())
                throw new Exception("Não foi informado uma string valida");

            List<char?> listVogal = new List<char?> { 'a', 'e', 'i', 'o', 'u', 'A', 'E', 'I', 'O', 'U' };

            char? vogal = null;
            char? anterior = null;

            do
            {
                char letra = input.getNext();

                if (listVogal.Contains(letra))
                {
                    if (anterior != null && !listVogal.Contains(anterior) && Char.IsLetter(anterior.ToString(), 0))
                        vogal = letra;
                }

                int count = input.getText().Count(x => x.Equals(letra.ToString().ToUpper().ToCharArray()[0]));

                if (vogal != null && count == 1)
                    return letra;
                else
                    vogal = null;

                anterior = letra;
            } while (!input.hasNext());

            return ' ';
        }
    }
}
