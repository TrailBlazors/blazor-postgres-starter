# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY BlazorPostgresStarter/*.csproj BlazorPostgresStarter/
RUN dotnet restore "BlazorPostgresStarter/BlazorPostgresStarter.csproj"
COPY BlazorPostgresStarter/ BlazorPostgresStarter/
WORKDIR /src/BlazorPostgresStarter
RUN dotnet build "BlazorPostgresStarter.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "BlazorPostgresStarter.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
EXPOSE 8080
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlazorPostgresStarter.dll"]