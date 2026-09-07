using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateNavigationRailLiveExamples()
    {
        var entries = new[]
        {
            new XYNavigationEntry("map", "地图系统", XyuiVectorIcon.Locate),
            new XYNavigationEntry("environment", "环境配置", XyuiVectorIcon.Eye),
            new XYNavigationEntry("dataset", "数据资产", XyuiVectorIcon.Code)
        };
        var contextMap = new Dictionary<string, IReadOnlyList<XYNavigationEntry>>
        {
            ["map"] = [new("base", "基础网格", XyuiVectorIcon.Section), new("terrain", "地形高程", XyuiVectorIcon.Section), new("vector", "矢量图层", XyuiVectorIcon.Section)],
            ["environment"] = [new("lighting", "天空光照", XyuiVectorIcon.Section), new("weather", "天气系统", XyuiVectorIcon.Section)],
            ["dataset"] = [new("tables", "数据表格", XyuiVectorIcon.Section), new("presets", "预设资产", XyuiVectorIcon.Section)]
        };
        var state = new XYNavigationState(entries, "map");
        var footer = new XYNavigationEntry("settings", "首选项", XyuiVectorIcon.Section);
        var rail = new XYNavigationRail(state, contextMap, footer, showExpandButton: true) { Width = 64, Height = 340 };

        var statusText = new TextBlock { Text = $"当前定位：{state.Selected?.Label} (点击图标呼出二级上下文菜单)", Classes = { "xyui-text-caption" } };
        var eventLog = new TextBlock { Text = "事件日志：就绪", Classes = { "xyui-text-caption" } };

        state.Changed += (_, _) => statusText.Text = $"当前定位：{state.Selected?.Label} (已激活对应 Flyout)";
        rail.ExpandRequested += (_, _) => eventLog.Text = "事件日志：触发 ExpandRequested · 响应式请求展开为 Sidebar (240 DIP)";

        var infoPanel = new StackPanel { Spacing = 8, Width = 340, Children = { statusText, eventLog } };
        var host = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 24, Children = { rail, infoPanel } };
        return WrapCard(host, "独立导航轨 · 64 DIP Canonical 轨与二级 Context Flyout 联动");
    }

    static Control CreateNavigationRailComposition()
    {
        var panel = new StackPanel { Spacing = 10 };
        var col1 = new StackPanel
        {
            Spacing = 6, Width = 160,
            Children =
            {
                new TextBlock { Text = "1. NavigationRail (折叠态)", Classes = { "xyui-text-label" } },
                new TextBlock { Text = "Canonical 64 DIP，纯图标，释放编辑器中央画布空间。", Classes = { "xyui-text-caption" }, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap },
                new Border { Classes = { "xyui-surface-panel" }, Width = 64, Height = 120, Child = new TextBlock { Text = "[ 64 DIP 轨 ]", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Classes = { "xyui-text-code" } } }
            }
        };
        var col2 = new StackPanel
        {
            Spacing = 6, Width = 260,
            Children =
            {
                new TextBlock { Text = "2. Sidebar (展开态)", Classes = { "xyui-text-label" } },
                new TextBlock { Text = "212~240 DIP，完整四段式结构，常驻展示深层要素树。", Classes = { "xyui-text-caption" }, TextWrapping = global::Avalonia.Media.TextWrapping.Wrap },
                new Border { Classes = { "xyui-surface-panel" }, Width = 240, Height = 120, Child = new TextBlock { Text = "[ 240 DIP 四段侧栏 ]", VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center, Classes = { "xyui-text-code" } } }
            }
        };
        var row = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 20, Children = { col1, col2 } };
        panel.Children.Add(row);
        panel.Children.Add(new TextBlock
        {
            Text = "响应式规则：宽度压缩时收拢为 64 DIP NavigationRail；展开时恢复用户记忆宽度 (ExpandedWidth)。两者 100% 共享单一 XYNavigationState 事实源。",
            Classes = { "xyui-text-caption" },
            TextWrapping = global::Avalonia.Media.TextWrapping.Wrap
        });
        return WrapCard(panel, "响应式演进对比 · Sidebar Expanded ⇄ NavigationRail Collapsed");
    }
}
