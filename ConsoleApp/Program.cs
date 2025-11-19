using ConsoleApp;
using ConsoleApp.Database;
using ConsoleApp.DataImport;
using ConsoleApp.DuplicateProcessing;
using ConsoleApp.Transformation;

try
{
    Console.WriteLine("Initializing... Please wait\n");
    var dataImportService = new DataImportService();
    var duplicateProcessor = new DuplicateProcessor();
    var rideTransformer = new RideTransformer();

    using var appDbContext = new AppDbContext();
    await appDbContext.Database.EnsureDeletedAsync();
    await appDbContext.Database.EnsureCreatedAsync();

    var rideRepository = new RideRepository(appDbContext);

    var etlPipeline = new EtlPipeline(
        dataImportService,
        duplicateProcessor,
        rideTransformer,
        rideRepository
    );
    await etlPipeline.RunPipelineAsync();

    var inputProcessor = new ConsoleInputProcessor(rideRepository);

    await inputProcessor.Run();
}
catch (Exception e)
{
    Console.WriteLine(e);
}
