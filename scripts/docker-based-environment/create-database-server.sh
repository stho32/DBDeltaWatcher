#!/bin/bash
docker pull mcr.microsoft.com/mssql/server
docker run -e 'ACCEPT_EULA=Y' -e "SA_PASSWORD=Start123!" -p 1433:1433 -d mcr.microsoft.com/mssql/server
