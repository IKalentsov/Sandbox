# syntax=docker/dockerfile:1

ARG BUILD_CONFIGURATION=Release

FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG BUILD_CONFIGURATION
WORKDIR /src

# Copy project files first to leverage layer caching
COPY ["Sandbox/Sandbox.csproj", "Sandbox/"]
COPY ["Sandbox.App/Sandbox.App.csproj", "Sandbox.App/"]
COPY ["Sandbox.DataAccess/Sandbox.DataAccess.csproj", "Sandbox.DataAccess/"]
COPY ["Sandbox.Domain/Sandbox.Domain.csproj", "Sandbox.Domain/"]

RUN dotnet restore "Sandbox/Sandbox.csproj"

# Copy the rest of the source
COPY . .

WORKDIR /src/Sandbox
RUN dotnet publish "Sandbox.csproj" -c "$BUILD_CONFIGURATION" -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Sandbox.dll"]
