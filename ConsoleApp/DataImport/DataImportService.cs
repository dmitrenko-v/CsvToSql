using System.Globalization;
using CsvHelper;

namespace ConsoleApp.DataImport;

public class DataImportService : IDataImportService
{
    private const string _dataCsvFileName = "sample-cab-data.csv";

    public async IAsyncEnumerable<Ride> ImportDataAsync()
    {
        CheckDataFileExistence();

        using var reader = new StreamReader(_dataCsvFileName);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        csvReader.Context.RegisterClassMap<RideMap>();

        await csvReader.ReadAsync(); // advance into header row
        csvReader.ReadHeader();

        while (await csvReader.ReadAsync())
        {
            var ride = csvReader.GetRecord<Ride>();
            yield return ride;
        }
    }

    private static void CheckDataFileExistence()
    {
        var workingDirectory = Environment.CurrentDirectory;
        if (!File.Exists($"{workingDirectory}/{_dataCsvFileName}"))
        {
            throw new InvalidOperationException("Csv file with data does not exist");
        }
    }
}
