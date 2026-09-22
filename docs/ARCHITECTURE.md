# Architecture

Cloud Storages has three layers.

## 1. WinUI shell

`CloudStorages.App` is a native WinUI 3 Windows 11 application. It provides the modern dashboard, account management, storage bars, settings and activity UI.

## 2. Provider adapters

Each provider implements a common abstraction:

```text
ICloudProvider
  AuthenticateAsync()
  GetQuotaAsync()
  ListAsync(path)
  OpenReadAsync(path)
  OpenWriteAsync(path)
  CreateDirectoryAsync(path)
  MoveAsync(source, destination)
  DeleteAsync(path)
```

Provider implementations must use the official/current APIs and OAuth flows for the provider.

## 3. WinFsp filesystem

A separate filesystem host receives normal Windows file operations and translates them into provider adapter calls. It mounts each connected provider at a selected drive letter, e.g. `G:` or `O:`.

The filesystem host must report provider quota through the filesystem volume information so Windows Explorer can display capacity/free-space information.

The filesystem process should remain separate from the GUI. WinFsp's documentation recommends a dedicated user-mode filesystem process/service rather than putting GUI behavior in the filesystem process.
