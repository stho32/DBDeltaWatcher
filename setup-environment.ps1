#!/snap/bin/pwsh

Set-Location $PSScriptRoot

Write-Host "  - building dotnet application ..." -ForegroundColor yellow

Push-Location source/dotnet/DbDeltaWatcher
dotnet restore
dotnet build
Pop-Location

Write-Host "  - rebuild database ..." -ForegroundColor yellow

Push-Location source/t-sql
&./setup-database.sh
Pop-Location

Write-Host "  - build complete ..." -ForegroundColor yellow
