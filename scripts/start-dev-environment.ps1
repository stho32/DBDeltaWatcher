#!/snap/bin/pwsh

<#
    .SYNOPSIS
    Setup and start a development environment for this project
#>

Set-Location $PSScriptRoot
$ErrorActionPreference = "Stop"

$setupScript = "
powershell -command `"Set-ExecutionPolicy Bypass -Scope Process -Force; [System.Net.ServicePointManager]::SecurityProtocol = [System.Net.ServicePointManager]::SecurityProtocol -bor 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))`"

C:\ProgramData\chocolatey\bin\choco install Firefox -y
C:\ProgramData\chocolatey\bin\choco install git -y
C:\ProgramData\chocolatey\bin\choco install jetbrains-rider -y

mkdir C:\Projekte
cd C:\Projekte
git clone https://github.com/stho32/DevToolsPS
git clone https://github.com/stho32/DBDeltaWatcher
"

$instance = New-VULTRInstance -OperatingSystem "Windows 2019 x64" -Plan "vc2-6c-16gb" -ProvisionUsingScript $setupScript

$instance | ConvertTo-Json | Out-File "environment-documentation.json" -Force

$instance
