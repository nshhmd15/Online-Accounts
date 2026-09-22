# Build Cloud Storages online with GitHub Actions

## 1. Create a GitHub repository

Create an empty repository on GitHub, then upload the contents of this folder. Make sure the `.github` folder is included.

## 2. Push/upload the files

The repository must contain:

- `CloudStorages.sln`
- `src/CloudStorages.App/...`
- `Installer/CloudStorages.iss`
- `.github/workflows/build-windows.yml`

## 3. Run the cloud build

Open the repository on GitHub and select **Actions**. You will see **Build Cloud Storages**.

Select it, click **Run workflow**, and run it on the `main` branch.

The workflow uses a GitHub-hosted Windows runner, publishes the x64 WinUI application, installs Inno Setup, and creates:

`Cloud-Storages-Setup.exe`

## 4. Download the EXE

After the workflow completes:

**Actions → Build Cloud Storages → latest successful run → Artifacts**

Download:

`Cloud-Storages-Windows11-Installer`

Inside it is:

`Cloud-Storages-Setup.exe`

## Important

This build workflow packages the current source project. The current provider cards/UI are a prototype foundation; Google Drive, OneDrive, MEGA and Degoo filesystem/authentication adapters still need to be implemented before those cloud locations can function as real drives in This PC.
