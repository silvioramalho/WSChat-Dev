# WSCHAT

Websocket Chat: C# .net core + Angular 

DEMO: https://wschat-backend.herokuapp.com

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

`Note: The websocket server will be available on => ws:localhost:5000/ws`

## Tests

### Units

> dotnet test tests/WSCHat.Backend.Tests/WSCHat.Backend.Tests.csproj

### Web Client

    Open in Google Chrome the file: `tests/webClient/index.html`

## Backend on Docker Container

### Build
docker build -t wschat-backend-img .

### Run on port 5000
docker run -it --rm -p 5000:80 wschat-backend-img

### [See more info about Backend on this link](Backend/README.md)

## Frontend

Simple websocket client for chat.

### Access directory

> cd WSChat-Dev/Frontend

### Restore packages

> npm install

### Development server

> ng serve

`Note: The frontend will be available on => http://localhost:4200/chat/register`

### [See more info about Frontend on this link](Frontend/README.md)


## Docker Container

The Dockerfile found in the root directory joins the frontend and the backend in a single container.

To generate it, you need to build the frontend app before running the command below:

> docker build -t `<name-docker-image>`  .

`Note: The Dockerfile inside the Backend directory generates a container with Backend only.` [more info](Backend/README.md)
