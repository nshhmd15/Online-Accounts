using CloudStorages.App.Models;
using CloudStorages.App.Services;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System.Diagnostics;

namespace CloudStorages.App;

public sealed partial class MainWindow : Window
{
    private readonly IReadOnlyList<CloudProvider> _providers = ProviderCatalog.GetDesignTimeProviders();

    public MainWindow()
    {
        InitializeComponent();
        ExtendsContentIntoTitleBar = true;
        SetTitleBar(AppTitleBar);
        Nav.SelectedItem = Nav.MenuItems[0];
        RenderProviders();
    }

    private void RenderProviders()
    {
        LeftCards.Children.Clear();
        RightCards.Children.Clear();
        for (var i = 0; i < _providers.Count; i++)
        {
            var card = BuildCard(_providers[i]);
            (i % 2 == 0 ? LeftCards : RightCards).Children.Add(card);
        }
    }

    private FrameworkElement BuildCard(CloudProvider p)
    {
        var border = new Border { Style = (Style)Resources["Card"] };
        var root = new StackPanel();
        var header = new Grid();
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        header.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

        var icon = new Border { Width = 42, Height = 42, CornerRadius = new CornerRadius(12), Background = (Brush)Application.Current.Resources["AccentFillColorSecondaryBrush"] };
        icon.Child = new TextBlock { Text = p.IconGlyph, FontSize = 18, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
        Grid.SetColumn(icon, 0); header.Children.Add(icon);

        var titles = new StackPanel { Margin = new Thickness(12, 0, 0, 0), VerticalAlignment = VerticalAlignment.Center };
        titles.Children.Add(new TextBlock { Text = p.Name, FontSize = 17, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold });
        titles.Children.Add(new TextBlock { Text = $"{p.Subtitle}  •  {p.DriveLetter}", FontSize = 12, Opacity = 0.65 });
        Grid.SetColumn(titles, 1); header.Children.Add(titles);

        var status = new TextBlock { Text = p.Connected ? "Connected" : "Not connected", FontSize = 12, Opacity = 0.7, VerticalAlignment = VerticalAlignment.Center };
        Grid.SetColumn(status, 2); header.Children.Add(status);
        root.Children.Add(header);

        var used = new TextBlock { Text = $"{p.UsedText} used", FontSize = 25, FontWeight = Microsoft.UI.Text.FontWeights.SemiBold, Margin = new Thickness(0, 22, 0, 0) };
        root.Children.Add(used);
        var bar = new ProgressBar { Value = p.UsageRatio * 100, Height = 7, Margin = new Thickness(0, 10, 0, 7) };
        root.Children.Add(bar);
        root.Children.Add(new TextBlock { Text = p.CapacityText, FontSize = 12, Opacity = 0.65 });

        var actions = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Margin = new Thickness(0, 18, 0, 0) };
        var open = new Button { Content = "Open in Explorer" };
        open.Click += (_, _) => OpenDrive(p.DriveLetter);
        actions.Children.Add(open);
        var connect = new Button { Content = p.Connected ? "Manage" : "Connect", Style = (Style)Application.Current.Resources["DefaultButtonStyle"] };
        connect.Click += (_, _) => StatusBar.Message = $"{p.Name}: provider authentication and filesystem adapter are the next integration layer.";
        actions.Children.Add(connect);
        root.Children.Add(actions);
        border.Child = root;
        return border;
    }

    private static void OpenDrive(string drive)
    {
        try
        {
            if (!string.IsNullOrWhiteSpace(drive)) Process.Start(new ProcessStartInfo("explorer.exe", $"{drive}\\") { UseShellExecute = true });
        }
        catch { }
    }

    private void Settings_Click(object sender, RoutedEventArgs e) => StatusBar.Message = "Settings will control provider accounts, drive letters, cache policy and startup behavior.";
    private void Nav_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args) { }
}
