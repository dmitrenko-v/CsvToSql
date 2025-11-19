using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Database;

public class RideRepository(AppDbContext context) : IRideRepository
{
    private readonly DbSet<Ride> rides = context.Set<Ride>();

    public async Task<int> GetPickupLocationIdWithHighestAvgTip()
    {
        return (int)
            await rides
                .GroupBy(r => r.PickupLocationId)
                .Select(g => new { LocationId = g.Key, AvgTip = g.Average(r => r.TipAmount) })
                .OrderByDescending(g => g.AvgTip)
                .Select(g => g.LocationId)
                .FirstAsync();
    }

    public async Task<IEnumerable<Ride>> GetLongestRidesByDistance()
    {
        return await rides.OrderByDescending(r => r.TripDistance).Take(100).ToListAsync();
    }

    public async Task<IEnumerable<Ride>> GetLongestRidesByTime()
    {
        return await rides.OrderByDescending(r => r.SecondsTravelled).Take(100).ToListAsync();
    }

    public async Task<IEnumerable<Ride>> GetRidesByPickupLocationId(int pickupLocationId)
    {
        return await rides.Where(r => r.PickupLocationId == pickupLocationId).ToListAsync();
    }

    public async Task InsertRidesAsync(IEnumerable<Ride> rides)
    {
        context.BulkInsert(rides);
        await context.BulkSaveChangesAsync();
    }
}
