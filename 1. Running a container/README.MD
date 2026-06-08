# Opdracht 1: Docker containers

Open je terminal en start WSL, dit is waar we alles gaan doen.

Run het volgende commando: 
`sudo docker run hello-world`

Je zou nu de volgende output moeten zien verschijnen:
```
Hello from Docker!
This message shows that your installation appears to be working correctly.

To generate this message, Docker took the following steps:
 1. The Docker client contacted the Docker daemon.
 2. The Docker daemon pulled the "hello-world" image from the Docker Hub.
    (amd64)
 3. The Docker daemon created a new container from that image which runs the
    executable that produces the output you are currently reading.
 4. The Docker daemon streamed that output to the Docker client, which sent it
    to your terminal.

To try something more ambitious, you can run an Ubuntu container with:
 $ docker run -it ubuntu bash

Share images, automate workflows, and more with a free Docker ID:
 https://hub.docker.com/

For more examples and ideas, visit:
 https://docs.docker.com/get-started/
```

Als je nu `sudo docker ps` runt, zal je zien dat de container zelf alweer is afgesloten omdat hij zijn einde heeft bereikt. Beetje saai wel.

Volg de suggestie uit de hello-world op en run `docker run -it ubuntu bash`.
Je hebt nu een interactive BASH sessie in een ubuntu container. 
Gerbruik `exit` om de container te veralten.

Laten we een wat uitdagendere image starten.

Run het volgende commando:
```
sudo docker run -d \
  --name=emulatorjs \
  -e PUID=1000 \
  -e PGID=1000 \
  -e TZ=Etc/UTC \
  -e SUBFOLDER=/ `#optional` \
  -p 3000:3000 \
  -p 80:80 \
  -p 4001:4001 `#optional` \
  -v /path/to/config:/config \
  -v /path/to/data:/data \
  --restart unless-stopped \
  lscr.io/linuxserver/emulatorjs:1.9.2
```

Run nogmaals "sudo docker ps" om te zien dat de container inderdaad draait.

Als je nu in je webbrowser naar `http://localhost:3000` gaat krijg je de emulator pagina te zien.

