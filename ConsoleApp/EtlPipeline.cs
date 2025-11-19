using ConsoleApp.Database;
using ConsoleApp.DataImport;
using ConsoleApp.DuplicateProcessing;
using ConsoleApp.Transformation;

namespace ConsoleApp;

public class EtlPipeline(
    IDataImportService dataImportService,
    IDuplicateProcessor duplicateProcessor,
    IRideTransformer rideTransformer,
    IRideRepository rideRepository
)
{
    public async Task RunPipelineAsync()
    {
        var uniqueRides = new HashSet<Ride>();
        var duplicateRides = new List<Ride>();

        var rides = dataImportService.ImportDataAsync();
        await foreach (var ride in rides)
        {
            var transformedRide = rideTransformer.TransformRide(ride);

            if (!uniqueRides.Add(transformedRide))
            {
                duplicateRides.Add(transformedRide);
            }
        }

        await rideRepository.InsertRidesAsync(uniqueRides);

        await duplicateProcessor.ProcessDuplicatesAsync(duplicateRides);
    }
}
