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
        var top = Read("Top/Top.axaml");
        Assert.Contains("<xy:XYBadge", top);
        Assert.Contains("<xy:XYSeparator Variant=\"VerticalSplit\"", top);
        Assert.DoesNotContain("statePill", top);
        Assert.Contains("<Menu>", top);
        Assert.Contains("Classes=\"cmdBtn\"", top);
        Assert.Contains("Classes=\"toolBtn\"", top);
    }
}
