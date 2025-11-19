using CsvHelper.Configuration;

namespace ConsoleApp;

public sealed class RideMap : ClassMap<Ride>
{
    public RideMap()
    {
        Map(m => m.DropoffDatetime).Name(DataColumnNames.DropoffDateTime);
        Map(m => m.PickupDatetime).Name(DataColumnNames.PickupDatetime);
        Map(m => m.PassengersCount).Name(DataColumnNames.PassengersCount);
        Map(m => m.TripDistance).Name(DataColumnNames.TripDistance);
        Map(m => m.StoreAndFwdFlag).Name(DataColumnNames.StoreAndFwdFlag);
        Map(m => m.PickupLocationId).Name(DataColumnNames.PickupLocationId);
        Map(m => m.DropoffLocationId).Name(DataColumnNames.DropoffLocationId);
        Map(m => m.FareAmount).Name(DataColumnNames.FareAmount);
        Map(m => m.TipAmount).Name(DataColumnNames.TipAmount);
        Map(m => m.SecondsTravelled).Ignore();
    }
}
