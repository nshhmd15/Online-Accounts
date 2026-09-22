# Build notes

The project is deliberately x64-first for Windows 11.

The current UI project is safe to build and inspect without credentials. Provider API work is intentionally isolated so credentials are never hard-coded into the application.

For real cloud drives, add a `CloudStorages.FileSystem` project that references WinFsp.NET or the native WinFsp API and implement the provider-backed filesystem. Keep the WinUI application responsible for configuration and IPC only.
