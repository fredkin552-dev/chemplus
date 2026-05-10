# Use the official .NET 10.0 runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 10.0 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copy only the ASP.NET app folder to avoid compiling duplicate top-level sources
COPY ./WebApplication1/ ./WebApplication1/

# Build the app from the correct csproj location
WORKDIR /src/WebApplication1
RUN dotnet restore "./WebApplication1.csproj"
RUN dotnet build "./WebApplication1.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./WebApplication1.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Final stage: copy the published app to the base image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApplication1.dll"]
