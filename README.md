The Repository Includes Follow up the any code changes done during every Video under [EF Core 6 Fundamental Course](https://app.pluralsight.com/library/courses/ef-core-6-fundamentals/), I divided Chapters under Course as Folder and every Folder include folder based on every Video's name under the Course.

Under Every Video Folder include Full Project details and Docker, Docker compose files Which Can Run Project.

***NOTE*** The Number of Video Folder Just for ording videos baesd on display under Every Chapter only

### For Running Project 

1. Navigate to Any Video Folder

2. Open Terminal And Run Project 

    2.1. Run with Docker Compose

    ```SHELL
    docker compose build
    docker compose up
    ```

    2.2. Run Project manually Separated with Docker [Build Console App then Run it and Run MSSQL Image Separated]

    ```SHELL
    docker build -it ef6fundamentalcourse .

    docker run -it --rm --name mssql -p 1433:1433 -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD={DATABASE_PASSWORD_ENV}" 
    \ -v ./DockerConfig/Sqlserver/data:/var/opt/mssql/data
    \ -v ./DockerConfig/Sqlserver/log:/var/opt/mssql/log
    \ -v ./DockerConfig/Sqlserver/secrets:/var/opt/mssql/secrets
    \ mcr.microsoft.com/mssql/server:2019-latest

    docker run -it --rm --name ef5 --link mssql:mssql ef6fundamentalcourse:latest
    ```
