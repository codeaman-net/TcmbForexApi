# TcmbForexApi

A multi-service .NET application for consuming, writing, and reading foreign exchange rate data using Kafka, Debezium, PostgreSQL, MongoDB, and Ocelot API gateway routing.

## Project Overview

This repository contains four .NET services plus a Docker Compose environment:

- `TcmbForexApi.WriteAPI` – accepts incoming write requests and persists forex data to PostgreSQL.
- `TcmbForexApi.Consumers` – consumes Kafka messages, transforms them, and stores currency rates in MongoDB.
- `TcmbForexApi.ReadAPI` – exposes read endpoints to retrieve forex rate data from MongoDB.
- `TcmbForexApi.Gateway` – routes client requests to the read and write APIs using Ocelot.

The root solution file is `TcmbForexApi.slnx`.

## Architecture

- `WriteAPI` and `ReadAPI` are ASP.NET Core web applications built with .NET 10.
- `WriteAPI` initializes the PostgreSQL database on startup and exposes write operations.
- `Consumers` is a long-running worker process that subscribes to a Kafka topic and writes processed forex rates to MongoDB.
- `Gateway` is an Ocelot-based API gateway providing a single access point for read and write endpoints.
- Docker Compose includes PostgreSQL, Kafka, Zookeeper, Debezium Connect, MongoDB, Mongo Express, and supporting UIs.

## Prerequisites

- .NET 10 SDK installed
- Docker Desktop with Docker Compose support
- Git clone of this repository

## Running Locally

### Option 1: Using Docker Compose

From the repository root:

```bash
docker compose up --build
```

This starts:

- PostgreSQL on `localhost:5432`
- Zookeeper on `localhost:2181`
- Kafka on `localhost:9092`
- Debezium Connect on `localhost:8083`
- Kafka UI on `localhost:8090`
- Debezium UI on `localhost:8080`
- MongoDB on `localhost:27017`
- Mongo Express on `localhost:8081`
- API Gateway on `localhost:5277`
- Write API on `localhost:5067`
- Read API on `localhost:5228`

To stop the environment:

```bash
docker compose down
```

### Option 2: Running Projects Individually

From the repository root, build the solution:

```bash
dotnet build TcmbForexApi.slnx
```

Run each service with its project path:

```bash
dotnet run --project src/TcmbForexApi.WriteAPI/TcmbForexApi.WriteAPI.csproj

dotnet run --project src/TcmbForexApi.ReadAPI/TcmbForexApi.ReadAPI.csproj

dotnet run --project src/TcmbForexApi.Gateway/TcmbForexApi.Gateway.csproj

dotnet run --project src/TcmbForexApi.Consumers/TcmbForexApi.Consumers.csproj
```

## Service Endpoints

### Gateway Endpoints

Use the API gateway at `http://localhost:5277`:

- `GET /gateway/forex-rates?code={code}`
- `GET /gateway/forex-rates?date={date}`
- `GET /gateway/forex-rates?code={code}&date={date}`
- `POST /gateway/write-forex-latest`
- `POST /gateway/write-custom-forex`

### Read API Endpoints

Direct read API routes:

- `GET /read/forex-by-code?code={code}`
- `GET /read/forex-by-date?date={date}`
- `GET /read/forex-by-code-and-date?code={code}&date={date}`

### Write API Endpoints

Direct write API routes:

- `POST /write/forex-latest`
- `POST /write/forex-custom`

## Configuration

Configuration files are located in each service directory:

- `src/TcmbForexApi.WriteAPI/appsettings.json`
- `src/TcmbForexApi.ReadAPI/appsettings.json`
- `src/TcmbForexApi.Gateway/ocelot.json`
- `src/TcmbForexApi.Consumers/appsettings.json`

Key configured services:

- PostgreSQL connection string in `WriteAPI`
- MongoDB connection string in `ReadAPI`
- Ocelot routing in `Gateway`
- Kafka consumer settings in `Consumers`

## Notes

- `Consumers` expects a Debezium-style payload from Kafka and ignores read operations.
- `Gateway` forwards incoming requests to the corresponding read/write APIs based on Ocelot routing.
- MongoDB and PostgreSQL container hostnames are defined in `docker-compose.yml`.

## Helpful Commands

- Build solution: `dotnet build TcmbForexApi.slnx`
- Run gateway: `dotnet run --project src/TcmbForexApi.Gateway/TcmbForexApi.Gateway.csproj`
- Run write API: `dotnet run --project src/TcmbForexApi.WriteAPI/TcmbForexApi.WriteAPI.csproj`
- Run read API: `dotnet run --project src/TcmbForexApi.ReadAPI/TcmbForexApi.ReadAPI.csproj`
- Run consumer: `dotnet run --project src/TcmbForexApi.Consumers/TcmbForexApi.Consumers.csproj`

## Project Structure

- `src/TcmbForexApi.WriteAPI` – write endpoints and PostgreSQL integration
- `src/TcmbForexApi.ReadAPI` – read endpoints and MongoDB integration
- `src/TcmbForexApi.Gateway` – Ocelot gateway configuration and routing
- `src/TcmbForexApi.Consumers` – Kafka consumer and rate persistence
- `docker-compose.yml` – local development containers and integration services
