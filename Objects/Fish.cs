using Stairway_FishingAlgorithm.Enums;
using Stairway_FishingAlgorithm.Helper;

namespace Stairway_FishingAlgorithm.Objects;

public class Fish(SizeTypes size, ColorTypes color, int minPrice, int maxPrice)
{
    public SizeTypes Size { get; } = size;
    public ColorTypes Color { get; } = color;
    private int MinPrice { get; } = minPrice;
    private int MaxPrice { get; } = maxPrice;
    
    public string GetName() => $"{Color} {Size} Fish";
    
    public int GetPrice() => RNG.Random.Next(MinPrice, MaxPrice + 1);
    
    public static Fish GetFish(ColorTypes color, SizeTypes size)
    {
        return color switch
        {
            ColorTypes.Red => size switch
            {
                SizeTypes.Small => new Fish(SizeTypes.Small, ColorTypes.Red, 1, 5),
                SizeTypes.Medium => new Fish(SizeTypes.Medium, ColorTypes.Red, 5, 10),
                SizeTypes.Big => new Fish(SizeTypes.Big, ColorTypes.Red, 10, 15),
                _ => throw new ArgumentException("Invalid size")
            },
            ColorTypes.Blue => size switch
            {
                SizeTypes.Small => new Fish(SizeTypes.Small, ColorTypes.Blue, 3, 5),
                SizeTypes.Medium => new Fish(SizeTypes.Medium, ColorTypes.Blue, 8, 10),
                SizeTypes.Big => new Fish(SizeTypes.Big, ColorTypes.Blue, 13, 15),
                _ => throw new ArgumentException("Invalid size")
            },
            ColorTypes.Green => size switch
            {
                SizeTypes.Small => new Fish(SizeTypes.Small, ColorTypes.Green, 5, 5),
                SizeTypes.Medium => new Fish(SizeTypes.Medium, ColorTypes.Green, 10, 10),
                SizeTypes.Big => new Fish(SizeTypes.Big, ColorTypes.Green, 15, 15),
                _ => throw new ArgumentException("Invalid size")
            },
            _ => throw new ArgumentException("Invalid color")
        };
    }
}