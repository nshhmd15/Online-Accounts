# Cloud Storages — Windows 11

Modern Windows 11 desktop shell for a multi-provider cloud drive application.

## Target UX

Each connected provider is intended to appear in **This PC > Devices and drives** as its own drive-style volume:

- Google Drive
- OneDrive
- MEGA
- Degoo Cloud

The Explorer entry is intended to expose real free/used capacity so Windows can render the normal drive capacity bar, like `Windows (C:)`.

## Architecture

- UI: WinUI 3 / Windows App SDK, using Mica and Fluent controls.
- Filesystem layer: WinFsp user-mode filesystem, which can expose a provider-backed filesystem as a Windows drive letter.
- Provider adapters: separate modules for OAuth/API access and remote filesystem operations.
- No whole-cloud local mirror is required; files can be fetched on demand.

Microsoft documents WinUI 3 as the recommended native framework for new Windows desktop apps, and its Mica/title-bar/navigation patterns are used by this project. The Windows Cloud Files API is another possible Explorer integration route, but this project targets drive-letter behavior because the requested UX is explicitly like `C:`. WinFsp is designed to expose user-mode filesystems as Windows drives.

## Important current status

This package contains the **modern UI and build/installer scaffold**, plus the provider/drive architecture. It does **not** yet contain production OAuth credentials or the complete remote filesystem implementations for Google Drive, OneDrive, MEGA, and Degoo. Those adapters must be implemented and tested against each provider's current API before the drive is used for real cloud data.

Do not treat the sample quota numbers in the dashboard as real account data.

## Build on Windows 11

Prerequisites:

1. Visual Studio 2022/2026 with .NET desktop development and Windows App SDK/WinUI support.
2. Windows 11 SDK.
3. .NET 8 SDK.
4. WinFsp runtime/development package for the filesystem layer.
5. Inno Setup if you want `Cloud-Storages-Setup.exe`.

Run PowerShell:

```powershell
Set-ExecutionPolicy -Scope Process Bypass
.\build.ps1
```

The application publish output goes to `publish`. If Inno Setup is installed and `iscc.exe` is on PATH, the installer is written to `release\Cloud-Storages-Setup.exe`.

## Next implementation layer

Implement these behind the provider interface:

- OAuth login/token storage.
- List directories and files.
- Open/read remote files.
- Create/write/upload files.
- Rename/move/delete.
- Remote-to-local streaming and local-to-remote streaming.
- Quota/free-space query.
- Offline/error states.
- Per-provider cache and retry policy.
- WinFsp filesystem callbacks.

For a production release, code-sign both the application and installer and distribute WinFsp using its own supported installer/licensing terms.
