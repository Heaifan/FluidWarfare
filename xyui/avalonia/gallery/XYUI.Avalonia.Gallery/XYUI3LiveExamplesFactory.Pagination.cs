using Avalonia.Controls;
using Avalonia.Layout;
using XYUI.Avalonia.Controls;

namespace XYUI.Avalonia.Gallery;

public static partial class XYUI3LiveExamplesFactory
{
    static Control CreatePaginationLiveExamples()
    {
        var pagination = new XYPagination { CurrentPage = 3, TotalPages = 20, TotalItems = 400, ShowTotalItems = true };
        var statusText = new TextBlock { Text = "当前活动页: 第 3 页 · 邻近页 [2, 3, 4] · 首尾按钮正常可用", Classes = { "xyui-text-caption" } };
        pagination.PageChanged += (_, p) => statusText.Text = $"当前活动页: 第 {p} 页 · 邻近页 [{p - 1}, {p}, {p + 1}] · 上一页: {(p > 1 ? "可用" : "禁用")}, 下一页: {(p < 20 ? "可用" : "禁用")}";
        pagination.InvalidPageRequested += (_, p) => statusText.Text = $"越界页码已拦截: {p} (合法区间 1 ~ 20)";

        var firstPage = new XYPagination { CurrentPage = 1, TotalPages = 10, TotalItems = 100, ShowTotalItems = false };
        var lastPage = new XYPagination { CurrentPage = 10, TotalPages = 10, TotalItems = 100, ShowTotalItems = false };

        var footer = new XYPaginationFooter(totalItems: 640, totalPages: 26);

        var col = new StackPanel
        {
            Spacing = 12,
            Children =
            {
                new TextBlock { Text = "交互式邻近页跳转与跳页输入 (支持点击与输入 Enter):", Classes = { "xyui-text-label" } },
                pagination,
                statusText,
                new TextBlock { Text = "边界禁用状态对比 (首页 Prev 禁用 / 末页 Next 禁用):", Classes = { "xyui-text-label" } },
                new StackPanel { Orientation = Orientation.Horizontal, Spacing = 20, Children = { firstPage, lastPage } },
                new TextBlock { Text = "标准数据页脚 XYPaginationFooter (含每页条数 25/50/100 联动):", Classes = { "xyui-text-label" } },
                footer
            }
        };
        return WrapCard(col, "高密度分页与数据页脚 · 34 DIP 等高同轴对齐");
    }

    static Control CreatePaginationComposition()
    {
        var tableHeader = new Border
        {
            Classes = { "xyui-surface-panel-alt" },
            Padding = new(10, 6),
            Child = new TextBlock { Text = "名称　　　　　　　类型　　　　大小　　　　修改时间", Classes = { "xyui-text-code" } }
        };
        var row1 = new Border { Padding = new(10, 6), Child = new TextBlock { Text = "Terrain_Highland.raw　HeightMap　32.4 MB　2026-09-01 10:20", Classes = { "xyui-text-caption" } } };
        var row2 = new Border { Padding = new(10, 6), Child = new TextBlock { Text = "Road_Network_01.bin　Topology　 4.8 MB　 2026-09-02 14:15", Classes = { "xyui-text-caption" } } };
        var row3 = new Border { Padding = new(10, 6), Child = new TextBlock { Text = "Foliage_Cluster.json　Config　　512 KB　 2026-09-03 09:30", Classes = { "xyui-text-caption" } } };

        var footer = new XYPaginationFooter(totalItems: 84, totalPages: 4);

        var panel = new StackPanel
        {
            Spacing = 4, Width = 680,
            Children =
            {
                tableHeader, row1, row2, row3,
                new Border { Height = 1, Classes = { "xyui-border-subtle" }, Margin = new(0, 4) },
                footer
            }
        };
        return WrapCard(panel, "资源视口底栏组合 · 聚合条数统计、分页容量与快速跳转");
    }
}
