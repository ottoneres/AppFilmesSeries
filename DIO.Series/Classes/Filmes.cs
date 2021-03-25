using System;

namespace DIO.Series
{
    public class Filmes : EntidadeBase
    {
        //Atributos

        private Genero Genero { get; set; }

        private Tipo Tipo { get; set; }

        private string Titulo { get; set; }

        private string Descricao { get; set; }

        private int Ano { get; set; }

        private bool Excluido { get; set; }

        // Métodos
        public Filmes(int id, Genero genero, Tipo tipo, string titulo, string descricao, int ano) 
        {
            this.Id = id;
            this.Genero = genero;
            this.Tipo = tipo;
            this.Titulo = titulo;
            this.Descricao = descricao;
            this.Ano = ano;
            this.Excluido = false;
        }

        public override string ToString()
        {
            string retorno = "";
            retorno += "Gênero: " + this.Genero + Environment.NewLine;
            retorno += "Tipo: " + this.Tipo + Environment.NewLine;
            retorno += "Título: " + this.Titulo + Environment.NewLine;
            retorno += "Descrição: " + this.Descricao + Environment.NewLine;
            retorno += "Ano: " + this.Ano + Environment.NewLine;
            retorno += "Excluído: " + this.Excluido;
            return retorno;
        }

        public string retornaTitulo()
        {
            return this.Titulo;
        }

        public int retornaId()
        {
            return this.Id;
        }

        public Tipo retornaTipo()
        {
            return this.Tipo;
        }

        public bool retornaExcluido()
        {
            return this.Excluido;
        }

        public void ExcluirFilme()
        {
            this.Excluido = true;
        }
    }
}