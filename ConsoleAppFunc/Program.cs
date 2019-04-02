using System;
using System.Threading.Tasks;

namespace ConsoleAppFunc
{
    class Program
    {
        static void Main(string[] args)
        {
            var robo1 = new Transportador();
            var robo2 = new Desempacotador();
            var robo3 = new Conferidor();

            var coordenador = new Coordenador();
            coordenador.Robo1 = robo1.Transportar;
            coordenador.Robo2 = robo2.Desempacotar;
            coordenador.Robo3 = robo3.Conferir;

            Task task = Task.Factory.StartNew(() => coordenador.Executar("Pacote 1", 1000));

            //Task task2 = Task.Factory.StartNew(() => coordenador.Executar("Pacote 2", 2000));


            Console.WriteLine("Task em Execução...");
            Console.ReadKey();


        }
    }


    class Coordenador
    {
        public Func<Coordenador, string, int, bool> Robo1 { get; set; }
        public Func<Coordenador, string, int, bool> Robo2 { get; set; }
        public Func<Coordenador, string, int, bool> Robo3 { get; set; }


        public void Executar(string mensagem, int intervalo)
        {
            Console.WriteLine($"==={mensagem}===");

            var r1 = Robo1.Invoke(this, mensagem, intervalo);
            if (r1)
            {
                var r2 = Robo2.Invoke(this, mensagem, intervalo);
                if (r2)
                {
                    var r3 = Robo3.Invoke(this, mensagem, intervalo);
                }
            }

        }
    }

    class Transportador
    {
        public bool Transportar(Coordenador c, string mensagem, int intervalo)
        {
            Console.WriteLine($"[{mensagem}] - Transportando...");
            System.Threading.Thread.Sleep(intervalo);
            return true;
        }
    }

    class Desempacotador
    {
        public bool Desempacotar(Coordenador c, string mensagem, int intervalo)
        {
            Console.WriteLine($"[{mensagem}] - Desempacotando...");
            System.Threading.Thread.Sleep(intervalo);
            return true;
        }
    }

    class Conferidor
    {
        private static int numerador = 1;

        public bool Conferir(Coordenador c, string mensagem, int intervalo)
        {
            Console.WriteLine($"[{mensagem}] - Conferindo...");
            System.Threading.Thread.Sleep(intervalo);
            numerador++;
            c.Executar("Pacote " + numerador, intervalo);
            return true;
        }
    }
}
