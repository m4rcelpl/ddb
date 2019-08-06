FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-alpine3.9 AS base
WORKDIR /app
RUN apk add --update 'mariadb-client' && \
    rm -rf /var/cache/apk/*

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine3.9 AS build
WORKDIR /src
COPY ["ddb.csproj", ""]
RUN dotnet restore "./ddb.csproj"
COPY . .
WORKDIR /src
RUN dotnet build "ddb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ddb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ddb.dll"]