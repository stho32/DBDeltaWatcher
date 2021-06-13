#!/snap/bin/pwsh

<#
    .SYNOPSIS
    Opens the remote desktop connection to the environment 
#>

Set-Location $PSScriptRoot
$ErrorActionPreference = "Stop"

$instance = Get-Content "environment-documentation.json" | ConvertFrom-Json

$encryptedPassword = ($instance.default_password | remmina --encrypt-password | Where-Object { $_.StartsWith("Encrypted") }).Replace("Encrypted password: ", "").Trim() 

Write-Host "remmina rdp://Administrator:$encryptedPassword@$($instance.main_ip)" -ForegroundColor Cyan
