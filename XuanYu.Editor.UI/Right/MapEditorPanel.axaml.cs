using Avalonia.Controls;
using XYUI.Avalonia.Controls;

namespace XuanYu.Editor.UI;

public partial class MapEditorPanel : UserControl
{
    public event Action<string>? TabChanged;
    public string SelectedTabId => MapTabs.SelectedTabId ?? "base";

    public MapEditorPanel()
    {
        InitializeComponent();
        SelectTab(MapTabs.SelectedTabId ?? "base");
    }

    void MapTabs_SelectionChanged(object? sender, XYTab tab) => SelectTab(tab.Id);

    public void SelectTab(string id)
    {
        id = id is "environment" or "dataset" ? id : "base";
        var changed = SelectedTabId != id;
        MapTabs.SelectedTabId = id;
        MapPageHost.IsVisible = id == "base";
        EnvironmentHost.IsVisible = id == "environment";
        DatasetHost.IsVisible = id == "dataset";
        if (changed) TabChanged?.Invoke(id);
    }
}
