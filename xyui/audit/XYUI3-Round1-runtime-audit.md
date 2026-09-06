# XYUI-3 Round 1 Runtime Audit

日期：2026-09-06

范围：CODEX Runtime / Rules & Architecture Owner，覆盖 3.01 MenuBar、3.02 Menu、3.03 ContextMenu、3.04 SubMenu、3.05 NavigationMenu、3.06 Sidebar。

## 审计结论

- 六个控件已收敛到共享 Menu / Navigation Runtime 基础，不接受 Gallery-only 状态替身。
- Menu 支持 Normal、Icon、Shortcut、Checked、Radio、Disabled、Separator、SubMenu，以及打开/关闭、hover switch、outside close、Esc、Enter、Up/Down、Left/Right、焦点恢复和嵌套 Pointer Transition。
- ContextMenu 持有真实 `Control` Target，并复用 `XYMenu`。
- NavigationMenu 与 Sidebar 共享 `XYNavigationState`；Sidebar 的 Primary Navigation、Context Region、Sticky Footer 和用户宽度恢复均由 Runtime 建模。
- 未修改 XYUI-2，未实现 3.07+，未接管 Gemini Gallery / Presentation 文件。

## 证据边界

- 源码编译：E 盘本机 SDK `E:\MyApp\sdk-dotnet\dotnet.exe`，0 warning / 0 error。
- 定向 Runtime/Interaction/Structure 测试：26 passed / 0 failed / 0 skipped。
- `D:\MyApp\sdk-dotnet\dotnet.exe` 在本机不存在；按仓库现有本机回退约定使用 E 盘 SDK。正式门禁仍需记录实际执行结果。
- 本文件不宣称用户视觉验收；Gallery 并行变化由 Gemini 负责。

## 留待用户

- 合流后进行 Gallery 视觉、键鼠真实窗口、触摸/真机和边缘定位人工验收。
