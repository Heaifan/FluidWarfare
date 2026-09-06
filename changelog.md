# changelog

## 归档规则

- 每个自然月执行一次 changelog 归档（宪法第五十三条《月度归档》）。
- 当前自然月记录保留在本文件；已结束月份按自然月归档至 `docs/archive/changelog/changelog-YYYY-MM.md`（单一归档位置，不为每个轮次单独建文件）。
- 归档内容原则上原样迁移，保留版本、日期、验证与遗留事项；版本历史不丢失，按下方索引可定位。

## 历史归档索引

| 月份 | 归档文件 | 条目范围 |
|---|---|---|
| 2026-08 | `docs/archive/changelog/changelog-2026-08.md` | v0.2.24.0-rz ～ v0.2.28.23-rz |
| 2026-07 | `docs/archive/changelog/changelog-2026-07.md` | v0.2.1.1-rz ～ v0.2.23.0-rz |
| 2026-06 | `docs/archive/changelog/changelog-2026-06.md` | v0.1.1.5-rz ～ v0.1.8.10-fix |
| 2026-05 | `docs/archive/changelog/changelog-2026-05.md` | v0.1.1.1-rz ～ v0.1.1.4-rz |

> 历史审计注记（SHR-2026-08-R2）：7 月归档内存在 3 处同一版本号分配给两个不同轮次的历史缺陷（v0.2.16.2-rz、v0.2.17.8-rz、v0.2.20.19-fix 各 2 条，内容不同）；按归档原则保留原文不篡改，追溯时以 Commit Hash 为准。版本号与日期顺序另有 18 处非单调，为历史既成事实，登记不重排。

---

## R2-B · 月度治理修复 · 2026-09-06 22:57:34

本轮只处理 2026-08 月度健康审计暴露的 Gate Scope 与文档事实源问题，不修改产品性能主链。

- 变化：5+100 范围改为由 `XuanYu.Engine.slnx` 纳管项目根决定；保留正式项目中的 tracked 与非 ignored untracked 手写源码检查，排除外部工程、bin/obj、generated、third-party 与 vendor；归档 8 月 changelog；整理 MAP-DOC-A、EDITOR-A、MAP-A、MAP-DATA-A 与 LAYER-A 文档生命周期；同步 `file-tree.md`、`docs/docs-index.md` 与 Knowledge 索引。
- 验证：Windows PowerShell 5.1 可启动 Gate；`dsh-electron/src/main.js` 不进入 5+100；正式 XYUI 测试文件仍被检查并如实检出 105 行；Markdown/path consistency PASS；`git diff --check` PASS。Build/Test 按本轮文档与脚本范围未运行。
- 状态：工作树治理修复已准备，Index 未修改，未 Commit，未 Push。
- 遗留：`XYUI/tests/XYUI.Avalonia.Tests/Colors/RawColorGuardTests.cs` 的 105 行正式项目债务留待后续处理；高频 HitTest、Projection、Runtime Projection 与测试层 Vulkan 耦合仅登记，不在本轮修复。

---

## v0.2.28.24-rz · MAP-DATA-A-R3 POINT FEATURE FOUNDATION
MAP-DATA-A-R3（2026-08-13 15:58:51）：以 `fe00c4b` 为基线，一轮完成 `marker` Dataset 与 Map Marker Point Consumer，接入通用编辑、局部查询、Snap、Undo/Redo、Save/Reload。
- 变化：新增 Point/MapMarker 领域模型、最小 Dataset codec/binding、单击放置、自动回选择、单控制点拖动与克制 Marker overlay；Marker 支持 Region/Road/Marker Vertex/Segment Snap。
- 边界：Point 复用既有 Generic Geometry Capability、Map-level History 与 Snap Policy；不新增 Point 专属拖动/Snap 系统，不引入城镇、资源、港口、Gameplay、Topology Weld 或 Shared Node。
- 验证：Point/Generic focused 13/13；Solution 0 Warning/0 Error；Core.Tests 339/339；World.Tests 1374/1374；WarCore.Tests 22/22；ARCH-A、5+100、AXAML/XML、版本四处一致与 `git diff --check` PASS。
- 状态：R3 `IMPLEMENTED · READY FOR USER ACCEPTANCE`；未标记 CLOSED，等待 M01～M08 真机验收。
- 遗留：真机验收见 `MAP-DATA-A-R3-point-feature-foundation-acceptance.md`。
- Hash：本条所在提交。
