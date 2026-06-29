# Opdracht 2: Docker images

## 2.1 Je eerste image
Bij deze opdracht zit een voorgemaakt ASP.NET TodoApp. Van deze applicatie gaan we een docker image bouwen, en dan gaan we die runnen. 
In de map TodoApp vind je een `dockerfile`. Edit deze file om een image te maken van je applicatie.

Build de image:
`docker build -t todo .`

En run:
`docker run -p 8080:8080 --name todo todo`

De app is nu beschikbaar op https://localhost:8080

Let op: als je de app herstart, is opeens je todo list weg!
Gelukkig is de app hier om voorbereid.

Run je container met een volume (-v) op de map /app/Data/ en kijk wat er nu gebeurt als je herstart!

## 2.2 Multi-stage image
De 1e image die je hebt gemaakt gebruikt waarschijnlijk nog de image `mcr.microsoft.com/dotnet/sdk:8.0-alpine`. 
Dit is een hele grote image met allerlei tools die niet nodig zijn op je productie systeem.

Probeer je dockerfile te optimaliseren met een multi-stage build.
Maak van de eerste FROM een "build" stage die de SDK image gebruikt.
Voeg een tweede FROM toe als "final" op basis van de image `mcr.microsoft.com/dotnet/aspnet:8.0-alpine`.
Optimaliseer nu je image en run opnieuw!

## 2.3 Bonus: een eigen base image maken
Voor je productie systeem heb je uitgebreide timezone informatie nodig en je wil niet in elke dockerfile opnieuw je dependencies installeren.
Maak een dockerfile waarmee je de image `mcr.microsoft.com/dotnet/aspnet:8.0-alpine` uitbreid met de package `apk add --no-cache tzdata` en de timezone instelt op `TZ=Europe/Amsterdam`.
Bouw deze image als `base`. 

Pas vervolgens de multi-stage build aan dat de FINAL stage gebaseerd is op `base`.
