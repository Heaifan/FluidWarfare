using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using Avalonia.VisualTree;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

public sealed partial class AreaAR4MenuRuntimeTests
{
    [Fact]
    public void Real_workspace_popup_radio_render_keeps_canonical_dot_geometry()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            var selector = new WorkspaceSelector { DataContext = vm };
            host.Show(selector, 640, 80);
            selector.UpdateLayout();
            var bar = selector.GetVisualDescendants().OfType<XYMenuBar>().Single();
            var trigger = bar.Items.Single();
            bar.Open(trigger);
            Dispatcher.UIThread.RunJobs();
            var menu = bar.OpenMenu!;
            Assert.Single(menu.Styles);
            var map = menu.Items.OfType<XYMenuItem>().Single(x => x.Label == "地图编辑");
            var region = menu.Items.OfType<XYMenuItem>().Single(x => x.Label == "区域编辑");
            Assert.True(map.IsChecked);
            Assert.False(region.IsChecked);
            bar.Close();
            AssertRender(menu, map, true);
            AssertRender(menu, region, false);
            Assert.True(region.Activate());
            Assert.False(vm.IsMapWorkspace);
            Assert.True(vm.IsRegionWorkspace);
            bar.Open(trigger);
            Dispatcher.UIThread.RunJobs();
            bar.Close();
            AssertRender(menu, map, false);
            AssertRender(menu, region, true);
        });
    }

    static void AssertRender(XYMenu menu, XYMenuItem item, bool selected)
    {
        var popupHost = new Window { Width = 360, Height = 120, Content = menu };
        popupHost.Show();
        popupHost.UpdateLayout();
        menu.UpdateLayout();
        var radio = item.GetVisualDescendants().OfType<Grid>().Single(x => x.Classes.Contains("xyui-menu-radio"));
        var ring = item.GetVisualDescendants().OfType<Ellipse>().Single(x => x.Classes.Contains("xyui-menu-radio-ring"));
        var dot = item.GetVisualDescendants().OfType<Ellipse>().Single(x => x.Classes.Contains("xyui-menu-radio-dot"));
        Assert.True(radio.Bounds.Width > 0 && radio.Bounds.Height > 0);
        Assert.Equal(16, radio.Bounds.Width);
        Assert.Equal(16, radio.Bounds.Height);
        Assert.True(ring.IsVisible && ring.Bounds.Width > 0 && ring.Bounds.Height > 0);
        Assert.NotNull(ring.Stroke);
        Assert.True(ring.StrokeThickness > 0);
        Assert.Equal(selected, dot.IsVisible);
        Assert.NotNull(dot.Fill);
        if (selected)
        {
            Assert.Equal(6, dot.Bounds.Width);
            Assert.Equal(6, dot.Bounds.Height);
            Assert.Equal(5, dot.Bounds.X);
            Assert.Equal(5, dot.Bounds.Y);
        }
        popupHost.Content = null;
        popupHost.Close();
    }
}
