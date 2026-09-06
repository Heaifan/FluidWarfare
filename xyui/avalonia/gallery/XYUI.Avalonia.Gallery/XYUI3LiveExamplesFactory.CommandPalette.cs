using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreateCommandPaletteLiveExamples()
    {
        var commands = new[]
        {
            new XYPaletteCommand("cmd-create-road", "创建主干道路", XYPaletteCommandType.Command, "地图", "在当前选定网格创建高等级沥青道路。", "Ctrl+Alt+R", ["创建", "道路", "road"]),
            new XYPaletteCommand("cmd-bake-light", "烘焙全局光照", XYPaletteCommandType.Command, "渲染", "使用 Vulkan 计算管线烘焙当前场景辐射度。", "F6", ["光照", "烘焙", "light"]),
            new XYPaletteCommand("obj-player-spawner", "角色出生点_01", XYPaletteCommandType.Object, "场景实体", "跳转并选中战场核心区域出生点。", "Alt+G", ["出生点", "spawner"]),
            new XYPaletteCommand("nav-asset-browser", "导航至资产视口", XYPaletteCommandType.Navigation, "视图", "将底部活动面板切换至工程资产浏览器。", "Ctrl+1", ["资产", "视口", "asset"]),
            new XYPaletteCommand("set-render-quality", "设置渲染品质: 极高", XYPaletteCommandType.Setting, "配置", "开启各向异性过滤与接触硬阴影。", "Ctrl+,", ["设置", "渲染", "setting"])
        };

        var palette = new XYCommandPalette(commands, [commands[0], commands[2], commands[3]]) { Width = 580 };
        var feedback = new TextBlock { Text = "就绪: 支持在上方直接输入关键字过滤、前缀 (> @ # :) 检索或使用键盘 ↑/↓/Enter 选择执行", Classes = { "xyui-text-caption" } };

        palette.ExecuteRequested += (_, item) => feedback.Text = $"已执行命令: [{item.Label}] (ID: {item.Id}, 类别: {item.Category})";

        var root = new StackPanel
        {
            Spacing = 12,
            Children =
            {
                new TextBlock { Text = "快速命令搜索面板 (左右双栏：左侧结果列表，右侧详情与快捷键):", Classes = { "xyui-text-label" } },
                palette,
                feedback
            }
        };
        return WrapCard(root, "命令面板 · 440~580 DIP 高密检索、全键盘闭环与分类前缀");
    }

    static Control CreateCommandPaletteComposition()
    {
        var commands = new[]
        {
            new XYPaletteCommand("cmd-save-all", "保存所有场景", XYPaletteCommandType.Command, "工程", "保存当前工程中所有已修改场景与资源。", "Ctrl+Shift+S", ["保存"]),
            new XYPaletteCommand("cmd-run-sim", "运行战争推演", XYPaletteCommandType.Command, "仿真", "启动多军团战场 AI 实时模拟。", "F5", ["推演", "运行"])
        };
        var modalPalette = new XYCommandPalette(commands);
        var feedback = new TextBlock { Text = "浮动快捷键状态: 等待唤起", Classes = { "xyui-text-caption" }, VerticalAlignment = VerticalAlignment.Center };

        var openBtn = new XYButton { Content = "唤起浮动命令面板 (Ctrl+P)", Variant = XyuiButtonVariant.Primary };
        openBtn.Click += (_, _) =>
        {
            modalPalette.Open(openBtn);
            feedback.Text = "浮动命令面板已弹出 (支持 Esc 或失焦关闭)";
        };
        modalPalette.ExecuteRequested += (_, cmd) => feedback.Text = $"浮动面板执行完成: {cmd.Label}";

        var bar = new StackPanel { Orientation = Orientation.Horizontal, Spacing = 14, Children = { openBtn, feedback } };
        var panel = new StackPanel
        {
            Spacing = 8, Width = 640,
            Children =
            {
                new TextBlock { Text = "全局快捷键唤起与浮层生命周期", Classes = { "xyui-text-label" } },
                bar,
                new TextBlock { Text = "注：命令面板通过 Popup 居中呈现，在失焦、窗口切换或按 Esc 时安全复位与恢复原焦点。", Classes = { "xyui-text-caption" } }
            }
        };
        return WrapCard(panel, "全局浮层命令面板 · 居中弹层与安全生命周期");
    }
}
