FROM mcr.microsoft.com/dotnet/core/runtime-deps:3.0-disco AS base 
RUN apt-get update \
    && apt-get -y upgrade \
    && apt-get install -y --no-install-recommends \
    mariadb-client \
    && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-disco AS build
#RUN git clone https://github.com/m4rcelpl/ddb.git /src
WORKDIR /src
COPY . .
#RUN dotnet publish "ddb.csproj" -r linux-x64 -c Release -o /app --self-contained true /p:PublishTrimmed=true
RUN dotnet publish "ddb.csproj" -r linux-x64 -c Release -o /app --self-contained true


FROM base AS final
WORKDIR /app
COPY --from=build /app .
RUN mkdir /app/backup
VOLUME ["/app/backup"]
ENTRYPOINT ["./ddb"]