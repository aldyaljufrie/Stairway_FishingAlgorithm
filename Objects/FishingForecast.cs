using Stairway_FishingAlgorithm.Enums;
using Stairway_FishingAlgorithm.Helper;

namespace Stairway_FishingAlgorithm.Objects;

public class FishingForecast
{
    public (Dictionary<SizeTypes, int> fishCounts, Dictionary<ColorTypes, int> colorDistribution) Data;
    public List<Fish> Fishes { get; } = new();

    public FishingForecast()
    {
        Fishes.Clear();
        
        int small = RNG.Random.Next(8, 17);
        int medium = RNG.Random.Next(6, 13);
        int big = RNG.Random.Next(4, 9);
        int totalFish = small + medium + big;
        
        Console.WriteLine($"Total Fish : {totalFish}");
        
        Dictionary<SizeTypes, int> fishCounts = new()
        {
            { SizeTypes.Small, small },
            { SizeTypes.Medium, medium },
            { SizeTypes.Big, big }
        };

        int red = RNG.Random.Next(10, 41);
        int blue = RNG.Random.Next(10, 61 - red);
        int green = 100 - red - blue;
        
        Dictionary<ColorTypes, int> colorDistribution = new()
        {
            { ColorTypes.Red, red },
            { ColorTypes.Blue, blue },
            { ColorTypes.Green, green }
        };

        Data = (fishCounts, colorDistribution);
        PopulateFishes(fishCounts, colorDistribution);
    }
    
    private void PopulateFishes(Dictionary<SizeTypes, int> fishCounts, Dictionary<ColorTypes, int> colorDistribution)
    {
        foreach (var sizePair in fishCounts)
        {
            SizeTypes size = sizePair.Key;
            int amount = sizePair.Value;

            // Determine the number of fish of each color for this size
            int redFishCount = amount * colorDistribution[ColorTypes.Red] / 100;
            int blueFishCount = amount * colorDistribution[ColorTypes.Blue] / 100;
            int greenFishCount = amount - redFishCount - blueFishCount;

            // Add red fish
            for (int i = 0; i < redFishCount; i++)
            {
                Fishes.Add(Fish.GetFish(ColorTypes.Red, size));
            }

            // Add blue fish
            for (int i = 0; i < blueFishCount; i++)
            {
                Fishes.Add(Fish.GetFish(ColorTypes.Blue, size));
            }

            // Add green fish
            for (int i = 0; i < greenFishCount; i++)
            {
                Fishes.Add(Fish.GetFish(ColorTypes.Green, size));
            }
        }
    }

    public Fish? CatchFish(ColorTypes color, SizeTypes size)
    {
        var caughtFish = Fishes.Find(f => f.Color == color && f.Size == size);

        if (caughtFish != null)
            Fishes.Remove(caughtFish);
        
        return caughtFish;
    }

    public bool FishAvailable(SizeTypes size)
    {
        return Data.fishCounts[size] > 0;
    }

    public void Display()
    {
        var (fishCounts, colorDistribution) = Data;
        Console.WriteLine("Today's forecast:");
        Console.WriteLine($"{fishCounts[SizeTypes.Small]} small fish, {fishCounts[SizeTypes.Medium]} medium fish, {fishCounts[SizeTypes.Big]} big fish.");
        Console.WriteLine($"{colorDistribution[ColorTypes.Red]}% are red, {colorDistribution[ColorTypes.Blue]}% are blue, {colorDistribution[ColorTypes.Green]}% are green.");
        
        // var groupedFishes = Fishes
        //     .GroupBy(fish => new { fish.Color, fish.Size })
        //     .Select(group => new
        //     {
        //         group.Key.Color,
        //         group.Key.Size,
        //         Count = group.Count()
        //     });
        //
        // Console.WriteLine("Available fishes in the pond:");
        // foreach (var fish in groupedFishes)
        // {
        //     Console.WriteLine($"{fish.Color} {fish.Size} fish: {fish.Count} available");
        // }
    }
}