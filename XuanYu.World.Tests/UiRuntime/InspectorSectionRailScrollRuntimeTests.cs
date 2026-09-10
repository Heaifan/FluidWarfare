using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class InspectorSectionRailScrollRuntimeTests
{
    readonly UiHeadlessFixture _fixture;

    public InspectorSectionRailScrollRuntimeTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Theory]
    [InlineData(300)]
    [InlineData(360)]
    [InlineData(480)]
    public void Entity_inspector_scrolls_without_moving_tab_header(double width)
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var result = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            vm.AddCubeEntity();
            var tabs = new EditorRightTabs { DataContext = vm };
            host.Show(tabs, width, 260); tabs.UpdateLayout();
            var inspector = tabs.FindControl<InspectorPanel>("InspectorWorkspace")!;
            var scroll = UiRuntimeTestHost.Descendants<ScrollViewer>(inspector).Single(x =>
                x.VerticalScrollBarVisibility == ScrollBarVisibility.Auto &&
                x.HorizontalScrollBarVisibility == ScrollBarVisibility.Disabled);
            var header = tabs.FindControl<XYTabs>("SideTabs")!.SelectedItem!;
            var before = header.Bounds;
            scroll.Offset = new Vector(0, scroll.Extent.Height - scroll.Viewport.Height);
            tabs.UpdateLayout();
            return (scroll.Extent, scroll.Viewport, before, header.Bounds);
        });

        Assert.True(result.Extent.Height > result.Viewport.Height);
        Assert.True(result.Extent.Width <= result.Viewport.Width + 1);
        Assert.Equal(result.before.Y, result.Bounds.Y);
    }

    [Fact]
    public void Empty_inspector_keeps_auto_scroll_inactive_when_content_fits()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var sizes = host.Run(() =>
        {
            var tabs = new EditorRightTabs { DataContext = new UiVm(null, seedInitialScene: false) };
            host.Show(tabs, 300, 420); tabs.UpdateLayout();
            var inspector = tabs.FindControl<InspectorPanel>("InspectorWorkspace")!;
            var scroll = UiRuntimeTestHost.Descendants<ScrollViewer>(inspector).Single(x =>
                x.VerticalScrollBarVisibility == ScrollBarVisibility.Auto &&
                x.HorizontalScrollBarVisibility == ScrollBarVisibility.Disabled);
            return (scroll.Extent, scroll.Viewport);
        });

        Assert.True(sizes.Extent.Height <= sizes.Viewport.Height);
    }
}
