# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

COPY . .

FROM mcr.microsoft.com/dotnet/aspnet:5.0
COPY --from=build /source ./
CMD ASPNETCORE_URLS=http://*:$PORT dotnet TodoApp.dll
