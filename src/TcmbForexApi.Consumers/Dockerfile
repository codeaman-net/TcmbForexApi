FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/TcmbForexApi.Consumers/TcmbForexApi.Consumers.csproj", "src/TcmbForexApi.Consumers/"]
RUN dotnet restore "src/TcmbForexApi.Consumers/TcmbForexApi.Consumers.csproj"
COPY . .
WORKDIR "/src/src/TcmbForexApi.Consumers"
RUN dotnet build "TcmbForexApi.Consumers.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TcmbForexApi.Consumers.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TcmbForexApi.Consumers.dll"]
