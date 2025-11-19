using System.Globalization;
using CsvHelper;

namespace ConsoleApp.DuplicateProcessing;

public class DuplicateProcessor : IDuplicateProcessor
{
    private const string _duplicatesCsvFileName = "duplicates.csv";

    public async Task ProcessDuplicatesAsync(IEnumerable<Ride> duplicateRides)
    {
        using var stream = new StreamWriter(_duplicatesCsvFileName);
        using var csvWriter = new CsvWriter(stream, CultureInfo.InvariantCulture);

        csvWriter.Context.RegisterClassMap<RideMap>();

        await csvWriter.WriteRecordsAsync(duplicateRides);
    }
}
