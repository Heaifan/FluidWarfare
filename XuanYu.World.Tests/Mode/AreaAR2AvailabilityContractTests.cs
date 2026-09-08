using XuanYu.Editor.UI;
using XuanYu.Editor.Workspace;

namespace XuanYu.World.Tests.Mode;

public sealed class AreaAR2AvailabilityContractTests
{
    [Fact]
    public void Manage_mode_blocks_edit_tool_commands()
    {
        var vm = new UiVm(null, () => true);

        vm.SelectToolCommand.Execute("移动");

        Assert.False(vm.CanUseEditTools);
        Assert.True(vm.IsSelectTool);
        vm.ToggleEditorModeCommand.Execute(null);
        vm.SelectToolCommand.Execute("移动");
        Assert.True(vm.IsMoveTool);
        vm.SwitchWorkspaceCommand.Execute(EditorWorkspaceId.RegionEditor);
        vm.SelectToolCommand.Execute("旋转");
        Assert.True(vm.IsSelectTool);
    }

    [Fact]
    public void Snap_only_mutates_in_map_edit_mode()
    {
        var vm = new UiVm(null, () => true);
        var initial = vm.IsSnapEnabled;

        vm.ToggleSnapCommand.Execute(null);
        Assert.False(vm.CanToggleSnap);
        Assert.Equal(initial, vm.IsSnapEnabled);
        vm.SwitchWorkspaceCommand.Execute(EditorWorkspaceId.RegionEditor);
        vm.ToggleEditorModeCommand.Execute(null);
        vm.ToggleSnapCommand.Execute(null);
        Assert.False(vm.CanToggleSnap);
        Assert.Equal(initial, vm.IsSnapEnabled);
        vm.SwitchWorkspaceCommand.Execute(EditorWorkspaceId.MapEditor);
        vm.ToggleSnapCommand.Execute(null);
        Assert.True(vm.CanToggleSnap);
        Assert.NotEqual(initial, vm.IsSnapEnabled);
    }

    [Fact]
    public void Box_select_command_keeps_the_active_tool()
    {
        var vm = new UiVm(null, () => true); vm.ToggleEditorMode();
        vm.SelectToolCommand.Execute("移动");
        vm.SelectToolCommand.Execute("框选");

        Assert.True(vm.IsMoveTool);
        Assert.False(vm.IsBoxSelectTool);
    }
}
