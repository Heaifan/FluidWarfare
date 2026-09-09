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
        var selected = WorkspaceId(_viewModel.LeftTabIndex);
        _workspaceState = new XYNavigationState(
            [new("project", "项目", XyuiVectorIcon.Browse),
             new("hierarchy", "层级", XyuiVectorIcon.Locate),
             new("map", "地图", XyuiVectorIcon.Eye, IsEnabled: _viewModel.IsMapEditMode),
             new("region", "区域", XyuiVectorIcon.Section, IsEnabled: _viewModel.IsRegionEditMode)], selected);
        _workspaceState.Changed += WorkspaceState_Changed;
        WorkspaceRail.NavigationState = _workspaceState;
    }

    void WorkspaceState_Changed(object? sender, EventArgs e)
    {
        if (_syncingRail || _viewModel is null || _workspaceState is null) return;
        _viewModel.LeftTabIndex = IndexOf(_workspaceState.SelectedId);
        RefreshWorkspaceVisuals();
    }

    void RefreshWorkspaceVisuals()
    {
        if (_viewModel is null) return;
        var index = _viewModel.LeftTabIndex;
        _syncingRail = true;
        try { _workspaceState?.Select(WorkspaceId(index)); }
        finally { _syncingRail = false; }
        ProjectWorkspace.IsVisible = index == 0;
        HierarchyWorkspace.IsVisible = index == 1;
        MapWorkspace.IsVisible = index == 2 && _viewModel.IsMapEditMode;
        RegionWorkspace.IsVisible = index == 3 && _viewModel.IsRegionEditMode;
        WorkspaceTitle.Text = index switch { 0 => "项目", 1 => "层级", 2 => "地图", 3 => "区域", _ => "工作区" };
        WorkspaceSubtitle.Text = index switch { 0 => "项目资源与场景资产", 1 => "场景对象层级", 2 => "地图编辑工作区", 3 => "区域作者工作区", _ => "编辑工作区" };
        WorkspaceHeaderIcon.Icon = index switch { 1 => XyuiVectorIcon.Locate, 2 => XyuiVectorIcon.Eye, 3 => XyuiVectorIcon.Section, _ => XyuiVectorIcon.Browse };
    }

    static string WorkspaceId(int index) => index switch { 1 => "hierarchy", 2 => "map", 3 => "region", _ => "project" };
    static int IndexOf(string? id) => id switch { "hierarchy" => 1, "map" => 2, "region" => 3, _ => 0 };
}
