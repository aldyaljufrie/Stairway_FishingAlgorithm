using Stairway_FishingAlgorithm.Enums;

namespace Stairway_FishingAlgorithm.Objects;

public class FishingPole(SizeTypes size, int cost)
{
    public static List<FishingPole> AvailablePoles { get; } =
    [
        new FishingPole(SizeTypes.Small, 5),
        new FishingPole(SizeTypes.Medium, 10),
        new FishingPole(SizeTypes.Big, 15)
    ];
    
    public SizeTypes Size { get; } = size;
    public int Cost { get; } = cost;

    public string GetName() => $"{Size} Fishing Pole";
}