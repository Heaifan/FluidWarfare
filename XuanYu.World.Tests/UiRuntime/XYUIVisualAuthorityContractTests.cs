using System.IO;
using System.Text.RegularExpressions;
using Avalonia.Controls;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class XYUIVisualAuthorityContractTests
{
    readonly UiHeadlessFixture _fixture;

    public XYUIVisualAuthorityContractTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Native_buttons_are_explicitly_legacy_scoped()
    {
        var root = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "..", "XuanYu.Editor.UI");
        var missing = Directory.GetFiles(root, "*.axaml", SearchOption.AllDirectories)
            .SelectMany(file => Regex.Matches(File.ReadAllText(file), "<Button\\b[^>]*>")
                .Cast<Match>()
                .Where(match => !match.Value.Contains("legacyButton"))
                .Select(match => file + ": " + match.Value));

        Assert.Empty(missing);
        Assert.Contains("legacyButton", Read("XuanYu.Editor.UI", "Win", "UiWin.DialogHost.cs"));
    }

    [Fact]
    public void Global_button_styles_are_scoped_away_from_xyui_controls()
    {
        var ui = Read("XuanYu.Editor.UI", "Ui.axaml");
        var d5 = Read("XuanYu.Editor.UI", "Design", "UiStyles.D5.axaml");
        foreach (var source in new[] { ui, d5 })
        {
            Assert.DoesNotContain("<Style Selector=\"Button\">", source);
            Assert.DoesNotContain("<Style Selector=\"Button:pointerover\">", source);
            Assert.DoesNotContain("<Style Selector=\"Button:pressed\">", source);
            Assert.DoesNotContain("<Style Selector=\"Button:focus-visible\">", source);
            Assert.DoesNotContain("<Style Selector=\"Button:disabled\">", source);
            Assert.DoesNotContain("<Style Selector=\"ToggleButton", source);
        }

        Assert.Contains("Button.legacyButton", ui);
        Assert.Contains("Button.legacyButton:pointerover", d5);
        Assert.Contains("Button.legacyButton:pressed", d5);
        Assert.Contains("Button.legacyButton:disabled", d5);
    }

    [Fact]
    public void Area_a_hosts_real_xyui_runtime_controls()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var counts = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            vm.ToggleEditorMode();
            var top = new Top { DataContext = vm };
            host.Show(top, 1200, 180);
            top.UpdateLayout();
            return (
                Buttons: UiRuntimeTestHost.Descendants<XYButton>(top).Count(),
                Toggles: UiRuntimeTestHost.Descendants<XYToggleButton>(top).Count(),
                Badges: UiRuntimeTestHost.Descendants<XYBadge>(top).Count());
        });

        Assert.True(counts.Buttons > 0);
        Assert.True(counts.Toggles > 0);
        Assert.True(counts.Badges > 0);
    }

    [Fact]
    public void Xyui_theme_is_registered_once_in_bootstrap()
    {
        var source = Read("XuanYu.Editor.UI", "Bootstrap", "App.axaml.cs");
        Assert.Single(Regex.Matches(source, "XyuiTheme.CreateThemeDictionaries\\(\\)"));
    }

    static string Read(params string[] path) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", Path.Combine(path)));
}
