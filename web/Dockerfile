FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app

RUN apt-get update && apt-get install -y wget 
HEALTHCHECK --interval=5m --timeout=3s CMD wget --no-verbose --tries=1 --spider  http://localhost:80/hc || exit 1
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Web.dll"]
