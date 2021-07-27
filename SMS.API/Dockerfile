#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["SMS.API/SMS.API.csproj", "SMS.API/"]
COPY ["SMS.Application/SMS.Application.csproj", "SMS.Application/"]
COPY ["EventBus/EventBus.csproj", "EventBus/"]
COPY ["SMS.Infrastructure/SMS.Infrastructure.csproj", "SMS.Infrastructure/"]
RUN dotnet restore "SMS.API/SMS.API.csproj"
COPY . .
WORKDIR "/src/SMS.API"
RUN dotnet build "SMS.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SMS.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SMS.API.dll"]