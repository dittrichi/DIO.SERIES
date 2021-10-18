using System;

namespace DIO.SERIES
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {            
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                Console.Clear();
                switch (opcaoUsuario)
                {
                    case "1":
                        ListarSeries();
                        break;
                    case "2":
                        InserirSerie();
                        break;
                    case "3":
                        AtualizarSerie();
                        break;
                    case "4":
                        ExcluirSerie();
                        break;
                    case "5":
                        VisualizarSerie();
                        break;

                    default:                        
                        ObterOpcaoUsuario();
                        break;
                }
                Console.Clear();
                opcaoUsuario = ObterOpcaoUsuario();
            }
        }

        private static void ListarSeries()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("Listar séries");

            var lista = repositorio.Lista();

            if(lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada");
                
                Console.WriteLine("Digite uma tecla para retornar ao Menu");
                Console.ReadKey();
                return;
            }
            
            foreach(var serie in lista)
            {
                var excluido = serie.retornaExcluido();
                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluído*":""));
            }
            Console.WriteLine("Digite uma tecla para retornar ao Menu");
            Console.ReadKey();
        } 

        private static void InserirSerie()
        {
            Console.WriteLine();
            Console.WriteLine("Inserir nova série");
            ApresentaGenero();
            Console.Write("Digite o gênero entre as opções acima:");
            int entradaGenero = ValidaGenero(Console.ReadLine());
            Console.Write("Digite o título da série:");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Digite o ano de início da série:");            
            int entradaAno = ValidaAno(Console.ReadLine());
            Console.Write("Digite a descrição da serie:");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            repositorio.Insere(novaSerie);

            Console.WriteLine("Digite uma tecla para retornar ao Menu");
            Console.ReadKey();
        }

        private static void AtualizarSerie()
        {
            Console.WriteLine();
            Console.WriteLine("Digite o Id série");
            int indiceSerie = ValidaIdSerie(Console.ReadLine());
            ApresentaGenero();
            Console.Write("Digite o gênero entre as opções acima:");
            int entradaGenero = ValidaGenero(Console.ReadLine());
            Console.Write("Digite o título da série:");
            string entradaTitulo = Console.ReadLine();
            Console.Write("Digite o ano de início da série:");
            int entradaAno = ValidaAno(Console.ReadLine());
            Console.Write("Digite a descrição da serie:");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);            
            repositorio.Atualiza(indiceSerie, atualizaSerie, out bool statusRetorno);

            if(statusRetorno==true)
                Console.WriteLine("Série atualizada com sucesso");
            else
                Console.WriteLine("Série não existe no catálogo");

            Console.WriteLine("Digite uma tecla para retornar ao Menu");
            Console.ReadKey();
        }

        private static void ExcluirSerie()
        {
            Console.WriteLine();
            Console.WriteLine("Digite o Id da série: ");
            int indiceSerie = ValidaIdSerie(Console.ReadLine());
            repositorio.Exclui(indiceSerie, out bool status);
            if(status==true)
                Console.WriteLine("Série excluída com sucesso");
            else
                Console.WriteLine("Série não existe no catálogo");

            Console.WriteLine("Digite uma tecla para retornar ao Menu");
            Console.ReadKey();
        }

        private static void VisualizarSerie()
        {
            Console.WriteLine();
            Console.Write("Digite o Id da série: ");
            int indiceSerie = ValidaIdSerie(Console.ReadLine());
            var serie = repositorio.RetornaPorId(indiceSerie);

            if(serie!=null)
                Console.WriteLine(serie);
            else
                Console.WriteLine("Série não cadastrada");

            Console.WriteLine("Digite uma tecla para retornar ao Menu");
            Console.ReadKey();
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine();
            Console.WriteLine("Bem vindo ao catálogo de séries");
            Console.WriteLine("Informe a opção desejada:");
            Console.WriteLine();
            Console.WriteLine("1- Listar séries");
            Console.WriteLine("2- Inserir nova série");
            Console.WriteLine("3- Atualizar série");
            Console.WriteLine("4- Excluir série");
            Console.WriteLine("5 - Visalizar série");
            Console.WriteLine("X- Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            return opcaoUsuario;           
        }

        private static int ValidaGenero(String respostaGenero)
        {
             while(!Int32.TryParse(respostaGenero, out int X) || (Enum.GetValues(typeof(Genero)).Length < int.Parse(respostaGenero)) )
            {
                Console.Write("!!!Digite apenas números relacionados aos gêneros acima!!!");
                respostaGenero = Console.ReadLine();
            }            
            return int.Parse(respostaGenero);
        }

        private static int ValidaIdSerie(String respostaSerie)
        {
             while(!Int32.TryParse(respostaSerie, out int X))
            {
                Console.Write("!!!Digite apenas números no Id das séries");
                respostaSerie = Console.ReadLine();
            }            
            return int.Parse(respostaSerie);
        }

        private static int ValidaAno(String anoInicio)
        {
            while(!Int32.TryParse(anoInicio, out int X))
            {
                Console.Write("!!!Digite novamente o ano apenas utilizando números!!!");
                anoInicio = Console.ReadLine();
            }
            return int.Parse(anoInicio);
        }

        private static void ApresentaGenero()
        {
            //https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getvalues?view=net-5.0
            //https://docs.microsoft.com/pt-br/dotnet/api/system.enum.getnames?view=net-5.0
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero),i));
            }
            Console.WriteLine();
        }
    }
}
