using Stairway_FishingAlgorithm.Enums;
using Stairway_FishingAlgorithm.Helper;

namespace Stairway_FishingAlgorithm.Objects;

public class Player(int initialGold)
{
    public int Gold { get; set; } = initialGold;
    public FishingPole CurrentPole { get; set; }
    public Dictionary<Bait, int> Baits { get; set; }

    public void Reset(int gold)
    {
        Gold = gold;
        CurrentPole = null;
        Baits.Clear();
    }
    
    public List<Fish> GoFishing(FishingForecast forecast)
    {
        Console.WriteLine("\nFishing begins!");

        SizeTypes targetSize = CurrentPole.Size;
        var catches = new List<Fish>();

        foreach (var baitEntry in Baits)
        {
            Bait bait = baitEntry.Key;
            int baitCount = baitEntry.Value;
            
            Console.WriteLine($"Using {bait.GetName()}!");
            while (baitCount > 0 && forecast.FishAvailable(targetSize))
            {
                Thread.Sleep(1000);

                var caughtFish = forecast.CatchFish(bait.Color, targetSize);
                if (caughtFish != null)
                {
                    Console.WriteLine($"Caught a {caughtFish.GetName()}!");
                    catches.Add(caughtFish);
                    baitCount--;
                }
                else
                {
                    Console.WriteLine($"No {targetSize} {bait.Color} Fish left to catch.");
                    break;
                }
            }
        }

        return catches;
    }
}