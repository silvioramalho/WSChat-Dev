# You must build before using this block (npm run build or ng build)
# If you don't want to build frontend app manually, uncomment the lines below.
FROM node:10.13-alpine AS angular
WORKDIR /ng-app
# COPY Frontend/package.json /ng-app
# RUN npm install --silent
COPY Frontend/. .
# RUN npm run build

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY Backend/src/WSChat.Backend.API/WSChat.Backend.API.csproj src/WSChat.Backend.API/
COPY Backend/src/WSChat.Backend.Lib.ChatWebSocket/WSChat.Backend.Lib.ChatWebSocket.csproj src/WSChat.Backend.Lib.ChatWebSocket/
COPY Backend/src/WSChat.Backend.Application/WSChat.Backend.Application.csproj src/WSChat.Backend.Application/
COPY Backend/src/WSChat.Backend.Domain/WSChat.Backend.Domain.csproj src/WSChat.Backend.Domain/
RUN dotnet restore "/src/src/WSChat.Backend.API/WSChat.Backend.API.csproj"
COPY Backend/. .
WORKDIR "/src/src/WSChat.Backend.API"
RUN dotnet build "WSChat.Backend.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WSChat.Backend.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=angular ng-app/dist/Frontend /app/wwwroot
ENTRYPOINT ["dotnet", "WSChat.Backend.API.dll"]


