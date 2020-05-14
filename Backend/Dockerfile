#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY src/WSChat.Backend.API/WSChat.Backend.API.csproj src/WSChat.Backend.API/
COPY src/WSChat.Backend.Lib.ChatWebSocket/WSChat.Backend.Lib.ChatWebSocket.csproj src/WSChat.Backend.Lib.ChatWebSocket/
COPY src/WSChat.Backend.Application/WSChat.Backend.Application.csproj src/WSChat.Backend.Application/
COPY src/WSChat.Backend.Domain/WSChat.Backend.Domain.csproj src/WSChat.Backend.Domain/
RUN dotnet restore "src/WSChat.Backend.API/WSChat.Backend.API.csproj"
COPY . .
WORKDIR "/src/src/WSChat.Backend.API"
RUN dotnet build "WSChat.Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSChat.Backend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WSChat.Backend.API.dll"]

