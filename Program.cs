using System;

class Program
{
    static void Main()
    {
        string[] choices = { "rock", "paper", "scissors" };
        Random rng = new Random();
        int wins = 0, losses = 0, ties = 0;

        Console.WriteLine("Rock Paper Scissors (console)");
        Console.WriteLine("Type rock, paper, or scissors. Type quit to exit.\n");

        while (true)
        {
            Console.Write("Your move: ");
            string? input = Console.ReadLine();
            if (input == null)
                break;

            string player = input.Trim().ToLowerInvariant();
            if (player == "quit" || player == "q" || player == "exit")
                break;

            if (Array.IndexOf(choices, player) < 0)
            {
                Console.WriteLine("Please type rock, paper, or scissors.\n");
                continue;
            }

            string computer = choices[rng.Next(choices.Length)];
            Console.WriteLine("Computer chose: " + computer);

            if (player == computer)
            {
                ties++;
                Console.WriteLine("Tie!");
            }
            else if (
                (player == "rock" && computer == "scissors") ||
                (player == "paper" && computer == "rock") ||
                (player == "scissors" && computer == "paper"))
            {
                wins++;
                Console.WriteLine("You win!");
            }
            else
            {
                losses++;
                Console.WriteLine("You lose.");
            }

            Console.WriteLine($"Score — Wins: {wins}  Losses: {losses}  Ties: {ties}\n");
        }

        Console.WriteLine("Thanks for playing.");
    }
}
