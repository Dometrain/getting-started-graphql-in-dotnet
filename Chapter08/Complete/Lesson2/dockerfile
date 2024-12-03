# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./ ./
RUN dotnet restore /app/src/Catalog.API
RUN dotnet publish /app/src/Catalog.API -c Release

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Disable the invariant mode (set in base image)
ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

WORKDIR /app
COPY --from=build-env /app/src/Catalog.API/bin/Release/net8.0/publish/ .
ENV ASPNETCORE_URLS=http://+:80
ENTRYPOINT ["dotnet", "eShop.Catalog.API.dll"]