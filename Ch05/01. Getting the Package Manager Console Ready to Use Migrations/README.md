
### For Running Project Through Docker Compose

```SHELL
docker compose build
docker compose up
```

### For Running Project Separated with Docker [Perferred As it Console App]

```SHELL
docker build -it ef6fundamentalcourse .

docker run -it --rm --name mssql -p 1433:1433 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=P@ass_LocalSql19" 
\ -v ./DockerConfig/Sqlserver/data:/var/opt/mssql/data
\ -v ./DockerConfig/Sqlserver/log:/var/opt/mssql/log
\ -v ./DockerConfig/Sqlserver/secrets:/var/opt/mssql/secrets
\ mcr.microsoft.com/mssql/server:2019-latest

docker run -it --rm --name ef5 --link mssql:mssql ef6fundamentalcourse:latest
```
