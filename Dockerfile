FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0 as runtime
WORKDIR /runtime-app
COPY --from=build-env /app/out .

EXPOSE 8080
ENTRYPOINT ["dotnet", "API.dll"]