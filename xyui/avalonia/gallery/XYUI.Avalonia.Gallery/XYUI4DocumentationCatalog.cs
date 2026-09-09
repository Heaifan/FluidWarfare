using XYUI.Avalonia.Catalog;

namespace XYUI.Avalonia.Gallery;

public static class XYUI4DocumentationCatalog
{
    static readonly IReadOnlySet<string> ComponentIds = new HashSet<string> { "XYUI-4-4.14", "XYUI-4-4.15" };

    public static IReadOnlyList<XYUI1ComponentDocument> Build() => XyuiCatalogSource.Load()
        .Where(x => ComponentIds.Contains(x.SourceItemId)).Select(Create).ToArray();

    static XYUI1ComponentDocument Create(XyuiCatalogEntry entry)
    {
        var id = entry.SourceItemId;
        var type = entry.AvaloniaType.Split('.').Last();
        return new(id, ChineseName(id), type, Overview(id), WhenToUse(id),
            () => XYUI4GalleryCatalog.CreatePreview(id), Usages(id), Variants(id), States(id),
            Properties(id), Tokens(id), type)
        {
            CanonicalIdentity = entry.CanonicalIdentity,
            Category = "Canonical Stable · Feedback / Activity",
            Acceptance = "RUNTIME IMPLEMENTED · READY FOR USER VISUAL ACCEPTANCE",
            QuickStartXaml = QuickStart(id), CoreRules = Rules(id),
            FoundationMappings = Foundations(id), HowToUse = Guides(id),
            LiveExamplesFactory = () => XYUI4GalleryCatalog.CreateLiveExamples(id)
        };
    }

    static string ChineseName(string id) => id == "XYUI-4-4.14" ? "加载指示" : "旋转加载";
    static string Overview(string id) => id == "XYUI-4-4.14"
        ? "LoadingIndicator 表达任务正在进行，并提供任务上下文；本轮用于 Vulkan 视口初始化。"
        : "Spinner 只表达无法可靠量化进度的持续活动图形，使用 Open Arc 造型。";
    static string WhenToUse(string id) => id == "XYUI-4-4.14"
        ? "用于视口、资源读取和其他不确定完成时间的初始化任务。"
        : "用于 LoadingIndicator 内部或短任务入口中的不确定活动反馈。";
    static string[] Usages(string id) => id == "XYUI-4-4.14"
        ? ["<c:XYLoadingIndicator Text=\"正在初始化渲染视口…\" Size=\"Compact\" />"]
        : ["<c:XYSpinner Size=\"Standard\" IsActive=\"True\" />"];
    static XYUIDocVariant[] Variants(string id) => id == "XYUI-4-4.14"
        ? [new("Inline", "Spinner + 主标签", "Area C Compact"), new("Detail", "Spinner + 主标签 + 次级上下文", "资源或后端初始化")]
        : [new("Compact", "14 × 14", "Area C"), new("Standard / Large", "18 / 24 DIP", "通用任务反馈")];
    static XYUIDocState[] States(string id) => id == "XYUI-4-4.14"
        ? [new("Active", "任务持续时显示活动图形"), new("Failure", "任务失败后退出 Loading，转入错误反馈")]
        : [new("Active", "800–1200ms 旋转 Open Arc"), new("Reduced Motion", "静态弧，不启动计时器")];

    static IReadOnlyList<XYUIDocProperty> Properties(string id) => id == "XYUI-4-4.14"
        ? [P("Text", "string", "正在加载…"), P("SecondaryText", "string", "空"), P("Size", "XyuiSpinnerSize", "Standard"), P("Variant", "XyuiLoadingIndicatorVariant", "Inline"), P("IsActive", "bool", "true")]
        : [P("Size", "XyuiSpinnerSize", "Standard"), P("IsActive", "bool", "true"), P("IsReducedMotion", "bool", "false"), P("Track / Arc", "IBrush?", "Theme Token")];
    static IReadOnlyList<XYUIDocToken> Tokens(string id) => id == "XYUI-4-4.14"
        ? [T("Indicator", "XY.Accent.Soft / XY.Color.Accent"), T("Text", "XY.Text.Secondary"), T("Background", "Transparent"), T("LayoutShift", "Forbidden")]
        : [T("Track", "XY.Accent.Soft"), T("Arc", "XY.Color.Accent"), T("Stroke", "2–3 DIP"), T("Center / Shadow", "Transparent / None")];

    static string QuickStart(string id) => id == "XYUI-4-4.14" ? "<c:XYLoadingIndicator Text=\"正在初始化渲染视口…\" Size=\"Compact\" />" : "<c:XYSpinner Size=\"Standard\" />";
    static IReadOnlyList<XYUIDocRule> Rules(string id) => id == "XYUI-4-4.14"
        ? [new("任务上下文", "LoadingIndicator 不显示孤立 Spinner，必须保留任务标签或上下文。"), new("真实状态", "禁止伪造进度；失败后退出 Loading。")]
        : [new("Open Arc", "完整低对比轨道加一段 Accent 弧，圆帽、透明中心。"), new("动效生命周期", "不可见或 Reduced Motion 时停止计时器。")];
    static IReadOnlyList<XYUIDocFoundationItem> Foundations(string id) => [new("颜色", id == "XYUI-4-4.14" ? "XY.Accent.Soft / XY.Color.Accent" : "XY.Accent.Soft / XY.Color.Accent", "由主题动态资源提供")];
    static IReadOnlyList<XYUIDocGuideItem> Guides(string id) => [new("Area C", id == "XYUI-4-4.14" ? "放在 Vulkan Viewport 左下角、Scale Indicator 上方。" : "作为 4.14 LoadingIndicator 的内部基础活动图形。")];
    static XYUIDocProperty P(string n, string t, string d) => new(n, t, d, "组件属性");
    static XYUIDocToken T(string n, string v) => new(n, v, "来自 XYUI Foundation 的语义 Token");
}
