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
        state.Changed += (_, _) => feedback.Text = $"标准导航目的地已切换至: [{state.SelectedId}] · 槽位宽度绝对稳定";
        primaryState.Changed += (_, _) => feedback.Text = $"主操作导航目的地已切换至: [{primaryState.SelectedId}]";
        primaryNav.PrimaryActionRequested += (_, _) => feedback.Text = "操作反馈: 触发中央独立 Primary Action [新建图层] (目的地保持不变)";

        var col = new StackPanel
        {
            Spacing = 12,
            Children =
            {
                new TextBlock { Text = "360 DIP 窄宽验证 (5 槽位等宽排布 · 图标在上文字在下 · 包含数字角标):", Classes = { "xyui-text-label" } },
                standard,
                new TextBlock { Text = "中央独立主操作按钮 (Primary Action 独立派发事件，不占用目的地状态):", Classes = { "xyui-text-label" } },
                primaryNav,
                feedback
            }
        };
        return WrapCard(col, "移动端等宽底部导航 · 360 DIP 窄宽无裁切、角标提示与中央主操作");
    }

    static Control CreateBottomNavigationComposition()
    {
        var items = new[]
        {
            new XYBottomNavigationItem("map", "地图", XyuiVectorIcon.Locate),
            new XYBottomNavigationItem("data", "数据", XyuiVectorIcon.Code),
            new XYBottomNavigationItem("logs", "日志", XyuiVectorIcon.Section)
        };
        var state = new XYNavigationState(items.Select(i => new XYNavigationEntry(i.Id, i.Label, i.Icon)), "map");
        var nav = new XYBottomNavigation(state, items) { Width = 320 };
        nav.DestinationRequested += (_, req) => req.Accept();

        var phoneShell = new Border
        {
            Width = 322,
            Height = 180,
            Classes = { "xyui-surface-panel-alt" },
            CornerRadius = new(8),
            BorderThickness = new(1),
            Child = new Grid
            {
                RowDefinitions = new RowDefinitions("*,Auto"),
                Children =
                {
                    new TextBlock { Text = "【移动端视口预览】\n底部导航吸附于页面下缘", HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center, Classes = { "xyui-text-caption" } },
                    nav
                }
            }
        };
        Grid.SetRow(nav, 1);

        return WrapCard(phoneShell, "移动端/触控外壳界面框架组合 · 底部等宽吸附与核心目的地切换");
    }
}
