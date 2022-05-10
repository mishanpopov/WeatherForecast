FROM mcr.microsoft.com/dotnet/sdk:3.1.418-bullseye AS build-env
WORKDIR /app

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore
## Build and publish a release
RUN dotnet publish -c Release -o out
#
# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:3.1.24-bullseye-slim
WORKDIR /app
COPY --from=build-env /app/out .
ENV ASPNETCORE_URLS=http://+:80 
ENTRYPOINT ["dotnet", "WeatherForecast.GismeteoRipper.dll"]
