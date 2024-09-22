using Stairway_FishingAlgorithm.Enums;

namespace Stairway_FishingAlgorithm.Objects;

public class Bait(ColorTypes color, int cost)
{
    public static List<Bait> AvailableBaits { get; } =
    [
        new Bait(ColorTypes.Red, 1),
        new Bait(ColorTypes.Blue, 2),
        new Bait(ColorTypes.Green, 3)
    ];
    
    public ColorTypes Color { get; } = color;
    public int Cost { get; } = cost;
    
    public string GetName() => $"{Color} Bait";
}