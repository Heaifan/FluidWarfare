using System.IO;
using Avalonia.Controls;
using XuanYu.Editor.UI;
using XYUI.Avalonia.Controls;

namespace XuanYu.World.Tests.UiRuntime;

[Collection("UiRuntime")]
public sealed class XYUI2R2BContractTests
{
    readonly UiHeadlessFixture _fixture;

    public XYUI2R2BContractTests(UiHeadlessFixture fixture) => _fixture = fixture;

    [Fact]
    public void Left_workspace_omits_uncontracted_search_and_uses_workspace_rail()
    {
        var source = Read("XuanYu.Editor.UI", "Left", "Left.axaml");
        Assert.Contains("<xy:XYNavigationRail", source);
        Assert.Contains("LayoutVariant=\"Workspace\"", source);
        Assert.DoesNotContain("XYSearchField", source);
        Assert.DoesNotContain("搜索项目树", source);
    }

    [Fact]
    public void Map_inputs_are_xyui_text_fields_and_keep_validation_bindings()
    {
        var source = Read("XuanYu.Editor.UI", "Right", "MapFormPanel.axaml");
        Assert.Equal(3, Count(source, "<xy:XYTextField"));
        Assert.DoesNotContain("<TextBox", source);
        Assert.Contains("MapWidthText", source);
        Assert.Contains("MapDepthText", source);
        Assert.Contains("MapBaseHeightText", source);
        Assert.Contains("Field_LostFocus", source);
    }

    [Fact]
    public void Right_actions_and_choices_use_xyui2_runtime_types()
    {
        var tabs = Read("XuanYu.Editor.UI", "Right", "EditorRightTabs.axaml");
        var page = Read("XuanYu.Editor.UI", "Right", "MapPagePanel.axaml");
        var dataset = Read("XuanYu.Editor.UI", "Right", "DatasetPanel.axaml");
        Assert.Equal(4, Count(tabs, "<xy:XYButton"));
        Assert.Contains("<xy:XYButton", page);
        Assert.Contains("<xy:XYButton", dataset);
        Assert.Contains("<xy:XYSelect", dataset);
        Assert.Contains("InteractionCommand", tabs);
        Assert.Contains("CommandParameter=\"应用地图属性\"", Read("XuanYu.Editor.UI", "Right", "MapFormPanel.axaml"));
    }

    [Fact]
    public void Right_map_actions_materialize_as_xyui2_controls()
    {
        using var host = new UiRuntimeTestHost(_fixture);
        var counts = host.Run(() =>
        {
            var vm = new UiVm(null, seedInitialScene: false) { RightTabIndex = 1 };
            var page = new MapPagePanel { DataContext = vm };
            var tabs = new EditorRightTabs { DataContext = vm };
            var root = new StackPanel { Children = { page, tabs } };
            host.Show(root, 720, 900); root.UpdateLayout();
            return (MapButtons: UiRuntimeTestHost.Descendants<XYButton>(page).Count(),
                MapIconButtons: UiRuntimeTestHost.Descendants<XYIconButton>(page).Count(),
                MapFields: UiRuntimeTestHost.Descendants<XYTextField>(page).Count(),
                DebugButtons: UiRuntimeTestHost.Descendants<XYButton>(tabs).Count());
        });

        Assert.Equal(7, counts.MapButtons);
        Assert.Equal(1, counts.MapIconButtons);
        Assert.Equal(3, counts.MapFields);
        Assert.Equal(4, counts.DebugButtons);
    }

    [Fact]
    public void R1_and_r2a_right_contracts_remain_present()
    {
        var inspector = Read("XuanYu.Editor.UI", "Right", "InspectorPanel.axaml");
        var form = Read("XuanYu.Editor.UI", "Right", "MapFormPanel.axaml");
        var right = Read("XuanYu.Editor.UI", "Right", "Right.axaml");
        Assert.Contains("<xy:XYSectionTitle", inspector);
        Assert.Contains("<xy:XYSeparator Variant=\"Section\"", inspector);
        Assert.Contains("ColumnDefinitions=\"80,*\"", inspector);
        Assert.Contains("PropsWide", form); Assert.DoesNotContain("PropsNarrow", form);
        Assert.Contains("EditorLayerDock", right);
    }

    static string Read(params string[] path) => File.ReadAllText(Path.Combine(
        AppContext.BaseDirectory, "..", "..", "..", "..", Path.Combine(path)));

    static int Count(string text, string value) => text.Split(value).Length - 1;
}
