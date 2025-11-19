using ConsoleApp.Database;

namespace ConsoleApp;

public class ConsoleInputProcessor(IRideRepository rideRepository)
{
    public async Task Run()
    {
        while (true)
        {
            Console.WriteLine(
                "Available options. Press option number to proceed or press 'q' to quit:\n"
                    + "1. Get pick-up location ID with highest average tip amount\n"
                    + "2. Get top 100 longest fares by distance\n"
                    + "3. Get top 100 longest fares by time\n"
                    + "4. Search fares by pick-up location ID\n"
            );

            var inputKey = Console.ReadKey().Key;

            Console.WriteLine("\n");

            switch (inputKey)
            {
                case ConsoleKey.D1:
                    var locationId = await rideRepository.GetPickupLocationIdWithHighestAvgTip();
                    Console.WriteLine(
                        $"Pickup location id with highest average tip amount: {locationId}"
                    );
                    break;

                case ConsoleKey.D2:
                    OutputRides(await rideRepository.GetLongestRidesByDistance());
                    break;

                case ConsoleKey.D3:
                    OutputRides(await rideRepository.GetLongestRidesByTime());
                    break;

                case ConsoleKey.D4:
                    await ProcessSearchByLocationIdAsync();
                    break;

                case ConsoleKey.Q:
                    return;

                default:
                    Console.WriteLine("Invalid key. Please try again");
                    break;
            }
        }
    }

    private static void OutputRides(IEnumerable<Ride> rides)
    {
        if (!rides.Any())
        {
            Console.WriteLine("No results by given query\n");
            return;
        }

        foreach (var ride in rides)
        {
            Console.WriteLine($"Pickup Datetime: {ride.PickupDatetime}");
            Console.WriteLine($"Dropoff Datetime: {ride.DropoffDatetime}");
            Console.WriteLine($"Passengers count: {ride.PassengersCount}");
            Console.WriteLine($"Trip distance: {ride.TripDistance}");
            Console.WriteLine($"Store and fwd flag: {ride.StoreAndFwdFlag}");
            Console.WriteLine($"Pickup location id: {ride.PickupLocationId}");
            Console.WriteLine($"Dropoff location id: {ride.DropoffLocationId}");
            Console.WriteLine($"Fare amount: {ride.FareAmount}");
            Console.WriteLine($"Tip amount: {ride.TipAmount}");
            Console.WriteLine();
        }
    }

    private async Task ProcessSearchByLocationIdAsync()
    {
        while (true)
        {
            Console.WriteLine(
                "Please, type location id or 'back' for going back and press enter\n"
            );
            var input = Console.ReadLine();

            var trimmedInput = input?.Trim();

            if (trimmedInput == null)
            {
                continue;
            }

            if (trimmedInput.Equals("back", StringComparison.CurrentCultureIgnoreCase))
            {
                break;
            }

            if (!int.TryParse(input, out var pickupLocationId))
            {
                Console.WriteLine("Please, enter valid number\n");
            }
            else
            {
                var rides = await rideRepository.GetRidesByPickupLocationId(pickupLocationId);
                OutputRides(rides);
                break;
            }
        }
    }
}
