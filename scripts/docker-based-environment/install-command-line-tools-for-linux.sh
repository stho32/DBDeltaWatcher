#!/bin/bash
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile

curl https://packages.microsoft.com/config/ubuntu/19.10/prod.list > /etc/apt/sources.list.d/mssql-release.list
sudo apt update
sudo ACCEPT_EULA=Y apt install mssql-tools unixodbc-dev

