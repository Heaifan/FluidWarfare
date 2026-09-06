namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3DocumentationCatalog
{
    static XYUI1ComponentDocument BuildNavigationRailDoc(string id, string type) => new(
        id, "导航轨", "NavigationRail",
        "桌面空间不足或侧边栏折叠时的紧凑图标导航轨，标准宽度 64 DIP，以纯图标槽位 + 左侧 Accent 保持位置定位。",
        "用于空间受限或折叠态编辑器主布局；支持独立主要图标、展开触发器、置底快捷入口与按需呼出上下文二级菜单。",
        () => XYUI3GalleryCatalog.CreatePreview(id),
        ["<c:XYNavigationRail NavigationState=\"{Binding NavigationState}\" Width=\"64\" />"],
        [new("Compact V2", "标准 64 DIP 紧凑图标轨，居中图标与左侧 Accent Bar", "Icon Rail"), new("Context Flyout", "点击一级图标锚定弹出二级 SubMenu 浮层", "Flyout Mode")],
        [new("Selected", "浅蓝背景 + 左侧 3 DIP Accent Bar"), new("Hover", "轻微悬浮背景高亮"), new("Focus", "XY.Focus 独立键盘外框"), new("Disabled", "图标对比度衰减不可导航")],
        Properties(id),
        [new("XY.Surface.Panel", "Panel", "导航轨容器底色"), new("XY.Brush.Accent.Default", "Accent", "左侧选中指示条"), new("XY.Border.Color.Subtle", "Subtle", "右侧与顶部边框线")],
        type)
    {
        CanonicalIdentity = "3.07 · NavigationRail / 导航轨",
        Category = "XYUI-3 · 导航与切换",
        Acceptance = "UI + INTERACTION IMPLEMENTED · AWAITING USER VISUAL ACCEPTANCE · AWAITING USER INTERACTION ACCEPTANCE",
        QuickStartXaml = """
<c:XYNavigationRail NavigationState="{Binding NavigationState}"
                    Width="64" />
""",
        CoreRules =
        [
            new("明确导航语义", "每个图标必须代表明确的主功能模块（如地图、环境、数据），禁止摆放无语义装饰图标。"),
            new("单一事实源驱动", "导航状态 100% 共享自 XYNavigationState，保证 Sidebar 展开与 Rail 折叠平滑双向同步。"),
            new("二级浮层解耦", "通过 OpenContext 消费真实 XYSubMenu 作为二级上下文菜单，不自造私有弹出层。")
        ],
        DoDonts =
        [
            new("尺寸契约", "DO: 遵循 Canonical NavigationRail Width (64 DIP) 权威真值。", "DON'T: 在代码或文档中手写 54 DIP 等历史过时数字。", "统一尺寸避免侧栏折叠发生视觉跳动。"),
            new("上下文交互", "DO: 点击一级图标直接呼出锚定二级浮层，并支持 Light Dismiss。", "DON'T: 必须再次展开 Sidebar 才能查看二级子项。", "折叠轨应具备高效率临时定位能力。"),
            new("置底区域", "DO: 将设置、收起/展开按钮固定置底吸附。", "DON'T: 将辅助入口随主导航列表滚动。", "常用系统级入口必须随时处于固定可点击位置。")
        ],
        LiveExamplesFactory = () => XYUI3LiveExamplesFactory.CreateLiveExamples(id)!,
        CompositionFactory = () => XYUI3LiveExamplesFactory.CreateComposition(id)!
    };
}
