# football-manager
 REST API for managing football teams
 Football Manager is a .NET Web api targets .NET 6. Football Manager uses InMemory Database provider, but you could use any provider that entity framework has.
 

## Run
You can run Api with this command:

`dotnet run -p .\src\FootballManager.Api\FootballManager.Api.csproj `

## Features
- [x] Run: http://localhost:5000
- [x] Swagger: http://localhost:5000/swagger/index.html
- [x] Health Check: http://localhost:5000/health

## Docker
You can build docker file with this command:

`docker build --tag football-manager .`

## TODO
- [ ] Http clients
- [ ] Unit tests