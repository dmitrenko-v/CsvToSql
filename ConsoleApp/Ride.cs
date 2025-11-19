using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp;

[Index(nameof(PickupLocationId))]
[Index(nameof(SecondsTravelled))]
[Index(nameof(TripDistance))]
public class Ride
{
    [Column(DataColumnNames.PickupDatetime)]
    public DateTime? PickupDatetime { get; set; }

    [Column(DataColumnNames.DropoffDateTime)]
    public DateTime? DropoffDatetime { get; set; }

    // This field and index on this field is created to make reads of top rides by time faster.
    // It introduces memory/speed tradeoff but speed is more important because app contains only reads basically(and one big write on initialization)
    [Column(DataColumnNames.SecondsTravelled)]
    public int? SecondsTravelled { get; set; }

    [Column(DataColumnNames.PassengersCount)]
    public int? PassengersCount { get; set; }

    [Column(DataColumnNames.TripDistance)]
    public double? TripDistance { get; set; }

    [Column(DataColumnNames.StoreAndFwdFlag)]
    public string? StoreAndFwdFlag { get; set; } = null!;

    [Column(DataColumnNames.PickupLocationId)]
    public int? PickupLocationId { get; set; }

    [Column(DataColumnNames.DropoffLocationId)]
    public int? DropoffLocationId { get; set; }

    [Column(DataColumnNames.FareAmount)]
    public double? FareAmount { get; set; }

    [Column(DataColumnNames.TipAmount)]
    public double? TipAmount { get; set; }

    public override bool Equals(object? obj)
    {
        var ride = obj as Ride;

        if (ride == null)
        {
            return false;
        }

        return ride.PickupDatetime == PickupDatetime
            && ride.DropoffDatetime == DropoffDatetime
            && ride.PassengersCount == PassengersCount;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PickupDatetime, DropoffDatetime, PassengersCount);
    }
}
