#!/snap/bin/pwsh

Set-Location $PSScriptRoot

Write-Host "  - building dotnet application ..." -ForegroundColor yellow

Push-Location source/dotnet/DbDeltaWatcher
dotnet restore
dotnet build
dotnet test
Pop-Location

Write-Host "  - build complete ..." -ForegroundColor yellow
