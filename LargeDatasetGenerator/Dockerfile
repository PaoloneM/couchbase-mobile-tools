#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:2.1-stretch-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:2.1-stretch AS build
WORKDIR /src
COPY ["LargeDatasetGenerator/LargeDatasetGenerator.csproj", "LargeDatasetGenerator/"]
COPY ["LargeDatasetGenerator.Abstractions/LargeDatasetGenerator.Abstractions.csproj", "LargeDatasetGenerator.Abstractions/"]
COPY ["LargeDatasetGenerator.Core/LargeDatasetGenerator.Core.csproj", "LargeDatasetGenerator.Core/"]
COPY ["LargeDatasetGenerator.CouchbaseLite/LargeDatasetGenerator.CouchbaseLite.csproj", "LargeDatasetGenerator.CouchbaseLite/"]
COPY ["LargeDatasetGenerator.CouchbaseServer/LargeDatasetGenerator.CouchbaseServer.csproj", "LargeDatasetGenerator.CouchbaseServer/"]
COPY ["LargeDatasetGenerator.SyncGateway/LargeDatasetGenerator.SyncGateway.csproj", "LargeDatasetGenerator.SyncGateway/"]
RUN dotnet restore "LargeDatasetGenerator/LargeDatasetGenerator.csproj"
COPY . .
WORKDIR "/src/LargeDatasetGenerator"
RUN dotnet build "LargeDatasetGenerator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LargeDatasetGenerator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY ["run.sh", "/app/run.sh"]
RUN chmod +x /app/run.sh
ENV TZ=Europe/Rome
RUN ln -snf /usr/share/zoneinfo/$TZ /etc/localtime && echo $TZ > /etc/timezone
ENTRYPOINT ["/app/run.sh"]