using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateNavigationDrawerLiveExamples()
    {
        var state = new XYNavigationState([
            new("map", "地图编辑", XyuiVectorIcon.Locate),
            new("data", "数据配置", XyuiVectorIcon.Code),
            new("settings", "系统设置", XyuiVectorIcon.Section)
        ], "map");
        var drawer = new XYNavigationDrawer(state);

        var feedback = new TextBlock { Text = "抽屉状态: 已收起 · 支持点击遮罩背景、按 Esc 键退出或点击导航项", Classes = { "xyui-text-caption" } };
        drawer.Closed += (_, _) => feedback.Text = $"抽屉状态: 已关闭 (Light Dismiss / Esc / 代码触发) · 当前目的地: [{state.SelectedId}]";
        state.Changed += (_, _) => feedback.Text = $"抽屉内目的地切换至: [{state.SelectedId}] · 主视口已同步";

        var btnOpen = new XYButton { Content = "展开抽屉 (Open)", Variant = XyuiButtonVariant.Primary };
        btnOpen.Click += (_, _) => { drawer.Open(); feedback.Text = "抽屉状态: 已展开 · 半透明遮罩激活，捕获焦点"; };
        var btnClose = new XYButton { Content = "代码关闭 (Close)", Variant = XyuiButtonVariant.Secondary };
        btnClose.Click += (_, _) => drawer.Close();

        var col = new StackPanel
        {
            Spacing = 12,
            Children =
            {
                new TextBlock { Text = "模态抽屉导航 (包含半透明遮罩、Esc 键监听、失焦退出与全功能 Sidebar 结构):", Classes = { "xyui-text-label" } },
                drawer,
                feedback,
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 8, Children = { btnOpen, btnClose } }
            }
        };
        return WrapCard(col, "响应式模态抽屉导航 · 侧边栏结构镜像、遮罩退出与键盘生命周期");
    }

    static Control CreateNavigationDrawerComposition()
    {
        var state = new XYNavigationState([
            new("map", "地图", XyuiVectorIcon.Locate),
            new("data", "数据", XyuiVectorIcon.Code),
            new("settings", "设置", XyuiVectorIcon.Section)
        ], "map");
        var drawer = new XYNavigationDrawer(state);

        var burger = new XYButton { Content = "☰ 导航", Variant = XyuiButtonVariant.Secondary };
        burger.Click += (_, _) => drawer.Open();

        var topBar = new Grid
        {
            ColumnDefinitions = new ColumnDefinitions("Auto,*,Auto"),
            Height = 36,
            Children = { burger, new TextBlock { Text = "窄屏编辑器视口 (宽度受限时收拢侧栏为抽屉)", VerticalAlignment = VerticalAlignment.Center, Classes = { "xyui-text-caption" }, Margin = new(12, 0) } }
        };
        Grid.SetColumn((TextBlock)topBar.Children[1], 1);

        var panel = new StackPanel
        {
            Spacing = 8,
            Children = { topBar, drawer, new TextBlock { Text = "说明: 点击汉堡按钮呼出抽屉，点击空白遮罩或按 Esc 即关闭，保持窄屏下视口最大化。", Classes = { "xyui-text-caption" } } }
        };
        return WrapCard(panel, "窄屏视口汉堡菜单与抽屉协同 · 响应式导航收拢形态");
    }
}
