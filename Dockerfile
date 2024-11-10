# Use the official .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy and restore dependencies for each project
COPY ArtistApplication.Domain/ArtistApplication.Domain.csproj ArtistApplication.Domain/
COPY ArtistApplication.Repository/ArtistApplication.Repository.csproj ArtistApplication.Repository/
COPY ArtistApplication.Service/ArtistApplication.Service.csproj ArtistApplication.Service/
COPY ArtistApplication/ArtistApplication.Web.csproj ArtistApplication/

# Restore dependencies
RUN dotnet restore ArtistApplication/ArtistApplication.Web.csproj

# Copy the entire project files
COPY . .

# Build and publish the Web project to a folder named /app/publish
RUN dotnet publish ArtistApplication/ArtistApplication.Web.csproj -c Release -o /app/publish

# Use the official .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 80 for the application
EXPOSE 80

# Start the application with the correct DLL
ENTRYPOINT ["dotnet", "ArtistApplication.Web.dll"]
