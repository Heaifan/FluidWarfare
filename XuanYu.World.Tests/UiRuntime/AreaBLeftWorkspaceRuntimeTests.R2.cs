using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input;
using Avalonia.Threading;
using Avalonia.VisualTree;
using XuanYu.Editor.UI;
using XuanYu.Editor.Workspace;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

public sealed partial class AreaBLeftWorkspaceRuntimeTests
{
    [Fact]
    public void Rail_contexts_follow_top_mode_and_pointer_click_selects_real_tabs()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var evidence = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            var left = new Left { DataContext = vm };
            var window = host.Show(left, 220, 860);
            left.UpdateLayout();
            var rail = left.FindControl<XYNavigationRail>("WorkspaceRail")!;
            var icon = rail.Items.First().GetVisualDescendants().OfType<XYIcon>().Single();
            var management = (rail.Items.Count, rail.Items.All(item => item.IsEnabled), rail.Bounds.Width,
                rail.Items.First().Bounds.Width, rail.Items.First().Bounds.Height, icon.Bounds.Width, icon.Bounds.Height);

            vm.ToggleEditorMode(); Dispatcher.UIThread.RunJobs();
            var mapRail = left.FindControl<XYNavigationRail>("WorkspaceRail")!;
            var dataset = mapRail.Items.Single(item => item.Id == "dataset");
            var point = dataset.TranslatePoint(new Point(dataset.Bounds.Width / 2, dataset.Bounds.Height / 2), window)!.Value;
            window.MouseDown(point, MouseButton.Left); Dispatcher.UIThread.RunJobs();
            var map = left.FindControl<MapEditorPanel>("MapWorkspace")!;
            var mapSelected = (mapRail.Items.Count, map.SelectedTabId, mapRail.Items.All(item => item.IsEnabled));

            vm.SwitchWorkspaceCommand.Execute("RegionEditor"); Dispatcher.UIThread.RunJobs(); left.UpdateLayout();
            var regionRail = left.FindControl<XYNavigationRail>("WorkspaceRail")!;
            Press(regionRail.Items.Single(item => item.Id == "road")); Dispatcher.UIThread.RunJobs();
            var region = left.FindControl<RegionalAuthoringPanel>("RegionWorkspace")!;
            return (management, mapSelected, regionRail.Items.Count, region.SelectedTabId, vm.CurrentRegionAuthoringMode);
        });

        Assert.Equal(2, evidence.management.Item1);
        Assert.True(evidence.management.Item2);
        Assert.Equal(52, evidence.management.Item3);
        Assert.Equal(46, evidence.management.Item4);
        Assert.Equal(50, evidence.management.Item5);
        Assert.Equal(14, evidence.management.Item6);
        Assert.Equal(14, evidence.management.Item7);
        Assert.Equal(3, evidence.mapSelected.Item1);
        Assert.Equal("dataset", evidence.mapSelected.Item2);
        Assert.True(evidence.mapSelected.Item3);
        Assert.Equal(3, evidence.Item3);
        Assert.Equal("road", evidence.Item4);
        Assert.Equal(RegionAuthoringMode.Road, evidence.Item5);
    }

    static void Press(Control target)
    {
        var pointer = new Avalonia.Input.Pointer(1, PointerType.Mouse, true);
        var properties = new PointerPointProperties(RawInputModifiers.LeftMouseButton, PointerUpdateKind.LeftButtonPressed);
        target.RaiseEvent(new PointerPressedEventArgs(target, pointer, target,
            new Point(target.Bounds.Width / 2, target.Bounds.Height / 2), 1, properties, KeyModifiers.None, 1));
    }
}
