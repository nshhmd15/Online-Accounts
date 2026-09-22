$ErrorActionPreference = 'Stop'
$root = Split-Path -Parent $MyInvocation.MyCommand.Path
Set-Location $root

Write-Host "Building Cloud Storages..." -ForegroundColor Cyan
New-Item -ItemType Directory -Force -Path .\publish, .\release | Out-Null

dotnet restore .\src\CloudStorages.App\CloudStorages.App.csproj
dotnet publish .\src\CloudStorages.App\CloudStorages.App.csproj -c Release -r win-x64 --self-contained true -p:Platform=x64 -p:WindowsAppSDKSelfContained=true -o .\publish

$iss = 'C:\Program Files (x86)\Inno Setup 6\ISCC.exe'
if (Test-Path $iss) {
    & $iss .\Installer\CloudStorages.iss
    if ($LASTEXITCODE -ne 0) { throw "Inno Setup failed with exit code $LASTEXITCODE" }
    Write-Host "Installer created: .\release\Cloud-Storages-Setup.exe" -ForegroundColor Green
} else {
    Write-Host "Application build completed in .\publish" -ForegroundColor Green
    Write-Host "Install Inno Setup to create the installer." -ForegroundColor Yellow
}
