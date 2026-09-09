using XYUI.Avalonia.Vector;

namespace XuanYu.Editor.UI;

public partial class Left
{
    void RefreshWorkspaceVisuals()
    {
        if (_viewModel is null) return;
        var index = _viewModel.LeftTabIndex;
        var manage = _viewModel.IsManageMode;
        _syncingRail = true;
        try { _workspaceState?.Select(SelectedContextId()); }
        finally { _syncingRail = false; }
        ProjectWorkspace.IsVisible = manage && index == 0;
        HierarchyWorkspace.IsVisible = manage && index == 1;
        MapWorkspace.IsVisible = _viewModel.IsMapEditMode;
        RegionWorkspace.IsVisible = _viewModel.IsRegionEditMode;
        var id = _workspaceState?.SelectedId ?? "project";
        (WorkspaceTitle.Text, WorkspaceSubtitle.Text, WorkspaceHeaderIcon.Icon) = id switch
        {
            "hierarchy" => ("层级", "场景对象层级", XyuiVectorIcon.Locate),
            "map-environment" => ("地图环境", "地图环境参数", XyuiVectorIcon.Eye),
            "dataset" => ("数据集", "地图数据集管理", XyuiVectorIcon.Browse),
            "map-base" => ("地图基础", "地图基础信息", XyuiVectorIcon.Eye),
            "road" => ("道路", "道路绘制与属性", XyuiVectorIcon.Locate),
            "marker" => ("地图标记", "地图标记管理", XyuiVectorIcon.Tag),
            "region" => ("区域面", "区域绘制与属性", XyuiVectorIcon.Section),
            _ => ("项目", "当前场景资源", XyuiVectorIcon.Browse)
        };
    }
}
