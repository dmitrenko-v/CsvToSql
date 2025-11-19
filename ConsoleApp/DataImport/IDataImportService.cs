namespace ConsoleApp.DataImport;

public interface IDataImportService
{
    IAsyncEnumerable<Ride> ImportDataAsync();
}
