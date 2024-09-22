using Stairway_FishingAlgorithm.Enums;
using Stairway_FishingAlgorithm.Objects;

namespace Stairway_FishingAlgorithm;

class Program
{
    static void Main(string[] args)
    {
        Game game = new Game();
        game.Start();
    }
}

class Game
{
    private Player player;
    private FishingForecast forecast;
    private FishMarket market;

    public Game()
    {
        player = new Player(100);
        market = new FishMarket();
    }
    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Welcome to the Fishing Game!");
            int initialGold = player.Gold;

            forecast = new FishingForecast();
            forecast.Display();

            market.RentFishingPole(ref player);
            market.BuyBaits(ref player);

            List<Fish> catches = player.GoFishing(forecast);

            int earnings = market.SellFish(catches);
            player.Gold += earnings;

            DisplayResults(initialGold, player.Gold, catches, earnings);

            Console.WriteLine("Do you want to start next day? (yes/no)");
            if (Console.ReadLine().ToLower() != "yes")
                break;

            player.Reset(100);
        }
    }

    private void DisplayResults(int initialGold, int finalGold, List<Fish> catches, int earnings)
    {
        Console.WriteLine("\nFishing Results:");
        
        var catchSummary = catches
            .GroupBy(fish => new { fish.Color, fish.Size })
            .Select(group => new
            {
                FishType = group.Key,
                Count = group.Count()
            })
            .ToList();
        var catchesSummary = string.Join(", ", catchSummary.Select(c => $"{c.Count} {c.FishType.Color} {c.FishType.Size} Fish"));

        Console.WriteLine($"Total Catch: {catches.Count}");
        Console.WriteLine($"Catches: {catchesSummary}");
        Console.WriteLine($"Total Earnings: {earnings} gold");
        Console.WriteLine($"Starting Gold: {initialGold}");
        Console.WriteLine($"Final Gold: {finalGold}");

        if (finalGold > initialGold)
            Console.WriteLine("You win!");
        else if (finalGold < initialGold)
            Console.WriteLine("You lose.");
        else
            Console.WriteLine("It's a tie.");
    }
}