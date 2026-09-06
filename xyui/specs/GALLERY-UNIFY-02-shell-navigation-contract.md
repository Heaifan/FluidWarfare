# GALLERY-UNIFY-02 Shell & Module Navigation Contract

## Shared Shell

`GalleryDocumentShell` 是 XYUI-1/2/3 组件文档的唯一主纵向滚动容器。长内容必须产生可滚动 extent，且用户必须能感知滚动反馈。实现必须显式使用 `VerticalScrollBarVisibility=Visible` 与 `HorizontalScrollBarVisibility=Disabled`。

页面不得添加第二个垂直 `ScrollViewer`，不得用固定高度伪造溢出；XYUI-3 Quick Start 的横向代码滚动不构成垂直 authority。

## Module Navigation

每个模块组统一使用：

```text
XYUI-X · Module
├─ XYUI-X · 模块概览
└─ 24 个编号组件
```

Overview 使用 `Document=null` 的导航项，由 ViewModel 路由到模块 Overview View；组件数量、编号、默认组件落点保持不变。

## Regression Matrix

结构守卫必须覆盖 XYUI-1、XYUI-2、XYUI-3：唯一纵向 Scroll Authority、长文档可滚动、滚动条未禁用、Overview 存在且为首项、组件数量不变。
