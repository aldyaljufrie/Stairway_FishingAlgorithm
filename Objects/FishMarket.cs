namespace Stairway_FishingAlgorithm.Objects;

public class FishMarket
{
    public int SellFish(List<Fish> catches)
    {
        return catches.Sum(fish => fish.GetPrice());
    }
    
    public void RentFishingPole(ref Player player)
    {
        Console.WriteLine($"\nYou have {player.Gold} gold.");
        Console.WriteLine("Choose a fishing pole:");
        for (int i = 0; i < FishingPole.AvailablePoles.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {FishingPole.AvailablePoles[i].GetName()}");
        }

        while (true)
        {
            if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= FishingPole.AvailablePoles.Count)
            {
                FishingPole selectedPole = FishingPole.AvailablePoles[choice - 1];
                if (player.Gold >= selectedPole.Cost)
                {
                    player.Gold -= selectedPole.Cost;
                    player.CurrentPole = selectedPole;
                    return;
                }
            }
            Console.WriteLine("Invalid choice or not enough gold. Try again.");
        }
    }
    
    public void BuyBaits(ref Player player)
    {
        Dictionary<Bait, int> chosenBaits = new Dictionary<Bait, int>();

        Console.WriteLine("\nBuying baits:");
        bool buying = true;

        while (buying && player.Gold > 0)
        {
            Console.WriteLine("\nWhich bait would you like to buy?");
            for (int i = 0; i < Bait.AvailableBaits.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Bait.AvailableBaits[i].GetName()} - {Bait.AvailableBaits[i].Cost} gold each");
            }
            Console.WriteLine("0. Done buying");

            int baitChoice;
            while (!int.TryParse(Console.ReadLine(), out baitChoice) || baitChoice < 0 || baitChoice > Bait.AvailableBaits.Count)
            {
                Console.WriteLine("Please enter a valid option.");
            }

            if (baitChoice == 0)
            {
                buying = false;
                continue;
            }

            Bait selectedBait = Bait.AvailableBaits[baitChoice - 1];

            // Determine how many baits can be bought with the remaining gold
            int maxBaitsAffordable = player.Gold / selectedBait.Cost;

            if (maxBaitsAffordable > 0)
            {
                Console.WriteLine($"How many {selectedBait.GetName()} would you like to buy? (Max {maxBaitsAffordable})");

                int amountToBuy;
                while (!int.TryParse(Console.ReadLine(), out amountToBuy) || amountToBuy < 0 || amountToBuy > maxBaitsAffordable)
                {
                    Console.WriteLine($"Please enter a valid number between 0 and {maxBaitsAffordable}:");
                }

                // Calculate total cost for the selected bait and reduce the player's gold
                int totalCost = selectedBait.Cost * amountToBuy;
                player.Gold -= totalCost;

                if (amountToBuy > 0)
                {
                    // Add the bait to the dictionary or update the existing count
                    if (chosenBaits.ContainsKey(selectedBait))
                    {
                        chosenBaits[selectedBait] += amountToBuy;
                    }
                    else
                    {
                        chosenBaits[selectedBait] = amountToBuy;
                    }
                }

                // Print the current status of baits and remaining gold
                Console.WriteLine($"You have bought {amountToBuy} {selectedBait.GetName()}.");
                Console.WriteLine($"You have {player.Gold} gold left.");
            }
            else
            {
                Console.WriteLine("You don't have enough gold to buy this bait.");
            }
        }

        // Summary of all baits bought
        Console.WriteLine("\nBait summary:");
        foreach (var baitEntry in chosenBaits)
        {
            Console.WriteLine($"- {baitEntry.Value} {baitEntry.Key.GetName()}");
        }
        Console.WriteLine($"Gold remaining: {player.Gold}");

        player.Baits = chosenBaits;
    }
}