# WSChat Backend

## DEVELOPMENT 

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

## Docker

### Create Image

> docker build -t `<docker-image-name>` .

### Run on por 5000

> docker run -it --rm -p 5000:80 `<docker-image-name>`

## Deploy to Heroku (after build)

`Note: Create app on heroku before continue.`

```
docker tag <docker-image-name> registry.heroku.com/<heroku-app-name>/web

heroku login
heroku container:login

heroku container:push web -a <heroku-app-name>
heroku container:release web -a <heroku-app-name>
```

### View logs

> heroku logs --tail --app `<heroku-app-name>`
