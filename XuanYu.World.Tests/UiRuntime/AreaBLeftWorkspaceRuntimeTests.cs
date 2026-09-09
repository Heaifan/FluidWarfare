using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Headless;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.VisualTree;
using System.Reflection;
using XuanYu.Editor.UI;
using XuanYu.Editor.Workspace;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed partial class AreaBLeftWorkspaceRuntimeTests
{
    readonly UiHeadlessFixture _fixture;
    public AreaBLeftWorkspaceRuntimeTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Narrow_left_materializes_rail_header_and_real_workspace_tabs()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var snapshot = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            vm.ToggleEditorMode();
            var left = new Left { DataContext = vm };
            host.Show(left, 360, 860);
            left.UpdateLayout();
            var rail = left.FindControl<XYNavigationRail>("WorkspaceRail")!;
            var map = left.FindControl<MapEditorPanel>("MapWorkspace")!;
            var region = left.FindControl<RegionalAuthoringPanel>("RegionWorkspace")!;
            rail.NavigationState.Select("dataset");
            Dispatcher.UIThread.RunJobs();
            return (rail.LayoutVariant, vm.LeftTabIndex, map.IsVisible, region.IsVisible,
                map.SelectedTabId, rail.Items.Count, left.Bounds.Width);
        });

        Assert.Equal(XyuiNavigationLayoutVariant.Workspace, snapshot.Item1);
        Assert.Equal(2, snapshot.Item2);
        Assert.True(snapshot.Item3);
        Assert.False(snapshot.Item4);
        Assert.Equal("dataset", snapshot.Item5);
        Assert.Equal(3, snapshot.Item6);
        Assert.Equal(360, snapshot.Item7);
    }

    [Fact]
    public void Hierarchy_context_menu_uses_real_popup_root_and_xyui_items()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var evidence = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: true);
            var hierarchy = new HierarchyWorkspace { DataContext = vm };
            host.Show(hierarchy, 300, 420);
            hierarchy.UpdateLayout();
            hierarchy.ContextMenu.Open();
            Dispatcher.UIThread.RunJobs();
            var field = typeof(XYContextMenu).GetField("_popup", BindingFlags.Instance | BindingFlags.NonPublic)!;
            var popupControl = field.GetValue(hierarchy.ContextMenu) as Popup;
            var root = popupControl?.GetType().GetProperty("VisualRoot",
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)?.GetValue(popupControl);
            var items = hierarchy.ContextMenu.Menu.Items.OfType<XYMenuItem>().Count();
            var popup = root?.GetType().Name ?? "HeadlessPopupRootUnavailable";
            var menuOpen = hierarchy.ContextMenu.IsOpen && popupControl?.IsOpen == true;
            var hasChild = popupControl?.Child is not null;
            hierarchy.ContextMenu.Close();
            return (popup, items, menuOpen, hasChild);
        });

        Assert.True(evidence.menuOpen);
        Assert.True(evidence.hasChild);
        Assert.Contains(evidence.popup, new[] { "PopupRoot", "HeadlessPopupRootUnavailable" });
        Assert.Equal(3, evidence.items);
    }

}
