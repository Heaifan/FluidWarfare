using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateToolGroupLiveExamples()
    {
        var t1 = new XYToolbarTool { Label = "选择", Icon = XyuiVectorIcon.Locate, ToolId = "g-select" };
        var t2 = new XYToolbarTool { Label = "平移", Icon = XyuiVectorIcon.Locate, ToolId = "g-move", IsSelected = true };
        var t3 = new XYToolbarTool { Label = "旋转", Icon = XyuiVectorIcon.StatusDot, ToolId = "g-rotate" };
        var groupTransform = new XYToolGroup(t1, t2, t3);

        var t4 = new XYToolbarTool { Label = "立方体", Icon = XyuiVectorIcon.Section, ToolId = "g-box" };
        var t5 = new XYToolbarTool { Label = "地表网格", Icon = XyuiVectorIcon.Eye, ToolId = "g-terrain" };
        var groupCreate = new XYToolGroup(t4, t5);

        var t6 = new XYToolbarTool { Label = "草丛画刷", Icon = XyuiVectorIcon.Code, ToolId = "g-brush", IsSelected = true };
        var t7 = new XYToolbarTool { Label = "水体生成", Icon = XyuiVectorIcon.Section, ToolId = "g-water" };
        var groupFoliage = new XYToolGroup(t6, t7) { IsCollapsed = true };

        var toolbar = new XYToolbar(groupTransform, groupCreate, groupFoliage) { IsCompact = true };
        var feedback = new TextBlock { Text = "当前组 1 选中: g-move | 组 3 为折叠态 (继承展示选中项 g-brush 图标，点击触发器展开)", Classes = { "xyui-text-caption" } };

        void Track(XYToolbarTool t) => t.SelectionRequested += (_, _) => feedback.Text = $"当前激活工具: {t.ToolId} ({t.Label}) | 组 1 活动: {groupTransform.ActiveToolId ?? "无"}";
        Track(t1); Track(t2); Track(t3); Track(t4); Track(t5); Track(t6); Track(t7);

        var toggleBtn = new XYButton { Content = "切换画刷组折叠/展开", Variant = XyuiButtonVariant.Secondary };
        toggleBtn.Click += (_, _) => groupFoliage.IsCollapsed = !groupFoliage.IsCollapsed;

        var panel = new StackPanel
        {
            Spacing = 14,
            Children =
            {
                new TextBlock { Text = "工具栏内嵌语义工具组 (包含内联组与单图标折叠组):", Classes = { "xyui-text-label" } },
                toolbar, feedback,
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 10, Children = { toggleBtn } }
            }
        };
        return WrapCard(panel, "工具组 · 24 DIP 垂直分割线、组内紧凑聚合与静态折叠");
    }

    static Control CreateToolGroupComposition()
    {
        var gNav = new XYToolGroup(new XYToolbarTool { Label = "聚焦", Icon = XyuiVectorIcon.Locate }, new XYToolbarTool { Label = "正交视图", Icon = XyuiVectorIcon.Eye, IsSelected = true });
        var gMod = new XYToolGroup(new XYToolbarTool { Label = "布尔差集", Icon = XyuiVectorIcon.Section }, new XYToolbarTool { Label = "倒角平滑", Icon = XyuiVectorIcon.StatusDot });
        var gExp = new XYToolGroup(new XYToolbarTool { Label = "拓扑分析", Icon = XyuiVectorIcon.Code, IsSelected = true }) { IsCollapsed = true };

        var bar = new XYToolbar(gNav, gMod, gExp) { IsCompact = true };
        var label = new TextBlock { Text = "几何体建模次级操作面板", Classes = { "xyui-text-label" } };
        var root = new StackPanel
        {
            Spacing = 8, Width = 640,
            Children =
            {
                label,
                new Border { Classes = { "xyui-surface-panel-alt" }, Padding = new(8), Child = bar },
                new TextBlock { Text = "注：不同几何阶段工具通过 ToolGroup 形成内聚段落，避免杂乱无章的大平铺。", Classes = { "xyui-text-caption" } }
            }
        };
        return WrapCard(root, "建模工具次级面板 · ToolGroup 阶段化聚合实践");
    }
}
