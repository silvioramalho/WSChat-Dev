# WSCHAT

Websocket Chat: C# .net core + Angular 

## Clone repository:

```
git clone git@github.com:silvioramalho/WSChat-Dev.git
```

## Backend

### Access directory

> cd WSChat-Dev/Backend

### Restore packages

> dotnet restore

### Run

> dotnet run --project src/WSChat.Backend.API/WSChat.Backend.API.csproj

`Note: Websocket server will run on => wss:localhost:5001/ws`

## Tests

### Units

> dotnet test tests/WSCHat.Backend.Tests/WSCHat.Backend.Tests.csproj

### Web Client

    Open in Google Chrome the file: `tests/webClient/index.html`


## Frontend

Simple websocket client for chat.

### Access directory

> cd WSChat-Dev/Frontend

### Restore packages

> npm install

### Development server

> ng serve

`Note: Frontend will run on => http://localhost:4200/chat/register`

### [See more info about Frontend on this link](Frontend/README.md)



