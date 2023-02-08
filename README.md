# football-manager
 REST API for managing football teams
 Football Manager is a .NET Web api targets .NET 6. Football Manager uses InMemory Database provider, but you could use any provider that entity framework has.
 

## Run
You can run Api with this command:

`dotnet run -p .\src\FootballManager.Api\FootballManager.Api.csproj `

## Features
- [x] Api:&emsp;&emsp;&emsp;&emsp;&ensp; http://localhost:5000
- [x] Swagger:&emsp;&emsp; http://localhost:5000/swagger/index.html
- [x] Health Check: http://localhost:5000/health

## Docker
You can build docker file and run with this commands:

Build:`docker build --tag football-manager .`<br />
Run:&nbsp;`docker run -p 5000:5000 football-manager`
