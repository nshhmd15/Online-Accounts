#define MyAppName "Cloud Storages"
#define MyAppVersion "0.1.0"
#define MyAppPublisher "Cloud Storages"
#define MyAppExeName "CloudStorages.App.exe"

[Setup]
AppId={{8D0C1E6C-8D6A-4F7C-AF0E-0F8A4B3E4C21}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={autopf}\Cloud Storages
DefaultGroupName=Cloud Storages
OutputDir=..\release
OutputBaseFilename=Cloud-Storages-Setup
Compression=lzma2
SolidCompression=yes
ArchitecturesInstallIn64BitMode=x64
PrivilegesRequired=admin
WizardStyle=modern
UninstallDisplayIcon={app}\{#MyAppExeName}

[Files]
Source: "..\publish\*"; DestDir: "{app}"; Flags: recursesubdirs ignoreversion

[Icons]
Name: "{autoprograms}\Cloud Storages"; Filename: "{app}\{#MyAppExeName}"
Name: "{autodesktop}\Cloud Storages"; Filename: "{app}\{#MyAppExeName}"

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "Launch Cloud Storages"; Flags: nowait postinstall skipifsilent
