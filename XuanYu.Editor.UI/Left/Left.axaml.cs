using System.ComponentModel;
using Avalonia.Controls;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XuanYu.Editor.UI;

public partial class Left : UserControl
{
    XYNavigationState? _workspaceState;
    UiVm? _viewModel;
    bool _syncingRail;

    public Left()
    {
        InitializeComponent();
        DataContextChanged += OnDataContextChanged;
        MapWorkspace.TabChanged += WorkspacePanel_TabChanged;
        RegionWorkspace.TabChanged += WorkspacePanel_TabChanged;
    }

    void OnDataContextChanged(object? sender, EventArgs e)
    {
        if (_viewModel is not null) _viewModel.PropertyChanged -= ViewModel_PropertyChanged;
        _viewModel = DataContext as UiVm;
        if (_viewModel is not null) _viewModel.PropertyChanged += ViewModel_PropertyChanged;
        RebuildWorkspaceState();
        RefreshWorkspaceVisuals();
    }

    void ViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName is nameof(UiVm.LeftTabIndex)
            or nameof(UiVm.IsMapEditMode)
            or nameof(UiVm.IsRegionEditMode)
            or nameof(UiVm.CurrentWorkspaceDisplayName))
        {
            RebuildWorkspaceState();
            RefreshWorkspaceVisuals();
        }
    }

    void RebuildWorkspaceState()
    {
        if (_viewModel is null) return;
        if (_workspaceState is not null) _workspaceState.Changed -= WorkspaceState_Changed;
        IReadOnlyList<XYNavigationEntry> entries = _viewModel.IsManageMode
            ? [new XYNavigationEntry("project", "项目", XyuiVectorIcon.Browse),
               new XYNavigationEntry("hierarchy", "层级", XyuiVectorIcon.Locate)]
            : _viewModel.IsMapEditMode
                ? [new XYNavigationEntry("map-base", "地图基础", XyuiVectorIcon.Eye),
                   new XYNavigationEntry("map-environment", "地图环境", XyuiVectorIcon.Eye),
                   new XYNavigationEntry("dataset", "数据集", XyuiVectorIcon.Browse)]
                : [new XYNavigationEntry("region", "区域面", XyuiVectorIcon.Section),
                   new XYNavigationEntry("road", "道路", XyuiVectorIcon.Locate),
                   new XYNavigationEntry("marker", "地图标记", XyuiVectorIcon.Tag)];
        WorkspaceRail.NavigationState = new XYNavigationState([]);
        WorkspaceRail.Items.Clear();
        _workspaceState = new XYNavigationState(entries, SelectedContextId());
        _workspaceState.Changed += WorkspaceState_Changed;
        WorkspaceRail.NavigationState = _workspaceState;
        foreach (var item in WorkspaceRail.Items)
        {
            item.Width = 46;
            item.Height = 50;
        }
    }

    string? SelectedContextId() => _viewModel!.IsManageMode
        ? _viewModel.LeftTabIndex == 1 ? "hierarchy" : "project"
        : _viewModel.IsMapEditMode ? MapWorkspace.SelectedTabId switch
        {
            "environment" => "map-environment", "dataset" => "dataset", _ => "map-base"
        } : RegionWorkspace.SelectedTabId;

    void WorkspaceState_Changed(object? sender, EventArgs e)
    {
        if (_syncingRail || _viewModel is null || _workspaceState is null) return;
        if (_viewModel.IsManageMode)
            _viewModel.LeftTabIndex = _workspaceState.SelectedId == "hierarchy" ? 1 : 0;
        else if (_viewModel.IsMapEditMode)
            MapWorkspace.SelectTab(_workspaceState.SelectedId ?? "base");
        else
            RegionWorkspace.SelectTab(_workspaceState.SelectedId ?? "region");
        RefreshWorkspaceVisuals();
    }

    void WorkspacePanel_TabChanged(string _)
    {
        if (_syncingRail) return;
        RebuildWorkspaceState();
        RefreshWorkspaceVisuals();
    }

}
