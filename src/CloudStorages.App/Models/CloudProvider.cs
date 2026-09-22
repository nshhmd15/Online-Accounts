namespace CloudStorages.App.Models;

public sealed class CloudProvider
{
    public string Name { get; init; } = "";
    public string Subtitle { get; init; } = "";
    public string IconGlyph { get; init; } = "☁";
    public double UsedBytes { get; init; }
    public double TotalBytes { get; init; }
    public bool Connected { get; init; }
    public string DriveLetter { get; init; } = "";

    public double FreeBytes => Math.Max(0, TotalBytes - UsedBytes);
    public double UsageRatio => TotalBytes <= 0 ? 0 : Math.Clamp(UsedBytes / TotalBytes, 0, 1);
    public string UsedText => FormatBytes(UsedBytes);
    public string FreeText => FormatBytes(FreeBytes);
    public string TotalText => FormatBytes(TotalBytes);
    public string CapacityText => $"{FreeText} free of {TotalText}";

    private static string FormatBytes(double bytes)
    {
        string[] units = ["B", "KB", "MB", "GB", "TB", "PB"];
        var value = bytes;
        var unit = 0;
        while (value >= 1024 && unit < units.Length - 1) { value /= 1024; unit++; }
        return unit == 0 ? $"{value:0} {units[unit]}" : $"{value:0.##} {units[unit]}";
    }
}
