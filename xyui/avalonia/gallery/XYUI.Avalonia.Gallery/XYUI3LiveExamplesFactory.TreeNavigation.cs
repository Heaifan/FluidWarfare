using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateTreeNavigationLiveExamples()
    {
        var node0 = new XYTreeNode { Label = "地图系统", Depth = 0, HasChildren = true, IsExpanded = true, Icon = XyuiVectorIcon.Locate };
        var node1 = new XYTreeNode { Label = "基础要素", Depth = 1, IsSelected = true, Icon = XyuiVectorIcon.Section };
        var node2 = new XYTreeNode { Label = "环境配置", Depth = 1, HasChildren = true, IsExpanded = true, Icon = XyuiVectorIcon.Eye };
        var node3 = new XYTreeNode { Label = "地形高度图", Depth = 2, Icon = XyuiVectorIcon.Section };
        var node4 = new XYTreeNode { Label = "气候与光照", Depth = 2, Icon = XyuiVectorIcon.Section };
        var node5 = new XYTreeNode { Label = "数据集合", Depth = 0, HasChildren = false, Icon = XyuiVectorIcon.Code };

        var tree = new XYTreeNavigation(node0, node1, node2, node3, node4, node5) { Width = 260 };

        var statusText = new TextBlock { Text = "当前选中：基础要素 (层级深度: 1)", Classes = { "xyui-text-caption" } };
        var eventLog = new TextBlock { Text = "操作指南：点击展开/收起箭头切换子树；点击节点选中；支持键盘 Left/Right/Up/Down 导航。", Classes = { "xyui-text-caption" } };

        tree.SelectionChanged += (_, node) => statusText.Text = $"当前选中：{node.Label} (层级深度: {node.Depth})";

        var infoPanel = new StackPanel { Spacing = 8, Width = 280, Children = { statusText, eventLog } };
        var host = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 20, Children = { tree, infoPanel } };
        return WrapCard(host, "高密度树状导航 · 真实折叠展开、引导线对齐与祖先高亮");
    }

    static Control CreateTreeNavigationComposition()
    {
        var col1 = new StackPanel
        {
            Spacing = 6, Width = 240,
            Children =
            {
                new TextBlock { Text = "高密度树结构 (28 DIP 行高)", Classes = { "xyui-text-label" } },
                new TextBlock { Text = "16 DIP 缩进，弱化引导线，左侧 Accent Bar，保持桌面引擎级高信息密度。", Classes = { "xyui-text-caption" }, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap },
                new Border
                {
                    Classes = { "xyui-surface-panel" },
                    Padding = new(8),
                    Child = new TextBlock { Text = "├─ 场景根节点 (Depth 0)\n│  ├─ 静态几何体 (Depth 1)\n│  └─ 动态角色层 (Depth 1)", Classes = { "xyui-text-code" } }
                }
            }
        };

        var col2 = new StackPanel
        {
            Spacing = 6, Width = 280,
            Children =
            {
                new TextBlock { Text = "要素树与属性视口联动模式", Classes = { "xyui-text-label" } },
                new TextBlock { Text = "树导航作为左侧层级事实源，驱动中央画布对象选中与右侧属性栏同步更新。", Classes = { "xyui-text-caption" }, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap },
                new Border
                {
                    Classes = { "xyui-surface-panel" },
                    Padding = new(8),
                    Child = new TextBlock { Text = "[ 树节点选中 ] ➔ 驱动 ➔ [ Inspector 属性更新 ]", Classes = { "xyui-text-caption" } }
                }
            }
        };

        var grid = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 20, Children = { col1, col2 } };
        return WrapCard(grid, "高密度工程场景 · 树导航在编辑器工作流中的协同角色");
    }
}
