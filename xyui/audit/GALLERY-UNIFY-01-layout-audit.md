# GALLERY-UNIFY-01 Gallery Layout Audit

## Scope

This is a Section / Row infrastructure audit for XYUI-1, XYUI-2 and XYUI-3.
It does not audit individual component content or Runtime behavior.
Round 4 component work remains paused.

## Current Architecture Matrix

| Semantic Role | XYUI-1 implementation | XYUI-2 implementation | XYUI-3 implementation | Action |
|---|---|---|---|---|
| Header | `XYUI1ComponentDocumentView` header block | Reuses XYUI-1 document view | `XYUI3HeaderSection` | MERGE INTO SHARED |
| Developer Quick Start | `XYUI1ComponentDocumentView` code / preview Grid | Reuses XYUI-1 document view | `XYUI3QuickStartSection` | MERGE INTO SHARED |
| Core Rules | Inline `Grid ColumnDefinitions="120,*"` | Same legacy inline template | `XYUI3RuleRow` with `210` DIP title | MERGE INTO SHARED |
| Variants / Anatomy | Inline `Grid ColumnDefinitions="120,*,*"` | Same legacy inline template | `180,*,*` Grid | MERGE INTO SHARED |
| States | Inline `Auto,12,*` Grid | Same legacy inline template | `180,*` Grid | MERGE INTO SHARED |
| Live Examples | `LiveExamplesHost` in legacy document view | Reuses legacy document view | `XYUI3LiveExamplesSection` | LEGACY ADAPTER |
| Composition / Advanced | Content-specific legacy host or catalog content | Content-specific legacy host or catalog content | `XYUI3CompositionSection` | MERGE INTO SHARED |
| Do / Don't | Catalog data only; no shared row | Catalog data only; no shared row | `XYUI3DoDontSection` with two-column Grid | MERGE INTO SHARED |
| Foundation / Token Contract | `Auto,12,Auto,12,*` Foundation and state rows | Reuses legacy data shapes | `200,140,*` Token Grid | MERGE INTO SHARED |
| Navigation / Catalog | `XYUI1DocumentationViewModel` shared shell | Same navigation shell | Same catalog route, custom document shell | KEEP |

## Duplicate Implementation Finding

There is one named duplicate family member today:

- `XYUI3RuleRow` exists in `Views/XYUI3CoreRulesSection.axaml.cs`.
- `XYUI1RuleRow` does not exist as a named type.
- `XYUI2RuleRow` does not exist as a named type.

However, XYUI-1 and XYUI-2 currently contain equivalent inline row layouts in
`Views/XYUI1ComponentDocumentView.axaml`, so the absence of a class name does
not mean the layout is unique. The migration must remove the inline layout
truth from the legacy view and make the shared row primitives the only layout
owners.

## Representative Regression Pages

- XYUI-1: `XYUI-1-19` (`XYTooltip`), selected because it has long usage,
  foundation and interaction descriptions.
- XYUI-2: `XYUI-2-13` (`XYSelect`), required long-title regression page.
- XYUI-3: `XYUI-3-3.18` (`XYCommandPalette`), required command-palette page.

## Disposition

The shared Gallery layer owns semantic arrangement. Catalogs continue to own
content and data only. Legacy document views remain adapters while migration
is in progress; they must not retain a second title-column truth.

## Handoff Status

`GALLERY UNIFICATION CONTRACT HANDOFF` sent to Gemini after this audit.
Codex will add architecture regression tests after Presentation migration.
