# Use the official .NET 10.0 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 10.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the ASP.NET app folder (avoid copying repo-root duplicates like Controllers/, Models/, Program.cs)
COPY ./WebApplication1/ ./WebApplication1/

WORKDIR /src/WebApplication1

# Restore + build using the app csproj inside WebApplication1/
RUN dotnet restore "./WebApplication1.csproj"
RUN dotnet build "./WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: copy the published app to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
