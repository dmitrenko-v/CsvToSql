## Running application
.NET 10 is required for running application.  

Before running application, make sure to add environment variable with connection string to your MS SQL database instance.  
On Windows:
```
set DB_CONNSTRING="<connection string>"
```

On Linux/MacOS
```
export DB_CONNSTRING="<connection string>"
```

Then, to run application, in the root folder type the following commands:
```
cd ConsoleApp/
dotnet run
```

## Implementation details and info

Following libraries were used in this task:
- [CsvHelper](https://www.nuget.org/packages/csvhelper/) for efficient working with csv files
- [Entity Framework Core](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore) as ORM
- [Entity Framework Core Bulk Extensions](https://www.nuget.org/packages/Z.EntityFramework.Extensions.EFCore/) for efficient and fast bulk insert
- [Entity Framework Core Sql Server](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.sqlserver/) for using MS SQL Database with EF Core
- [Entity Framework Core Design](https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design) for creating and using migrations

Database indexes were used to speed up search.

New field *seconds_travelled* and index for this field were added into db for making search of longest trips(by time) much faster.  
This field represents total time of trip in seconds.  
This approach, of course, uses more memory, but for app with read-only queries like this(with one big write at initialization) it makes perfect sense.

All the processing of data is done during the initial read of csv file and then bulk inserted into db.

Database contains **29889** rows after running program.

## Possible changes for scaling
CsvHelper is pretty efficient in the way that it doesnt load the whole file into memory at once, so memory-wise current implementation can scale.  
But, processing bigger files requires more time and for making it faster, different parts of csv file may be processed in separate threads as long as these parts can be processed independetly.

