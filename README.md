# Guide to Handling Concurrency Conflicts
This is a reference repository for the article on Guide to Handling Concurrency Conflicts.

## Quick Start
You will need to have .NET Core installed. This application was tested on .NET 8. You will also need to have SQL Server installed and configure the connection string in appsettings.Development.json. Then, run the following commands from the root of the application

If you don't have dotnet ef tools installed
```
dotnet tool install --global dotnet-ef
```

Then

```
dotnet ef database update
dotnet run
```
## License
[MIT](./LICENSE)