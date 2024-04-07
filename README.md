# Guide to Handling Concurrency Conflicts
This is a reference repository for the article on [Guide to Handling Concurrency Conflicts In ASP.NET Core](https://medium.com/@chris.claude/guide-to-handling-concurrency-conflicts-in-asp-net-core-db26c75a8267).

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

You should be able to access the Swagger UI at https://localhost:7073/swagger/index.html

## License
[MIT](./LICENSE)