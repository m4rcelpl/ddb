FROM mcr.microsoft.com/dotnet/core/runtime:3.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-buster AS build
WORKDIR /src
COPY ["ddb.csproj", ""]
RUN dotnet restore "./ddb.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "ddb.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ddb.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
RUN apt-get update && \
	apt-get -y dist-upgrade && \
	apt-get -y autoremove && \
	apt-get clean && \
	apt-get install -y \
		mariadb-client \
		bzip2

COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ddb.dll"]