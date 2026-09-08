using System.IO;

namespace XuanYu.World.Tests.UiTokens;

public sealed class UiR1FinalLeftTopContractTests
{
    static string Read(string rel) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", "XuanYu.Editor.UI", rel));

    [Fact]
    public void Left_tree_titles_use_canonical_truncation()
    {
        var left = Read("Left/Left.axaml");
        Assert.Contains("<xy:XYTruncatedText", left);
        Assert.Contains("Text=\"{Binding Title}\"", left);
        Assert.DoesNotContain("Classes=\"treeText\"", left);
    }

    [Fact]
    public void Regional_authoring_heading_uses_section_title()
    {
        var panel = Read("Left/RegionalAuthoringPanel.axaml");
        Assert.Contains("<xy:XYSectionTitle Text=\"内容类型\"", panel);
        Assert.Contains("SelectRegionAuthoringModeCommand", panel);
    }

    [Fact]
    public void Region_and_road_display_use_xyui1_semantics()
    {
        foreach (var rel in new[] { "Left/RegionPanel.axaml", "Left/RoadPanel.axaml" })
        {
            var panel = Read(rel);
            Assert.Contains("<xy:XYSectionTitle", panel);
            Assert.Contains("<xy:XYSeparator Variant=\"Section\"", panel);
            Assert.Contains("<xy:XYLabel", panel);
            Assert.Contains("<xy:XYText", panel);
            Assert.Contains("<xy:XYSelectableText", panel);
            Assert.Contains("Variant=\"Technical\"", panel);
            Assert.Contains("<xy:XYCaption", panel);
            Assert.DoesNotContain("Classes=\"uiSection\"", panel);
            Assert.DoesNotContain("Classes=\"uiValue\"", panel);
        }
    }

    [Fact]
    public void Marker_display_uses_xyui1_semantics_and_keeps_button()
    {
        var panel = Read("Left/MarkerPanel.axaml");
        Assert.Contains("<xy:XYSectionTitle", panel);
        Assert.Contains("<xy:XYText", panel);
        Assert.Contains("<xy:XYSelectableText", panel);
        Assert.Contains("<xy:XYCaption", panel);
        Assert.Contains("Click=\"MarkerPlacement_Click\"", panel);
        Assert.DoesNotContain("Classes=\"uiValue\"", panel);
    }

    [Fact]
    public void Top_uses_badges_and_canonical_vertical_separators()
    {
        var top = Read("Top/Top.axaml"); var runtime = Read("Top/RuntimeStatusModule.axaml");
        var file = Read("Top/FileModule.axaml"); var tools = Read("Top/EditToolsModule.axaml");
        var view = Read("Top/ViewModule.axaml"); var snap = Read("Top/SnapModule.axaml");
        var workspace = Read("Workspace/WorkspaceSelector.axaml");
        Assert.DoesNotContain("玄域引擎", top); Assert.DoesNotContain("场景编辑器", top);
        Assert.DoesNotContain("XYSeparator", top);
        Assert.Contains("<local:WorkspaceSelector Grid.Column=\"0\"", top);
        Assert.Contains("<local:FileModule Grid.Column=\"1\"", top);
        Assert.Contains("<local:RuntimeStatusModule Grid.Column=\"3\"", top);
        Assert.Contains("<local:EditToolsModule/>", top);
        Assert.Contains("<local:ViewModule/>", top);
        Assert.Contains("<local:SnapModule/>", top);
        Assert.DoesNotContain("IsVisible", top);
        Assert.Contains("IsEnabled=\"{Binding IsEditMode}\"", tools);
        Assert.Contains("IsEnabled=\"False\"", tools);
        Assert.Contains("ToolTip.Tip=\"框选功能尚未实装（即将推出）\"", tools);
        Assert.DoesNotContain("CommandParameter=\"框选\"", tools);
        Assert.Contains("IsEnabled=\"{Binding IsMapEditMode}\"", snap);
        Assert.Contains("Data=\"{StaticResource FocusIcon}\"", view);
        Assert.Contains("Data=\"{StaticResource ViewAllIcon}\"", view);
        Assert.Contains("<xy:XYBadge", runtime); Assert.Contains("<Menu", file);
        Assert.Contains("Text=\"工作区\"", workspace); Assert.Contains("Text=\"文件\"", file);
        Assert.Contains("Text=\"编辑工具\"", tools); Assert.Contains("Text=\"视图\"", view);
        Assert.Contains("Text=\"吸附\"", snap); Assert.Contains("Text=\"运行\"", runtime);
        Assert.Contains("Text=\"状态\"", runtime);
        Assert.DoesNotContain("WORKSPACE", top); Assert.DoesNotContain("EDIT TOOLS", tools);
        Assert.DoesNotContain("statePill", top);
    }
}
