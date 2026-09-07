using System.IO;

namespace XuanYu.World.Tests.UiRuntime;

public sealed class UiR2BLeftMapMigrationContractTests
{
    static string Read(string rel) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", "XuanYu.Editor.UI", rel));

    [Fact]
    public void Left_authoring_uses_xyui2_actions_and_search()
    {
        var left = Read("Left/Left.axaml");
        Assert.Contains("<xy:XYSearchField", left);
        Assert.Contains("<xy:XYIcon Icon=\"ChevronDown\"", left);
        Assert.Contains("<xy:XYIcon Icon=\"ChevronRight\"", left);
        Assert.DoesNotContain("<TextBox", left.Replace("<TextBox Grid.Column=\"3\"", ""));
        foreach (var rel in new[] { "Left/RegionPanel.axaml", "Left/RoadPanel.axaml", "Left/MarkerPanel.axaml" })
            Assert.DoesNotContain("<Button", Read(rel));
        Assert.DoesNotContain("<ToggleButton", Read("Left/RegionalAuthoringPanel.axaml"));
    }

    [Fact]
    public void Map_forms_and_dataset_actions_use_xyui2_controls()
    {
        foreach (var rel in new[] { "Right/MapFormPanel.axaml", "Right/MapPagePanel.axaml", "Right/DatasetPanel.axaml" })
        {
            var view = Read(rel);
            Assert.DoesNotContain("<Button", view);
            Assert.DoesNotContain("<TextBox", view);
        }

        var form = Read("Right/MapFormPanel.axaml");
        Assert.Contains("<xy:XYTextField", form);
        var dataset = Read("Right/DatasetPanel.axaml");
        Assert.Contains("<xy:XYSelect", dataset);
        Assert.Contains("<xy:XYButton", dataset);
        Assert.DoesNotContain("<ComboBox ", dataset);
    }

    [Fact]
    public void Dataset_layer_actions_use_xyui2_toggles()
    {
        var panel = Read("Right/DatasetLayerPanel.axaml");
        Assert.Contains("<xy:XYToggleButton", panel);
        Assert.DoesNotContain("<ToggleButton", panel);
    }
}
