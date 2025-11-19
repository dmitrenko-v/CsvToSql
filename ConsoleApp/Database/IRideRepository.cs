namespace ConsoleApp.Database;

public interface IRideRepository
{
    Task InsertRidesAsync(IEnumerable<Ride> rides);

    Task<int> GetPickupLocationIdWithHighestAvgTip();

    Task<IEnumerable<Ride>> GetLongestRidesByDistance();

    Task<IEnumerable<Ride>> GetLongestRidesByTime();

    Task<IEnumerable<Ride>> GetRidesByPickupLocationId(int pickupLocationId);
}
