namespace ConsoleApp.DuplicateProcessing;

public interface IDuplicateProcessor
{
    Task ProcessDuplicatesAsync(IEnumerable<Ride> duplicateRides);
}
