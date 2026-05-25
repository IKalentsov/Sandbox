# syntax=docker/dockerfile:1

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /source

# Copy project structure for layered builds
COPY DirectoryProperties.props ./
COPY Sandbox/Sandbox.csproj Sandbox/
COPY Sandbox.App/Sandbox.App.csproj Sandbox.App/
COPY Sandbox.DataAccess/Sandbox.DataAccess.csproj Sandbox.DataAccess/
COPY Sandbox.Domain/Sandbox.Domain.csproj Sandbox.Domain/

# Restore dependencies
RUN dotnet restore Sandbox/Sandbox.csproj

# Copy remaining source and build
COPY . .
WORKDIR /source/Sandbox
RUN dotnet publish "Sandbox.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish \
    -p:Version=$BUILD_VERSION || true

# Runtime stage - use Alpine for smaller image
FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS final
WORKDIR /app

# Install dependencies for database connectivity
RUN apk add --no-cache postgresql-client

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

COPY --from=build /app/publish .

HEALTHCHECK --interval=30s --timeout=3s --start-period=5s --retries=3 \
    CMD pg_isready -h localhost -p 5432 || exit 1

ENTRYPOINT ["dotnet", "Sandbox.dll"]
