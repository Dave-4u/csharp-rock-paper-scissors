using System;

/// <summary>Pure game rules, kept separate so they can be tested.</summary>
public static class Rules
{
    public static readonly string[] Choices = { "rock", "paper", "scissors" };

    /// <summary>Returns "win", "lose", or "tie" from the player's point of view.</summary>
    public static string Decide(string player, string computer)
    {
        if (player == computer) return "tie";
        bool win = (player == "rock" && computer == "scissors")
                || (player == "paper" && computer == "rock")
                || (player == "scissors" && computer == "paper");
        return win ? "win" : "lose";
    }

    /// <summary>Accepts full words or the first letter (r / p / s). Returns null if not a move.</summary>
    public static string? Normalize(string input)
    {
        string s = input.Trim().ToLowerInvariant();
        foreach (var c in Choices)
            if (s == c || (s.Length == 1 && c[0] == s[0])) return c;
        return null;
    }
}

class Program
{
    static int Main(string[] args)
    {
        if (args.Length > 0 && args[0] == "--selftest") return SelfTest();

        Random rng = new Random();
        int wins = 0, losses = 0, ties = 0, streak = 0, best = 0;

        Console.WriteLine("Rock Paper Scissors (console)");
        Console.WriteLine("Type rock, paper, or scissors (or just r / p / s). Type quit to exit.\n");

        while (true)
        {
            Console.Write("Your move: ");
            string? input = Console.ReadLine();
            if (input == null) break;

            string trimmed = input.Trim().ToLowerInvariant();
            if (trimmed == "quit" || trimmed == "q" || trimmed == "exit") break;

            string? player = Rules.Normalize(trimmed);
            if (player == null)
            {
                Console.WriteLine("Please type rock, paper, or scissors.\n");
                continue;
            }

            string computer = Rules.Choices[rng.Next(Rules.Choices.Length)];
            Console.WriteLine("Computer chose: " + computer);

            switch (Rules.Decide(player, computer))
            {
                case "tie":
                    ties++;
                    Console.WriteLine("Tie!");
                    break;
                case "win":
                    wins++;
                    streak++;
                    best = Math.Max(best, streak);
                    Console.WriteLine(streak >= 3 ? $"You win! That's {streak} in a row." : "You win!");
                    break;
                default:
                    losses++;
                    streak = 0;
                    Console.WriteLine("You lose.");
                    break;
            }

            Console.WriteLine($"Score — Wins: {wins}  Losses: {losses}  Ties: {ties}\n");
        }

        Console.WriteLine(best > 0 ? $"Thanks for playing. Best streak: {best}." : "Thanks for playing.");
        return 0;
    }

    static int SelfTest()
    {
        int failed = 0;
        void Check(bool ok, string name)
        {
            Console.WriteLine((ok ? "  ok - " : "  FAIL - ") + name);
            if (!ok) failed++;
        }
        Check(Rules.Decide("rock", "scissors") == "win", "rock beats scissors");
        Check(Rules.Decide("paper", "rock") == "win", "paper beats rock");
        Check(Rules.Decide("scissors", "paper") == "win", "scissors beats paper");
        Check(Rules.Decide("rock", "paper") == "lose", "rock loses to paper");
        Check(Rules.Decide("paper", "paper") == "tie", "same move ties");
        Check(Rules.Normalize(" R ") == "rock", "r means rock");
        Check(Rules.Normalize("Scissors") == "scissors", "case-insensitive");
        Check(Rules.Normalize("lizard") == null, "rejects unknown moves");
        Console.WriteLine(failed == 0 ? "All tests passed" : $"{failed} test(s) failed");
        return failed == 0 ? 0 : 1;
    }
}
