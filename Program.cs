using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SecurityMaster
{
    class Program
    {
        static void Main(string[] args)
        {
            int escolha;
            do
            {
                Console.WriteLine("Bem vindo ao nosso site!!!\n1-Registrar-se:\n2-Entrar:\n3-Sair");
                escolha = int.Parse(Console.ReadLine());
                switch (escolha)
                {
                    case 1:
                        RegistrarUsuario();
                        break;
                    case 2:
                        ValidarUsuario();
                        break;
                    case 3:
                        Console.WriteLine("Obrigado por usar nosso site!!");
                        break;

                    default:
                        Console.WriteLine("Escolha uma opção válida!!!");
                        break;
                }
            }
            while (escolha != 3);
            
            
        }
        public static String ValidarTamanho(string palavra)
        {
            while(palavra.Length<4 || palavra.Length>8)
            {
                Console.WriteLine("Tamanho inválido, digite entre 4 a 8 caracteres apenas: ");
                palavra = Console.ReadLine();
            }
            return palavra;
        }
        public static void ValidarSenha(string senha, string validarSenha)
        {
            while (validarSenha != senha)
            {
                Console.WriteLine("Atenção, as senhas não estão iguais!!! Tente de novo: ");
                validarSenha = Console.ReadLine();
            }
        }
        public static void RegistrarUsuario()
        {
            Console.WriteLine("Nome de usuário (4 a 8 caracteres): ");
            string nome = Console.ReadLine();
            nome = ValidarTamanho(nome);
            Console.WriteLine("Senha de usuário (4 a 8 caracteres): ");
            string criarSenha = Console.ReadLine();
            criarSenha = ValidarTamanho(criarSenha);
            Console.WriteLine("Confirmar senha: ");
            string confirmarSenha = Console.ReadLine();
            ValidarSenha(criarSenha, confirmarSenha);
            
            Console.WriteLine("Cadastro feito com sucesso!");
            using (StreamWriter Dados = new StreamWriter("UsuariosCadastrados", true))
            {
                Dados.Write($"{nome},{criarSenha}");
                Dados.WriteLine();
            }
        }
        public static void ValidarUsuario()
        {
            string usuario, senha;
            bool autenticado = false;
            int cont = 0;

            while (cont < 3 && !autenticado)
            {
                Console.WriteLine("Informe o usuário: ");
                usuario = Console.ReadLine();
                Console.WriteLine("Informe a senha: ");
                senha = Console.ReadLine();
                using (StreamReader autenticador = new StreamReader("UsuariosCadastrados"))
                {
                    string line;
                    while ((line = autenticador.ReadLine()) != null)
                    {
                        string[] partes = line.Split(',');
                        string user = partes[0];
                        string password = partes[1];

                        if (usuario == user && senha == password)
                        {
                            Console.WriteLine("Login feito com sucesso!!!");
                            autenticado = true;
                            break;
                        }
                    }
                }

                if (!autenticado)
                {
                    cont++;
                    if (cont < 3)
                    {
                        Console.WriteLine("Usuário ou senha inválidos!!!");
                    }
                }
            }

            if (!autenticado)
            {
                Console.WriteLine("Limite de tentativas excedido, tente novamente mais tarde!!!");
            }
        }
    }
}
