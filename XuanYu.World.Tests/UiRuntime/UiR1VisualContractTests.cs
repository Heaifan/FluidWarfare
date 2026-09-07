using Avalonia.Controls;
using Avalonia.Media;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class UiR1VisualContractTests
{
    readonly UiHeadlessFixture _fixture;
    public UiR1VisualContractTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Editor_right_debug_titles_use_the_canonical_section_visual()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var state = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false) { RightTabIndex = 1 };
            var tabs = new EditorRightTabs { DataContext = vm };
            host.Show(tabs, 640, 700); tabs.UpdateLayout();
            var titles = UiRuntimeTestHost.Descendants<XYSectionTitle>(tabs).ToArray();
            var first = titles.First();
            var header = Assert.IsType<Grid>(first.Child);
            var mark = Assert.IsType<Border>(header.Children[0]);
            var text = Assert.IsType<TextBlock>(header.Children[1]);
            return (Count: titles.Length, Separators: UiRuntimeTestHost.Descendants<XYSeparator>(tabs).Count(),
                Background: ColorOf(first.Background), Mark: ColorOf(mark.Background), MarkWidth: mark.Width,
                MarkHeight: mark.Height, FontSize: text.FontSize, FontWeight: text.FontWeight,
                Text: text.Text, MarkClass: mark.Classes.Contains("xyui-section-title-left-mark"));
        });

        Assert.Equal(4, state.Count); Assert.Equal(0, state.Separators);
        Assert.Equal(Color.Parse("#EEF3F6"), state.Background);
        Assert.Equal(Color.Parse("#526873"), state.Mark);
        Assert.Equal(3, state.MarkWidth); Assert.Equal(16, state.MarkHeight);
        Assert.Equal(14, state.FontSize); Assert.Equal(FontWeight.SemiBold, state.FontWeight);
        Assert.False(string.IsNullOrWhiteSpace(state.Text)); Assert.True(state.MarkClass);
    }

    [Fact]
    public void Engine_app_keeps_representative_xyui1_visual_contracts()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var state = host.Run(() =>
        {
            var panel = new StackPanel
            {
                Children =
                {
                    new XYHeading { Text = "检查器" }, new XYLabel { Text = "字段" },
                    new XYBadge { Text = "Accent", Variant = XyuiBadgeVariant.Accent },
                    new XYStatusBadge { Text = "完成", State = XyuiStatusState.Success },
                    new XYErrorText { Text = "错误" },
                },
            };
            host.Show(panel, 420, 180); panel.UpdateLayout();
            return (Heading: panel.Children.OfType<XYHeading>().Single().FontSize > 0,
                Label: panel.Children.OfType<XYLabel>().Single().FontSize > 0,
                Badge: panel.Children.OfType<XYBadge>().Single().Height == XYBadge.BadgeHeight,
                Status: panel.Children.OfType<XYStatusBadge>().Single().Classes.Contains("xyui-status-success"),
                Error: panel.Children.OfType<XYErrorText>().Any());
        });

        Assert.True(state.Heading); Assert.True(state.Label); Assert.True(state.Badge);
        Assert.True(state.Status); Assert.True(state.Error);
    }

    static Color ColorOf(IBrush? brush) => Assert.IsType<SolidColorBrush>(brush).Color;
}
