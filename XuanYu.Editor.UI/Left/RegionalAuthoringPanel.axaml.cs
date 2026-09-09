using System.ComponentModel;
using Avalonia.Controls;
using XuanYu.Editor.Workspace;
using XYUI.Avalonia.Controls;

namespace XuanYu.Editor.UI;

public partial class RegionalAuthoringPanel : UserControl
{
    public event Action<string>? TabChanged;
    UiVm? _viewModel;
    bool _syncing;

    public RegionalAuthoringPanel()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
    }

    void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (_viewModel is not null) _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        _viewModel = DataContext as UiVm;
        if (_viewModel is not null) _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        SyncTab();
    }

    void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(UiVm.CurrentRegionAuthoringMode)
            or nameof(UiVm.IsRegionSurfaceAuthoringMode)
            or nameof(UiVm.IsRoadAuthoringMode)
            or nameof(UiVm.IsMarkerAuthoringMode)) SyncTab();
    }

    void AuthoringTabs_SelectionChanged(object? sender, XYTab tab)
    {
        if (_syncing || _viewModel is null) return;
        SelectTab(tab.Id);
    }

    public string SelectedTabId => AuthoringTabs.SelectedTabId ?? "region";

    public void SelectTab(string id)
    {
        if (_viewModel is null) return;
        _viewModel.SelectRegionAuthoringMode(id switch { "road" => "道路", "marker" => "地图标记", _ => "区域面" });
        SyncTab();
    }

    void SyncTab()
    {
        if (_viewModel is null) return;
        _syncing = true;
        try
        {
            var id = _viewModel.CurrentRegionAuthoringMode switch { RegionAuthoringMode.Road => "road", RegionAuthoringMode.Marker => "marker", _ => "region" };
            var changed = SelectedTabId != id;
            AuthoringTabs.SelectedTabId = id;
            if (changed) TabChanged?.Invoke(id);
        }
        finally { _syncing = false; }
    }
}
