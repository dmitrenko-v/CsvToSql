using System.Runtime.InteropServices;

namespace ConsoleApp.Transformation;

public class RideTransformer : IRideTransformer
{
    public Ride TransformRide(Ride ride)
    {
        return new Ride
        {
            PickupDatetime = TransformEstDateTimeToUtc(ride.PickupDatetime),
            DropoffDatetime = TransformEstDateTimeToUtc(ride.DropoffDatetime),
            StoreAndFwdFlag = TransformStoreAndFwdFlag(ride.StoreAndFwdFlag),
            SecondsTravelled = CalculateTotalSeconds(ride.PickupDatetime, ride.DropoffDatetime),
            PickupLocationId = ride.PickupLocationId,
            DropoffLocationId = ride.DropoffLocationId,
            TripDistance = ride.TripDistance,
            FareAmount = ride.FareAmount,
            TipAmount = ride.TipAmount,
            PassengersCount = ride.PassengersCount,
        };
    }

    private static DateTime? TransformEstDateTimeToUtc(DateTime? dateTime)
    {
        if (dateTime == null)
        {
            return null;
        }

        var timeZoneId = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
            ? "Eastern Standard Time"
            : "America/New_York";

        var estTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

        return TimeZoneInfo.ConvertTimeToUtc((DateTime)dateTime, estTimeZone);
    }

    private static string? TransformStoreAndFwdFlag(string? flag)
    {
        if (flag == null)
        {
            return null;
        }

        var trimmedFlag = flag.Trim();

        return trimmedFlag switch
        {
            "N" => "No",
            "Y" => "Yes",
            _ => trimmedFlag,
        };
    }

    private static int? CalculateTotalSeconds(DateTime? pickupDate, DateTime? dropoffDate)
    {
        if (pickupDate == null || dropoffDate == null)
        {
            return null;
        }
        return (int)((DateTime)dropoffDate - (DateTime)pickupDate).TotalSeconds;
    }
}
