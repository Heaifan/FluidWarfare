using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateSidebarLiveExamples()
    {
        var primary = new[]
        {
            new XYNavigationItem { Id = "map", Label = "地图系统", Icon = XyuiVectorIcon.Locate, IsSelected = true },
            new XYNavigationItem { Id = "env", Label = "环境配置", Icon = XyuiVectorIcon.Eye },
            new XYNavigationItem { Id = "data", Label = "数据集", Icon = XyuiVectorIcon.Code }
        };
        var context = new[]
        {
            new XYNavigationItem { Id = "base", Label = "地图基础网格", Icon = XyuiVectorIcon.Section },
            new XYNavigationItem { Id = "terrain", Label = "地形高度图", Icon = XyuiVectorIcon.Section },
            new XYNavigationItem { Id = "vector", Label = "矢量要素层", Icon = XyuiVectorIcon.Section }
        };

        var sidebar = new XYSidebar { PrimaryItems = primary, ContextItems = context, Height = 420 };
        sidebar.Build();

        var toggleBtn = new XYButton { Content = "展开 / 折叠侧边栏 (IsCollapsed)" };
        toggleBtn.Click += (_, _) => sidebar.IsCollapsed = !sidebar.IsCollapsed;

        var footerFeedback = new TextBlock { Text = "Footer 状态：就绪", Classes = { "xyui-text-caption" } };
        sidebar.FooterInvoked += (_, _) => footerFeedback.Text = "Footer 已点击 · 打开系统设置浮层";

        var controls = new StackPanel { Spacing = 8, Children = { toggleBtn, footerFeedback } };
        var demo = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 20, Children = { sidebar, controls } };

        return WrapCard(demo, "完整侧边栏 · Primary Navigation + Context Region + Sticky Footer");
    }

    static Control CreateSidebarComposition()
    {
        var host = new Border
        {
            Classes = { "xyui-surface-panel" },
            Padding = new(10),
            CornerRadius = new(6),
            Height = 300
        };

        var mockSidebar = new Border
        {
            Classes = { "xyui-surface-panel" },
            Width = 140,
            Child = new StackPanel
            {
                Spacing = 6,
                Children =
                {
                    new TextBlock { Text = "玄域引擎", Classes = { "xyui-text-label" } },
                    new Border { Height = 1, Classes = { "xyui-surface-panel" } },
                    new TextBlock { Text = "[主导航区]", Classes = { "xyui-text-caption" } },
                    new TextBlock { Text = "· 地图\n· 环境\n· 数据", Classes = { "xyui-text-code" } },
                    new Border { Height = 1, Classes = { "xyui-surface-panel" } },
                    new TextBlock { Text = "[上下文区]", Classes = { "xyui-text-caption" } },
                    new TextBlock { Text = "· 基础要素\n· 地形", Classes = { "xyui-text-code" } },
                    new TextBlock { Text = "[置底 Footer]", Classes = { "xyui-text-caption" }, Margin = new(0, 40, 0, 0) }
                }
            }
        };

        var mockWorkspace = new Border
        {
            Classes = { "xyui-surface-panel" },
            Padding = new(12),
            Child = new TextBlock { Text = "中央编辑器工作区（随侧栏折叠自适应扩充）", Classes = { "xyui-text-body" } }
        };

        var layout = new DockPanel();
        DockPanel.SetDock(mockSidebar, Dock.Left);
        layout.Children.Add(mockSidebar);
        layout.Children.Add(mockWorkspace);
        host.Child = layout;

        var panel = new StackPanel { Spacing = 8 };
        panel.Children.Add(host);
        panel.Children.Add(new TextBlock
        {
            Text = "响应式演进说明：当窗口宽度进一步压缩至窄屏时，Sidebar 平滑降级为 NavigationRail (54 DIP)；但 3.07 NavigationRail 的独立完整交互留待下一轮开发，此处严守职责边界。",
            Classes = { "xyui-text-caption" },
            TextWrapping = global::Avalonia.Media.TextWrapping.Wrap
        });
        return WrapCard(panel, "桌面编辑器三区布局 · 侧栏置底与自适应容器");
    }
}
