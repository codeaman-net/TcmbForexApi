FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

ENV ASPNETCORE_URLS=http://+:80

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["src/TcmbForexApi.Gateway/TcmbForexApi.Gateway.csproj", "src/TcmbForexApi.Gateway/"]
RUN dotnet restore "src/TcmbForexApi.Gateway/TcmbForexApi.Gateway.csproj"
COPY . .
WORKDIR "/src/src/TcmbForexApi.Gateway"
RUN dotnet build "TcmbForexApi.Gateway.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "TcmbForexApi.Gateway.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TcmbForexApi.Gateway.dll"]
