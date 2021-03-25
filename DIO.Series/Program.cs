using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static FilmeRepositorio repositorioFilme = new FilmeRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarTitulos();
                        break;
                    case "2":
                        InserirTitulo();
                        break;
                    case "3":
                        AtualizarTitulo();
                        break;
                    case "4":
                        ExcluirTitulo();
                        break;
                    case "5":
                        VisualizarTitulo();
                        break;
                    case "C":
                        Console.Clear();
                        break;
                    
                    default:
                        Console.WriteLine("OPÇÃO INVÁLIDA! Tente novamente!");
                        break;

                }

                opcaoUsuario = ObterOpcaoUsuario();
            }
        }


        private static void VisualizarTitulo()
        {
            int tipoItem = definirTipo();
            
            Console.Write("Digite o ID do Título: ");
            int indiceSerie = int.Parse(Console.ReadLine());


            if (tipoItem == 2)
            {
                var serie = repositorio.RetornaPorId(indiceSerie);

                Console.WriteLine(serie);
            }
            else
            {
                var filme = repositorioFilme.RetornaPorId(indiceSerie);

                Console.WriteLine(filme);
            }

        }
        private static void AtualizarTitulo()
        {
            int tipoItem = definirTipo();

            Console.Write("Digite o ID do Título: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            if (tipoItem == 2 && indiceSerie >= repositorio.ProximoId())
            {
                Console.WriteLine("Série não encontrada");
            }
            else if (tipoItem == 1 && indiceSerie >= repositorioFilme.ProximoId())
            {
                Console.WriteLine("Filme não encontrado");
            }
            else
            {

                string nomeSerie, descricaoSerie;
                int generoSerie, anoSerie;
                
                ColetaDadosEntrada(out nomeSerie, out generoSerie, out anoSerie, out descricaoSerie);

                    if (tipoItem == 2)
                    {

                        Series serieAtualizada = new Series(id: indiceSerie, 
                                                        genero: (Genero)generoSerie,
                                                        tipo: (Tipo)tipoItem,
                                                        titulo: nomeSerie,
                                                        descricao: descricaoSerie,
                                                        ano: anoSerie);

                        repositorio.Atualiza(indiceSerie, serieAtualizada);
                    }
                    else
                    {
                        Filmes filmeInserido = new Filmes(id: indiceSerie, 
                                                        genero: (Genero)generoSerie,
                                                        tipo: (Tipo)tipoItem,
                                                        titulo: nomeSerie,
                                                        descricao: descricaoSerie,
                                                        ano: anoSerie);

                        repositorioFilme.Atualiza(indiceSerie, filmeInserido);
                    }
            }
        }
        private static void ExcluirTitulo()
        {
            Console.WriteLine("Excluir um Título");
            Console.WriteLine();

            int tipoItem = definirTipo();

            Console.Write("Digite o ID do título que você quer excluir: ");
            int entradaID = Convert.ToInt32(Console.ReadLine());

            if (tipoItem == 2 && entradaID >= repositorio.ProximoId())
            {
                Console.WriteLine("Série não encontrada");
            }
            else if (tipoItem == 1 && entradaID >= repositorioFilme.ProximoId())
            {
                Console.WriteLine("Filme não encontrado");
            }
            else
            {
                Console.Write("Tem certeza? S/N");
                string entradaConfirma = Console.ReadLine();

                if (tipoItem == 2 && entradaConfirma.Contains('S'))
                {
                    repositorio.Exclui(entradaID);
                }
                else if (tipoItem == 1 && entradaConfirma.Contains('S'))
                {
                    repositorioFilme.Exclui(entradaID);
                }
                else
                {
                    Console.WriteLine("O Título não foi excluído.");
                }
            }
        }

        private static void ListarTitulos()
        {
            Console.WriteLine("Listar títulos");

            var lista = repositorio.Lista();
            var listaFilme = repositorioFilme.Lista();

            if (lista.Count == 0 && listaFilme.Count == 0)
            {
                Console.WriteLine("Nenhum título cadastrado.");
                return;
            }
            
            int tipoItem = definirTipo();

            if (tipoItem == 2)
            {
                if (lista.Count == 0)
                {
                    Console.WriteLine("Nenhuma série cadastrada.");
                    return;
                }
                
                foreach (var serie in lista)
                {
                    var excluido = serie.retornaExcluido();

                    Console.WriteLine("#ID {0}: - {1}{2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? " - Excluido" : ""));
                }
            }
            else
            {
                if (listaFilme.Count == 0)
                {
                    Console.WriteLine("Nenhum filme cadastrado.");
                    return;
                }
                
                foreach (var filme in listaFilme)
                {
                    var excluido = filme.retornaExcluido();

                    Console.WriteLine("#ID {0}: - {1}{2}", filme.retornaId(), filme.retornaTitulo(), (excluido ? " - Excluido" : ""));
                }
            }
        }

        private static void InserirTitulo()
        {

            Console.WriteLine("Inserir um item");
            Console.WriteLine();

            int tipoItem = definirTipo();
            string nomeSerie, descricaoSerie;
            int generoSerie, anoSerie;

            ColetaDadosEntrada(out nomeSerie, out generoSerie, out anoSerie, out descricaoSerie);

            if (tipoItem == 2)
            {

                Series serieInserida = new Series(id: repositorio.ProximoId(),
                                                    genero: (Genero)generoSerie,
                                                    tipo: (Tipo)tipoItem,
                                                    titulo: nomeSerie,
                                                    descricao: descricaoSerie,
                                                    ano: anoSerie);

                repositorio.Insere(serieInserida);
            }
            else
            {
                Filmes filmeInserido = new Filmes(id: repositorio.ProximoId(),
                                                    genero: (Genero)generoSerie,
                                                    tipo: (Tipo)tipoItem,
                                                    titulo: nomeSerie,
                                                    descricao: descricaoSerie,
                                                    ano: anoSerie);
                repositorioFilme.Insere(filmeInserido);
            }
        }

        private static void ColetaDadosEntrada(out string nomeSerie, out int generoSerie, out int anoSerie, out string descricaoSerie)
        {
            Console.WriteLine("Qual o nome do título?");
            nomeSerie = Console.ReadLine();

            Console.WriteLine("Qual o gênero? ");
            foreach (int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }
            generoSerie = int.Parse(Console.ReadLine());
            Console.WriteLine();

            while (!Enum.IsDefined(typeof(Genero), generoSerie))
            {
                Console.WriteLine("Não conseguimos identificar o gênero.");
                Console.Write("Digite novamente: ");
                generoSerie = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Qual o ano do título? ");
            anoSerie = int.Parse(Console.ReadLine());
            Console.WriteLine();

            Console.WriteLine("Escreva a descrição do título: ");
            descricaoSerie = Console.ReadLine();
            Console.WriteLine();
        }

        private static int definirTipo()
        {
            Console.WriteLine("Qual o tipo do item?");
                foreach (int j in Enum.GetValues(typeof(Tipo)))
                {
                    Console.WriteLine("{0} - {1}", j, Enum.GetName(typeof(Tipo), j));
                }
                int tipoItem = int.Parse(Console.ReadLine());

                while (!Enum.IsDefined(typeof(Tipo), tipoItem))
                {
                    Console.WriteLine("Não conseguimos identificar o tipo.");
                    Console.Write("Digite novamente: ");
                    tipoItem = int.Parse(Console.ReadLine());
                }
            return tipoItem;
        }
        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("DIO Series a seu dispor!!!");
            Console.WriteLine("Informe a opção desejada");

            Console.WriteLine("1 - Listar");
            Console.WriteLine("2 - Inserir novo");
            Console.WriteLine("3 - Atualizar");
            Console.WriteLine("4 - Excluir");
            Console.WriteLine("5 - Visualizar");
            Console.WriteLine("C - Limpar Tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;
        }
        
    }
}
