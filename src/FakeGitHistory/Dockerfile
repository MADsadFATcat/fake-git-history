FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FakeGitHistory.csproj", "."]
RUN dotnet restore "FakeGitHistory.csproj"

COPY . .
RUN dotnet build "FakeGitHistory.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "FakeGitHistory.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:9.0 AS final
USER 0
RUN apt-get update && apt-get install -y git && rm -rf /var/lib/apt/lists/*

USER $APP_UID
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FakeGitHistory.dll"]
