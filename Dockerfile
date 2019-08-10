FROM alpine:3.10 AS base 
RUN apk add --no-cache --update 'mariadb-client>10.3.17'
RUN apk add --no-cache \
    ca-certificates \
    \
    # .NET Core dependencies
    krb5-libs \
    libgcc \
    libintl \
    libssl1.1 \
    libstdc++ \
    lttng-ust \
    tzdata \
    userspace-rcu \
    zlib
ENV ASPNETCORE_URLS=http://+:80 \
    DOTNET_RUNNING_IN_CONTAINER=true \
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=true
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.0-alpine3.9 AS build
WORKDIR /src
COPY . .
RUN dotnet publish "ddb.csproj" -r linux-musl-x64 -c Release -o /app --self-contained true

FROM base AS final
WORKDIR /app
COPY --from=build /app .
RUN mkdir /app/backup
VOLUME ["/app/backup"]
ENTRYPOINT ["./ddb"]