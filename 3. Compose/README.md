# Opdracht 3: Docker compose
Het doel van deze opdracht is om een docker-compose file te maken waarmee het volgende gebeurd:
- De todo app wordt gehost:
    - Build de image in de compose
    - Expose poort 8080
    - voeg een appsettings.json toe met een connectionstring via een volume mount.
    - koppel een intern todo-netwerk
- Er wordt een postgresql database gehost:
    - Run de alpine image
    - expose geen poorten
    - zorg voor een volume van de pg_data folder voor persistency
    - koppel een intern todo-netwerk
    - stel het superuser wachtwoord in
- Definieer de juiste volumes
- Definieer de netwerken

De docker-compose.yaml file bevat alvast een startpunt. 
Gebruik docker docs voor referentie `https://docs.docker.com/reference/compose-file/networks/`
