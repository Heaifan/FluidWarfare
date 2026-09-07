using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Foundation;
using XYUI.Avalonia.Theme;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class UiR1VisualFixContractTests
{
    readonly UiHeadlessFixture _fixture;
    public UiR1VisualFixContractTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Section_title_uses_frozen_secondary_mark_contract_in_both_themes()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var state = host.Run(() =>
        {
            Assert.True(XyuiColorTokens.TryFind("XY.Text.Secondary", out var token));
            var light = XyuiSectionTitleResources.Create(false);
            var dark = XyuiSectionTitleResources.Create(true);
            var section = new XYSectionTitle { Text = "地图属性" };
            host.Show(section, 320, 28);
            section.ApplyStyling();
            var header = Assert.IsType<Grid>(section.Child);
            var mark = Assert.IsType<Border>(header.Children[0]);
            var text = Assert.IsType<TextBlock>(header.Children[1]);
            return (token.DarkHex,
                Light: ColorOf(light[XyuiSectionTitleResources.LeftMarkKey]),
                Dark: ColorOf(dark[XyuiSectionTitleResources.LeftMarkKey]),
                SectionHeight: section.Height, CornerRadius: section.CornerRadius,
                HeaderHeight: header.Height, MarkWidth: mark.Width, MarkHeight: mark.Height,
                MarkVerticalAlignment: mark.VerticalAlignment,
                TextVerticalAlignment: text.VerticalAlignment);
        });

        Assert.Equal("#B3BFC6", state.DarkHex);
        Assert.Equal(Color.Parse("#526873"), state.Light);
        Assert.Equal(Color.Parse("#B3BFC6"), state.Dark);
        Assert.Equal(28, state.SectionHeight);
        Assert.Equal(new CornerRadius(0), state.CornerRadius);
        Assert.Equal(28, state.HeaderHeight);
        Assert.Equal(3, state.MarkWidth);
        Assert.Equal(16, state.MarkHeight);
        Assert.Equal(global::Avalonia.Layout.VerticalAlignment.Center, state.MarkVerticalAlignment);
        Assert.Equal(global::Avalonia.Layout.VerticalAlignment.Center, state.TextVerticalAlignment);
    }

    [Fact]
    public void Editor_right_tabs_have_no_extra_xy_separator()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var separators = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false) { RightTabIndex = 1 };
            var tabs = new EditorRightTabs { DataContext = vm };
            host.Show(tabs, 640, 700);
            tabs.UpdateLayout();
            return UiRuntimeTestHost.Descendants<XYSeparator>(tabs).Count();
        });

        Assert.Equal(0, separators);
    }

    static Color ColorOf(object? value) =>
        Assert.IsType<SolidColorBrush>(value).Color;
}
