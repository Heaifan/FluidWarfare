# GALLERY-UNIFY-01 Shared Gallery Layout Contract

## Ownership

`Component Catalog = content.`
`Shared Gallery Layout = semantic arrangement.`

XYUI-1, XYUI-2 and XYUI-3 must consume the same semantic layout primitives.
No batch-specific Universal shell or row implementation is allowed.

## Shared Semantic Layer

The shared Presentation layer owns these sections:

- `UniversalDocumentShell`
- `UniversalHeaderSection`
- `UniversalQuickStartSection`
- `UniversalCoreRulesSection`
- `UniversalVariantsStatesSection`
- `UniversalLiveExamplesSection`
- `UniversalCompositionSection`
- `UniversalDoDontSection`
- `UniversalTokenContractSection`

The concrete repository names may use the existing Gallery naming convention,
but they must not contain an XYUI batch number.

## Shared Row Primitives

The following primitives are the single layout truth:

- `RuleRow`: title plus description.
- `VariantRow`: name plus description plus usage.
- `StateRow`: state name plus description.
- `DoDontRow`: do text plus don't text plus rationale.
- `TokenRow`: key plus token/value plus description.
- `KeyValueRow`: generic label/value or concept/description mapping.

Catalog data may use legacy record types and adapters. Row primitives must not
own component Runtime state, commands, or content-specific business rules.

## Responsive Contract

Desktop layout:

- title column approximately 210 DIP;
- title-to-description gap 16 DIP;
- description receives the remaining width;
- title and description both wrap.

Narrow layout:

- title occupies one row;
- description occupies the next row;
- no overlap, clipping, negative margin, font-size workaround, or page spacer.

The breakpoint is a shared implementation detail. Individual pages must not
override it with a local Grid or fixed column width.

## Migration Rules

- XYUI-1 and XYUI-2 legacy document views may remain thin adapters only.
- XYUI-3 section views may remain thin adapters only.
- `XYUI1RuleRow`, `XYUI2RuleRow`, `XYUI3RuleRow` must not coexist as separate
  semantic implementations.
- Existing catalog counts, navigation routes, component previews, live example
  behavior, Runtime APIs and accepted component visuals remain unchanged.
- Round 4 `3.19` to `3.24` development remains paused until this contract is
  integrated and gated.

## Required Architecture Evidence

Tests must prove shared row usage, absence of duplicate named row
implementations, wrapping and narrow-layout structure, unchanged catalog
counts, and representative XYUI-1/2/3 document routing.
