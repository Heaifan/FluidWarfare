using Avalonia.Controls;
using XYUI.Avalonia.Controls;

namespace XuanYu.Editor.UI;

public partial class MapEditorPanel : UserControl
{
    public MapEditorPanel()
    {
        InitializeComponent();
        SelectTab(MapTabs.SelectedTabId ?? "base");
    }

    void MapTabs_SelectionChanged(object? sender, XYTab tab) => SelectTab(tab.Id);

    void SelectTab(string id)
    {
        MapPageHost.IsVisible = id == "base";
        EnvironmentHost.IsVisible = id == "environment";
        DatasetHost.IsVisible = id == "dataset";
    }
}
