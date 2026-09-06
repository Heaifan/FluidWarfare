using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateBottomNavigationLiveExamples()
    {
        var items = new[]
        {
            new XYBottomNavigationItem("map", "地图", XyuiVectorIcon.Locate),
            new XYBottomNavigationItem("data", "数据", XyuiVectorIcon.Code),
            new XYBottomNavigationItem("experiment", "实验", XyuiVectorIcon.Clear),
            new XYBottomNavigationItem("logs", "日志", XyuiVectorIcon.Section, "1"),
            new XYBottomNavigationItem("settings", "设置", XyuiVectorIcon.Info)
        };
        var state = new XYNavigationState(items.Select(i => new XYNavigationEntry(i.Id, i.Label, i.Icon)), "map");
        var standard = new XYBottomNavigation(state, items) { Width = 360 };
        standard.DestinationRequested += (_, req) => req.Accept();

        var primaryItems = items.Where(i => i.Id != "experiment").ToArray();
        var primaryState = new XYNavigationState(primaryItems.Select(i => new XYNavigationEntry(i.Id, i.Label, i.Icon)), "map");
        var primaryBtn = new XYButton { Content = new XYIcon { Icon = XyuiVectorIcon.Add, Size = XyuiIconSize.Small }, Variant = XyuiButtonVariant.Primary };
        var primaryNav = new XYBottomNavigation(primaryState, primaryItems, primaryBtn) { Width = 360 };
        primaryNav.DestinationRequested += (_, req) => req.Accept();

        var feedback = new TextBlock { Text = "当前目的地: map (地图) · 槽位宽度: 72 DIP (等宽锁定，无抖动)", Classes = { "xyui-text-caption" } };
        var tip = new TextBlock { Text = "说明: 中央 Primary Action (54×54 圆形按钮) 向上浮出 14 DIP，ClipToBounds=false 确保上半部悬浮区域完整响应点击。", Classes = { "xyui-text-caption" } };
        state.Changed += (_, _) => feedback.Text = $"标准导航目的地已切换至: [{state.SelectedId}] · 槽位宽度绝对稳定";
        primaryState.Changed += (_, _) => feedback.Text = $"主操作导航目的地已切换至: [{primaryState.SelectedId}]";
        primaryNav.PrimaryActionRequested += (_, _) => feedback.Text = "操作反馈: 触发中央独立 Primary Action [新建图层] (目的地保持不变，悬浮上半部点击有效)";

        var col = new StackPanel
        {
            Spacing = 10,
            Children =
            {
                new TextBlock { Text = "360 DIP 窄宽验证 (5 槽位等宽排布 · 图标在上文字在下 · 包含数字角标):", Classes = { "xyui-text-label" } },
                standard,
                new TextBlock { Text = "中央独立主操作按钮 (54×54 悬浮圆形按钮，独立派发事件且不占目的地状态):", Classes = { "xyui-text-label" } },
                primaryNav, tip, feedback
            }
        };
        return WrapCard(col, "移动端等宽底部导航 · 360 DIP 窄宽无裁切、角标提示与中央主操作");
    }

    static Control CreateBottomNavigationComposition()
    {
        var items = new[] { new XYBottomNavigationItem("map", "地图", XyuiVectorIcon.Locate), new XYBottomNavigationItem("data", "数据", XyuiVectorIcon.Code), new XYBottomNavigationItem("logs", "日志", XyuiVectorIcon.Section) };
        var nav0 = new XYBottomNavigation(items) { Width = 240, SafeAreaBottom = 0 };
        var nav24 = new XYBottomNavigation(items) { Width = 240, SafeAreaBottom = 24 };
        Control MockPhone(string title, XYBottomNavigation nav, string desc)
        {
            var grid = new Grid { RowDefinitions = new RowDefinitions("*,Auto") };
            grid.Children.Add(new StackPanel { Margin = new(8, 12), Spacing = 4, Children = { new TextBlock { Text = title, Classes = { "xyui-text-label" } }, new TextBlock { Text = desc, Classes = { "xyui-text-caption" } } } });
            Grid.SetRow(nav, 1); grid.Children.Add(nav);
            return new Border { Width = 242, Classes = { "xyui-surface-panel-alt" }, CornerRadius = new(8), BorderThickness = new(1), Child = grid };
        }
        var row = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 16, Children = { MockPhone("标准模式 (SafeArea=0)", nav0, "高度 66 DIP，槽位紧贴下边缘"), MockPhone("手势区 (SafeArea=24)", nav24, "高度 90 DIP，内衬 24 DIP 安全边距") } };
        return WrapCard(row, "移动端全面屏安全区适配对比 · SafeAreaBottom (0 vs 24 DIP) 内衬与高度伸缩");
    }
}
