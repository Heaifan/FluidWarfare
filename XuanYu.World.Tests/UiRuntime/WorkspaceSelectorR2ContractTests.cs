using System.IO;
using Avalonia.Controls;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class WorkspaceSelectorR2ContractTests
{
    readonly UiHeadlessFixture _fixture;

    public WorkspaceSelectorR2ContractTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Selector_declares_both_xyui_mode_entries_and_state_visibility()
    {
        var source = Read("XuanYu.Editor.UI", "Workspace", "WorkspaceSelector.axaml");
        Assert.Equal(2, Count(source, "<xy:XYButton"));
        Assert.Contains("Content=\"管理模式\"", source);
        Assert.Contains("Content=\"{Binding CurrentEditorModeText}\"", source);
        Assert.Equal(2, Count(source, "Command=\"{Binding ToggleEditorModeCommand}\""));
        Assert.Contains("<StackPanel Orientation=\"Horizontal\" Spacing=\"4\">", source);
    }

    [Fact]
    public void Manage_and_edit_states_expose_a_visible_xyui_mode_control()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var states = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false);
            var selector = new WorkspaceSelector { DataContext = vm };
            host.Show(selector, 420, 48);
            selector.UpdateLayout();
            var manage = VisibleButtons(selector).Select(ContentOf).ToArray();
            vm.ToggleEditorMode();
            selector.UpdateLayout();
            var edit = VisibleButtons(selector).Select(ContentOf).ToArray();
            return (manage, edit);
        });

        Assert.Contains("管理模式", states.manage);
        Assert.Contains("管理模式", states.edit);
        Assert.Contains("地图编辑", states.edit);
    }

    [Fact]
    public void Top_declares_one_environment_menu()
    {
        var source = Read("XuanYu.Editor.UI", "Top", "Top.axaml");
        Assert.Equal(1, Count(source, "Header=\"环境\""));
    }

    static IEnumerable<XYButton> VisibleButtons(Control root) =>
        UiRuntimeTestHost.Descendants<XYButton>(root).Where(button => button.IsVisible);

    static string ContentOf(XYButton button) => button.Content?.ToString() ?? string.Empty;

    static string Read(params string[] path) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", Path.Combine(path)));

    static int Count(string text, string value) => text.Split(value).Length - 1;
}
