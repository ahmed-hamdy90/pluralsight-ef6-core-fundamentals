FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY . .

WORKDIR "/src/PublisherConsole"
RUN dotnet restore "PublisherConsole.csproj"
RUN dotnet build "PublisherConsole.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PublisherConsole.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "PublisherConsole.dll"]