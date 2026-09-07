using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateMenuBarLiveExamples()
    {
        var feedback = new TextBlock { Text = "就绪 · 点击菜单栏项执行命令", Classes = { "xyui-text-caption" } };
        XYMenuItem Act(string label, string sc = "", XyuiVectorIcon? icon = null, bool enabled = true,
            bool checkedItem = false, XyuiMenuCheckKind check = XyuiMenuCheckKind.None)
        {
            var item = new XYMenuItem { Label = label, Shortcut = sc, Icon = icon, IsEnabled = enabled, IsChecked = checkedItem, CheckKind = check };
            item.Invoked += (_, _) => feedback.Text = $"已触发命令 · {label}";
            return item;
        }

        var fileMenu = new XYMenu(Act("新建项目", "Ctrl+N", XyuiVectorIcon.Add), Act("打开工程", "Ctrl+O", XyuiVectorIcon.Browse), Act("保存", "Ctrl+S"), Act("另存为..."), XYMenu.Separator(), Act("退出"));
        var editMenu = new XYMenu(Act("撤销", "Ctrl+Z"), Act("重做", "Ctrl+Y", enabled: false), XYMenu.Separator(), Act("剪切", "Ctrl+X"), Act("复制", "Ctrl+C"), Act("粘贴", "Ctrl+V"));
        var viewMenu = new XYMenu(Act("显示网格", checkedItem: true, check: XyuiMenuCheckKind.Check), Act("正交视图", checkedItem: true, check: XyuiMenuCheckKind.Radio), Act("透视视图", check: XyuiMenuCheckKind.Radio), XYMenu.Separator(), Act("重置视口"));
        var helpMenu = new XYMenu(Act("快捷键指南"), XYMenu.Separator(), Act("关于玄域引擎"));

        var bar = new XYMenuBar(
            new XYMenuBarItem { Label = "文件", Menu = fileMenu },
            new XYMenuBarItem { Label = "编辑", Menu = editMenu },
            new XYMenuBarItem { Label = "视图", Menu = viewMenu },
            new XYMenuBarItem { Label = "帮助", Menu = helpMenu });

        var panel = new StackPanel { Spacing = 10 };
        panel.Children.Add(bar);
        panel.Children.Add(feedback);
        return WrapCard(panel, "桌面一级菜单栏 · 点击展开真实 Popup 菜单");
    }

    static Control CreateMenuBarComposition()
    {
        var host = new Border { Classes = { "xyui-surface-panel" }, Padding = new(8, 4), CornerRadius = new(4) };
        var dock = new DockPanel();
        var chip = new Border
        {
            Classes = { "xyui-surface-panel" },
            Padding = new(8, 3),
            CornerRadius = new(3),
            Child = new TextBlock { Text = "工作区：地图编辑", Classes = { "xyui-text-caption" } }
        };
        DockPanel.SetDock(chip, Dock.Right);
        dock.Children.Add(chip);

        var bar = new XYMenuBar(
            new XYMenuBarItem { Label = "项目" },
            new XYMenuBarItem { Label = "地图", IsActive = true },
            new XYMenuBarItem { Label = "图层" },
            new XYMenuBarItem { Label = "渲染" });
        dock.Children.Add(bar);
        host.Child = dock;

        var panel = new StackPanel { Spacing = 8 };
        panel.Children.Add(host);
        panel.Children.Add(new TextBlock
        {
            Text = "键盘导航规范：Left / Right 切换相邻菜单项；Enter / Down 打开菜单；Esc 关闭菜单。",
            Classes = { "xyui-text-caption" },
            TextWrapping = global::Avalonia.Media.TextWrapping.Wrap
        });
        return WrapCard(panel, "主窗口顶栏集成 · 状态线跟随 Active 项");
    }
}
