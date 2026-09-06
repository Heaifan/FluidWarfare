# GALLERY-UNIFY-02 Shell & Module Navigation Audit

## Scope

本轮只审计共享 Gallery Document Shell 与 XYUI-1/2/3 模块概览导航，不修改 Runtime、组件视觉或 3.19+。

## Root Cause

- `GalleryDocumentShell` 原先只禁用横向滚动，未显式声明纵向滚动反馈；主题默认行为可能使长文档的滚动条不可感知。
- `XYUI1DocumentationViewModel.XYUI3Items` 原先直接映射 24 个组件，缺少 Overview model、Overview view 与空 Document 路由。

## Recovered Contract

- `GalleryDocumentShell` 是组件文档唯一主纵向 Scroll Authority。
- `VerticalScrollBarVisibility=Visible`，`HorizontalScrollBarVisibility=Disabled`。
- XYUI-1、XYUI-2、XYUI-3 的模块列表首项均为 `XYUI-X` Overview，随后为 24 个组件。
- XYUI-3 Overview 复用既有模块概览模式，不改变 24 个组件目录、编号或默认末项落点。

## Evidence

- `GalleryLayoutArchitectureTests`：12/12 PASS。
- 结构守卫覆盖唯一滚动 authority、长文档 Offset 推进、滚动条未禁用、三模块 Overview 首项与组件数量。
- Gemini Presentation 修改未触及 `xyui/avalonia/src/XYUI.Avalonia/` Runtime。

## Boundary

自动测试只证明结构与合同；长页滚动反馈、模块概览可读性仍需用户执行 Gallery Shell Smoke Review。
