using System;
using System.Collections.Generic;
using System.Text;

public class Pessoa
{
    public string Nome { get; set; }
    public int Idade { get; set; }

    public Pessoa(string nome, int idade)
    {
        Nome = nome;
        Idade = idade;
    }
}

public class Suite
{
    public string Tipo { get; set; }
    public decimal ValorDiaria { get; set; }

    public Suite(string tipo, decimal valorDiaria)
    {
        Tipo = tipo;
        ValorDiaria = valorDiaria;
    }
}

public class Reserva
{
    public List<Pessoa> Hospedes { get; set; } = new List<Pessoa>();
    public Suite Suite { get; set; }
    public int DiasReservados { get; set; }

    public Reserva(Suite suite)
    {
        Suite = suite;
    }

    public void AdicionarHospede(Pessoa hospede)
    {
        Hospedes.Add(hospede);
    }

    public decimal CalcularValorTotal()
    {
        decimal valorTotal = DiasReservados * Suite.ValorDiaria;

        if (DiasReservados > 10)
        {
            valorTotal *= 0.9m;
        }

        return valorTotal;
    }
}

class Program
{
    static List<Reserva> reservas = new List<Reserva>();

    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        bool continuar = true;
        while (continuar)
        {
            Console.WriteLine("\nBem-vindo ao Sistema de Reservas de Hotel");
            Console.WriteLine("1. Cadastrar Reserva");
            Console.WriteLine("2. Ver Reservas");
            Console.WriteLine("3. Calcular Valor da Estadia");
            Console.WriteLine("4. Sair");
            Console.Write("Escolha uma opção: ");
            var opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarReserva();
                    break;
                case "2":
                    VerReservas();
                    break;
                case "3":
                    CalcularValorEstadia();
                    break;
                case "4":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarReserva()
    {
        Console.Write("Informe o tipo de suíte: ");
        string tipo = Console.ReadLine();

        Console.Write("Informe o valor da diária: ");
        decimal valorDiaria = decimal.Parse(Console.ReadLine());

        var suite = new Suite(tipo, valorDiaria);
        var reserva = new Reserva(suite);

        Console.Write("Informe quantos dias serão reservados: ");
        reserva.DiasReservados = int.Parse(Console.ReadLine());

        Console.Write("Quantos hóspedes? ");
        int quantidadeHospedes = int.Parse(Console.ReadLine());
        for (int i = 0; i < quantidadeHospedes; i++)
        {
            Console.Write($"Nome do hóspede {i + 1}: ");
            string nome = Console.ReadLine();

            Console.Write($"Idade do hóspede {i + 1}: ");
            int idade = int.Parse(Console.ReadLine());

            reserva.AdicionarHospede(new Pessoa(nome, idade));
        }

        reservas.Add(reserva);
        Console.WriteLine("Reserva cadastrada com sucesso!");
    }

    static void VerReservas()
    {
        if (reservas.Count == 0)
        {
            Console.WriteLine("Não há reservas cadastradas.");
            return;
        }

        foreach (var reserva in reservas)
        {
            Console.WriteLine($"Suíte: {reserva.Suite.Tipo}, Valor da Diária: {reserva.Suite.ValorDiaria}, Dias Reservados: {reserva.DiasReservados}");
            foreach (var hospede in reserva.Hospedes)
            {
                Console.WriteLine($"Hóspede: {hospede.Nome}, Idade: {hospede.Idade}");
            }
            Console.WriteLine("-----------------------------------");
        }
    }

    static void CalcularValorEstadia()
    {
        if (reservas.Count == 0)
        {
            Console.WriteLine("Não existem reservas cadastradas.");
            return;
        }

        for (int i = 0; i < reservas.Count; i++)
        {
            var reserva = reservas[i];
            string nomePrimeiroHospede = reserva.Hospedes.Count > 0 ? reserva.Hospedes[0].Nome : "N/A";
            Console.WriteLine($"{i + 1} - {nomePrimeiroHospede} - {reserva.Suite.Tipo} - R${reserva.Suite.ValorDiaria:0.00}");
        }

        Console.Write("Escolha o índice da reserva para calcular o valor total da estadia: ");
        int indice = int.Parse(Console.ReadLine()) - 1;

        if (indice < 0 || indice >= reservas.Count)
        {
            Console.WriteLine("Índice de reserva inválido.");
            return;
        }

        var reservaEscolhida = reservas[indice];
        decimal valorTotal = reservaEscolhida.CalcularValorTotal();
        Console.WriteLine($"O valor total da estadia é: R$ {valorTotal:0.00}");
    }
}
