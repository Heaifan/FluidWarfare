# XYUI-3 Round 1 Runtime Audit

日期：2026-09-06

范围：CODEX Runtime / Rules & Architecture Owner，覆盖 3.01 MenuBar、3.02 Menu、3.03 ContextMenu、3.04 SubMenu、3.05 NavigationMenu、3.06 Sidebar。

## 审计结论

- 六个控件已收敛到共享 Menu / Navigation Runtime 基础，不接受 Gallery-only 状态替身。
- Menu 支持 Normal、Icon、Shortcut、Checked、Radio、Disabled、Separator、SubMenu，以及打开/关闭、hover switch、outside close、Esc、Enter、Up/Down、Left/Right、焦点恢复和嵌套 Pointer Transition。
- ContextMenu 持有真实 `Control` Target，并复用 `XYMenu`。
- NavigationMenu 与 Sidebar 共享 `XYNavigationState`；Sidebar 的 Primary Navigation、Context Region、Sticky Footer 和用户宽度恢复均由 Runtime 建模。
- 未修改 XYUI-2，未实现 3.07+；Gemini Gallery / Presentation 已按 handoff 合流，Runtime 所有权边界保持不变。
- 本轮修复 Sidebar 折叠态 `XYNavigationRail` 图标缺失；Rail 继续保留选中态，展开后恢复用户宽度，Canonical Rail Width 为 64 DIP。
- 3.03、3.05、3.06 Gallery Quick Start 已对齐真实 XAML Consumer Contract；3.05 Badge / Status 使用真实 Runtime 链路，移除过时 GAP；3.06 不要求消费者调用 `Build()`。

## 证据边界

- 根 solution 与 `XYUI.Avalonia.slnx` 编译：E 盘本机 SDK `E:\MyApp\sdk-dotnet\dotnet.exe`，0 warning / 0 error。
- 全量测试：Core 339、WarCore 22、World 1286、XYUI 502，合计 2149 passed / 0 failed / 0 skipped。
- ARCH-A（含 5+100）：PASS；`git diff --check`：PASS。
- 本轮 Runtime 回归测试：32/32 PASS；Gemini handoff：`PRESENTATION FIX IMPLEMENTED`、`HANDOFF READY FOR CODEX`。
- `D:\MyApp\sdk-dotnet\dotnet.exe` 在本机不存在；按仓库现有本机回退约定使用 E 盘 SDK。正式门禁仍需记录实际执行结果。
- 本文件不宣称用户视觉验收；当前状态为 `TECHNICAL PASS · READY FOR USER VISUAL RE-REVIEW`。

## 留待用户

- 用户进行 Gallery 视觉、键鼠真实窗口、触摸/真机和边缘定位人工验收。
