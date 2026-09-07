using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;
using XYUI.Avalonia.Vector;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateMenuLiveExamples()
    {
        var feedback = new TextBlock { Text = "就绪 · 点击菜单项体验交互与状态切换", Classes = { "xyui-text-caption" } };
        XYMenuItem M(string label, string sc = "", XyuiVectorIcon? icon = null, bool enabled = true,
            bool checkedItem = false, XyuiMenuCheckKind check = XyuiMenuCheckKind.None, bool danger = false, bool sub = false)
        {
            var item = new XYMenuItem
            {
                Label = label, Shortcut = sc, Icon = icon, IsEnabled = enabled,
                IsChecked = checkedItem, CheckKind = check, IsDestructive = danger, HasSubMenu = sub
            };
            item.Invoked += (_, _) => feedback.Text = $"已触发 · {label}";
            return item;
        }

        var menu = new XYMenu(
            M("普通命令 · 保存", "Ctrl+S"),
            M("带图标命令 · 导入资产", "Ctrl+I", XyuiVectorIcon.Add),
            M("快捷键提示 · 查找", "Ctrl+F"),
            XYMenu.Separator(),
            M("勾选状态 · 显示对齐辅助线", checkedItem: true, check: XyuiMenuCheckKind.Check),
            M("单选状态 A · 实体坐标模式", checkedItem: true, check: XyuiMenuCheckKind.Radio),
            M("单选状态 B · 世界坐标模式", check: XyuiMenuCheckKind.Radio),
            XYMenu.Separator(),
            M("禁用命令 · 恢复默认值", "Ctrl+Shift+R", enabled: false),
            M("子菜单入口 · 导出为...", sub: true),
            XYMenu.Separator(),
            M("危险操作 · 删除选中对象", danger: true)
        );

        var panel = new StackPanel { Spacing = 10, Width = 280 };
        panel.Children.Add(menu);
        panel.Children.Add(feedback);
        return WrapCard(panel, "标准桌面菜单 · Leading / Label / Shortcut / Chevron 四列对齐");
    }

    static Control CreateMenuComposition()
    {
        var feedback = new TextBlock { Text = "当前选中模式：透视投影", Classes = { "xyui-text-caption" } };
        var radio1 = new XYMenuItem { Label = "透视投影 (Perspective)", CheckKind = XyuiMenuCheckKind.Radio, IsChecked = true };
        var radio2 = new XYMenuItem { Label = "正交投影 (Orthographic)", CheckKind = XyuiMenuCheckKind.Radio };
        var radio3 = new XYMenuItem { Label = "等轴投影 (Isometric)", CheckKind = XyuiMenuCheckKind.Radio };

        void SelectRadio(XYMenuItem target)
        {
            radio1.IsChecked = ReferenceEquals(target, radio1);
            radio2.IsChecked = ReferenceEquals(target, radio2);
            radio3.IsChecked = ReferenceEquals(target, radio3);
            feedback.Text = $"当前选中模式：{target.Label}";
        }
        radio1.Invoked += (_, _) => SelectRadio(radio1);
        radio2.Invoked += (_, _) => SelectRadio(radio2);
        radio3.Invoked += (_, _) => SelectRadio(radio3);

        var menu = new XYMenu(radio1, radio2, radio3, XYMenu.Separator(),
            new XYMenuItem { Label = "重置相机参数", Icon = XyuiVectorIcon.Browse });

        var panel = new StackPanel { Spacing = 8, Width = 280 };
        panel.Children.Add(menu);
        panel.Children.Add(feedback);
        return WrapCard(panel, "视图模式互斥单选组 · 状态流转与单选联动");
    }
}
