FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/FootballManager.Api/FootballManager.Api.csproj", "src/FootballManager.Api/"]
RUN dotnet restore "src/FootballManager.Api/FootballManager.Api.csproj"
COPY . .
WORKDIR "/src/src/FootballManager.Api"
RUN dotnet build "FootballManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FootballManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FootballManager.Api.dll"]
