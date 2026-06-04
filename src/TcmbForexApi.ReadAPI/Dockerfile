FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/TcmbForexApi.ReadAPI/TcmbForexApi.ReadAPI.csproj", "src/TcmbForexApi.ReadAPI/"]
RUN dotnet restore "src/TcmbForexApi.ReadAPI/TcmbForexApi.ReadAPI.csproj"
COPY . .
WORKDIR "/src/src/TcmbForexApi.ReadAPI"
RUN dotnet build "TcmbForexApi.ReadAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TcmbForexApi.ReadAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TcmbForexApi.ReadAPI.dll"]
