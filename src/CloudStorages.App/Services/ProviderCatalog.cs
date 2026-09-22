using CloudStorages.App.Models;

namespace CloudStorages.App.Services;

public static class ProviderCatalog
{
    public static IReadOnlyList<CloudProvider> GetDesignTimeProviders() =>
    [
        new() { Name="Google Drive", Subtitle="Google account", IconGlyph="G", UsedBytes=63.4 * Math.Pow(1024,3), TotalBytes=100 * Math.Pow(1024,3), DriveLetter="G:" },
        new() { Name="OneDrive", Subtitle="Microsoft account", IconGlyph="O", UsedBytes=816.3 * Math.Pow(1024,3), TotalBytes=1024 * Math.Pow(1024,3), DriveLetter="O:" },
        new() { Name="MEGA", Subtitle="MEGA account", IconGlyph="M", UsedBytes=14.2 * Math.Pow(1024,3), TotalBytes=20 * Math.Pow(1024,3), DriveLetter="M:" },
        new() { Name="Degoo Cloud", Subtitle="Degoo account", IconGlyph="D", UsedBytes=17.4 * Math.Pow(1024,3), TotalBytes=100 * Math.Pow(1024,3), DriveLetter="D:" }
    ];
}
