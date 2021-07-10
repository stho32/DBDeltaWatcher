#!/bin/bash

echo "  - Installing mysql-server ..."
sudo apt-get install mysql-server -y
echo "--------------------------"
echo " set password : Start123! "
echo " thanks "
echo "--------------------------"

sudo mysql_secure_installation

echo "  - Installing SQL Server ..."
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/20.04/mssql-server-2019.list)"
sudo apt-get update
sudo apt-get install -y mssql-server
sudo /opt/mssql/bin/mssql-conf setup

sudo apt install curl -y
curl https://packages.microsoft.com/config/ubuntu/20.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.list
sudo apt-get update
sudo apt-get install mssql-tools unixodbc-dev -y

echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bash_profile
echo 'export PATH="$PATH:/opt/mssql-tools/bin"' >> ~/.bashrc
source ~/.bashrc

cp ./IntegrationTestConfiguration.json_template ./source/dotnet/DbDeltaWatcher/IntegrationTestConfiguration.json

