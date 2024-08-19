FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build-env
WORKDIR /app

COPY ./src ./src
RUN dotnet publish -c Release -r linux-x64 --self-contained -o /app/out ./src/Redirector.App/Redirector.App.csproj

FROM ubuntu/dotnet-deps:7.0_edge AS final

LABEL org.opencontainers.image.source="https://github.com/gldraphael/redirector"
LABEL org.opencontainers.image.description="A simple hello world application."

ENV \
    # Configure web servers to bind to port 80 when present
    ASPNETCORE_URLS=http://+:80      \
    # Enable detection of running in a container
    DOTNET_RUNNING_IN_CONTAINER=true \
    # Disable globalization
    DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=1

EXPOSE 80

WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["./Redirector.App"]
