using System.IO;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class AreaATopChromeContractTests
{
    [Fact]
    public void Row1_keeps_commands_icons_and_xyui_actions()
    {
        var source = Read("XuanYu.Editor.UI", "Top", "Top.axaml");
        foreach (var label in new[] { "文件", "添加", "新建", "打开", "保存", "另存为", "撤销", "重做", "运行", "停止" })
            Assert.Contains(label, source);
        foreach (var command in new[] { "新建", "打开", "保存", "另存为", "撤销", "重做", "运行", "停止" })
            Assert.Contains($"CommandParameter=\"{command}\"", source);
        Assert.True(Count(source, "<xy:XYButton") >= 8);
        Assert.Contains("Variant=\"Primary\"", source);
        Assert.Contains("Variant=\"Danger\"", source);
        Assert.Contains("<xy:XYBadge", source);
    }

    [Fact]
    public void Row2_keeps_dual_workspace_entries_and_tool_commands()
    {
        var top = Read("XuanYu.Editor.UI", "Top", "Top.axaml");
        var workspace = Read("XuanYu.Editor.UI", "Workspace", "WorkspaceSelector.axaml");
        foreach (var label in new[] { "选择", "框选", "移动", "旋转", "缩放", "吸附", "聚焦", "查看全部", "平移", "环绕" })
            Assert.Contains(label, top);
        Assert.True(Count(top, "<xy:XYToggleButton") >= 6);
        Assert.Contains("Content=\"管理模式\"", workspace);
        Assert.Contains("CurrentEditorModeText", workspace);
        Assert.Equal(2, Count(workspace, "ToggleEditorModeCommand"));
    }

    [Fact]
    public void AreaA_has_one_environment_entry_and_no_legacy_visual_classes()
    {
        var top = Read("XuanYu.Editor.UI", "Top", "Top.axaml");
        var states = Read("XuanYu.Editor.UI", "Top", "Top.States.axaml");
        var workspace = Read("XuanYu.Editor.UI", "Workspace", "WorkspaceSelector.axaml");
        Assert.Equal(1, Count(top, "Header=\"环境\""));
        foreach (var legacy in new[] { "cmdBtn", "toolBtn", "modeSurface" })
        {
            Assert.DoesNotContain(legacy, top);
            Assert.DoesNotContain(legacy, states);
            Assert.DoesNotContain(legacy, workspace);
        }
        Assert.DoesNotContain("<Button", top);
        Assert.DoesNotContain("<Button", workspace);
    }

    static string Read(params string[] path) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", Path.Combine(path)));

    static int Count(string text, string value) => text.Split(value).Length - 1;
}
