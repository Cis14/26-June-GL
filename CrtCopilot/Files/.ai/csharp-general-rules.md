# C# General Rules

Purpose: Unified coding standards for all C# projects in this Core repo (net472 / netstandard / net8.0).

## 1. Fundamental Principles
- Keep the codebase consistent, readable, testable, and maintainable.
- Prefer clarity over cleverness.
- Reuse existing abstractions in Terrasoft.* / Creatio integration libraries instead of duplicating logic.
- Follow SOLID and strive for low coupling / high cohesion.
- All new code must compile on every targeted TF (net472 + net8.0 / netstandard) unless explicitly scoped.
- Treat warnings as build blockers unless explicitly listed in Directory.Build.props `WarningsNotAsErrors`.

## 2. Target Framework Strategy
- Libraries may be multi-targeted (e.g. net472 + net8.0 or netstandard2.x). Avoid TF-specific `#if` unless absolutely necessary.
- When conditional compilation is required:
    - Keep directives at the narrowest scope.
    - Prefer feature-based (API presence) over TF symbolic branching if possible.

## 3. Naming & Structure
- Projects: Keep existing Terrasoft.* conventions; new libs should align with organizational domain (avoid arbitrary suffixes).
- Namespaces mirror folder structure; avoid deep (>5 levels) nesting.
- Classes / Interfaces: PascalCase (I-prefixed interfaces only for abstractions, not DTOs).
- Methods / Local variables / Parameters: camelCase.
- Constants: PascalCase (avoid SCREAMING_SNAKE_CASE unless interop demands).
- Private fields: `_camelCase` with underscore. Use `readonly` where applicable.
- Generic type parameters: `T` or `TEntity`, `TResult`, `TOptions` (PascalCase with a clear semantic suffix).
- Async methods: suffix `Async` (including private) except explicit override of framework contract.
- Events: past tense or present participle describing occurrence (e.g., `ProcessCompleted`, `MessageReceived`).
- Delegates: end with `Handler` if representing an event signature.
- File name = single public type name.
- Avoid partial classes unless (a) auto-generated separation or (b) platform-specific segmentation.

### 3.1 DTO & Result Naming
- Request/Command: `<Verb><Noun>Request` / `<Verb><Noun>Command`.
- Response/Result objects: `<Noun>Result` if encapsulating multiple values or status metadata.

### 3.2 Method & Boolean Naming (Confluence Alignment)
- Public/protected method names SHOULD begin with an action verb: `Get`, `Set`, `Create`, `Update`, `Apply`, `Calculate`, `Build`, `Load`, `Validate`, etc.
- Avoid starting method names purely with auxiliary verbs (`Is`, `Has`, `Have`, `Can`) unless returning a boolean state; otherwise prefer `GetIs...` only if mandated for legacy interoperability (prefer concise `IsReady`).
- Boolean fields / properties / parameters MUST start with one of: `Is`, `Has`, `Have`, `Can`, `Should`, `Was`, `Use`, `Need`, or contextually appropriate past participle/predicate (e.g., `ProcessCompleted`).
- Avoid ambiguous verb-less names like `Initialized` (use `IsInitialized`).

### 3.3 Allowed Abbreviations (Curated)
Use only the following sanctioned abbreviations; avoid inventing new ones without governance:
| Full Term | Abbrev | Notes |
|-----------|--------|-------|
| Application | app | e.g. `appSettings` |
| Buffer | buf |  |
| DataReader | dr | Inside data access internals only |
| DataSource | ds | Data layer contexts |
| Default | def | e.g. `defValue` (avoid in public API) |
| Dictionary | dict | Prefer semantic name if externally visible |
| Data Transfer Object | dto | Suffix: `UserDto` |
| EntitySchemaQuery | esq | Domain-specific |
| Exception (catch variable only) | ex | Do not use for other purposes |
| Localization | lcz | Internal localization helpers |
| Message / Messaging | msg | e.g. `msgChannel` |
| Package | pkg | Internal build/package tooling |
| StringBuilder | sb | Local variables only |
| Synchronization | sync | E.g. `syncContext` |
| System | sys | Low-level system constructs |
| License | lic | Licensing contexts |
| Config | cfg | Avoid in public API (prefer full word) |
| Arguments | args | Method parameters / `Main` |
| System Under Test (tests only) | sut | Unit tests only |

If a term is not in the list, spell it out fully for clarity.

### 3.4 `var` Usage Clarification
Use `var` when:
- The right-hand side contains `new TypeName(...)` clearly identifying the static type.
- Introducing anonymous types or tuples.
- The declared type is repeated on RHS (casts / `as` patterns) and adds no clarity.
- In LINQ query chains and `foreach` loops where intent is obvious.
  Specify the explicit type when:
- The RHS expression is non-obvious (method returning an interface or base type and semantic clarity matters).
- Numeric literals where widening/precision could be misread (`int`, `float`, `decimal`).
- Maintaining API readability in public static factory helpers.

## 4. Using Directives & Namespace Layout
- Order groups: 1) System* 2) Third-party 3) Terrasoft / Creatio internal 4) Local project.
- Within each group: alphabetical.
- Do not use wildcard (`using System.*;`).
- Place `using` directives outside the namespace (default modern style) unless a specific analyzer rule dictates otherwise.
- Remove unused usings.

## 5. File & Formatting Rules
- Indentation: TABs (per stylecop.json).
- Max physical line length guideline: 120 chars (when tabs are expanded to 4 spaces).
- Max logical line length guideline: 160 chars (soft, break chained calls sensibly).
- One type per file (public). Internal private nested helper classes may remain co-located if tightly bound.
- Trailing whitespace: disallowed.
- Final newline: required.

### 5.1 Regions (Optional – High Ceremony Style)
For legacy convergence with region-centric style:
- Acceptable but NOT mandatory to group members in `#region` blocks following this order: `Enum`, `Delegates`, `Interface`, `Struct`, `Class`, `Constants`, `Fields`, `Constructors`, `Properties`, `Events`, `Methods`.
- Within each, group by access modifier order: `private`, `protected`, `internal`, `protected internal`, `public`.
- Avoid nesting regions deeply; keep code navigable. New code MAY omit regions if file is small (< ~300 lines) and logically ordered.
- Region naming pattern: `#region <MemberType>: <AccessModifier>`

### 5.2 Brace Placement
- Namespaces & types: opening brace on next line (current repository convention).
- Methods & properties: opening brace on next line (repository standard).
- Control blocks (`if`, `for`, `while`, `switch`, `using`, `lock`): opening brace on same line is PERMITTED; repository currently uses mixed Allman style—prefer consistency within a file. Do not reformat entire files solely to shift brace style.
- Always include braces even for single-line statements.

### 5.3 Collection Initializers
Single-line if the full initializer comfortably fits within line length guidance; otherwise one element per line with trailing comma optional for diff friendliness.

## 6. Class Member Ordering
1. Constants
2. Static readonly fields
3. Static fields
4. Instance readonly fields
5. Instance fields
6. Constructors (public → internal → protected → private)
7. Finalizer (rare; avoid) directly after constructors
8. Properties (auto first → logic)
9. Indexers
10. Events
11. Operators
12. Public methods
13. Protected methods
14. Internal methods
15. Private methods
16. Nested types

## 7. Coding Style
- Tabs for indentation. One statement per line.
- Always use braces `{}`.
- Expression-bodied members only for trivial one-liners (no multi-line expressions disguised as single-line).
- Prefer `var` when type is obvious from RHS or repeated; otherwise spell out for clarity.
- Avoid `dynamic` unless required for COM / plugin interaction.
- Never silently swallow exceptions; log or rethrow (`throw;`).
- Catch specific exception types; avoid broad `catch (Exception)` except at process boundary.
- Avoid `async void` (only for event handlers). Prefer `ValueTask` in high-frequency paths.
- `ConfigureAwait(false)` in library code (non-UI contexts) consistently.
- Prefer string interpolation over concatenation; avoid allocation-heavy patterns in hot loops.
- Backing fields MAY appear immediately above their property (exception to general ordering) to preserve locality.

## 8. Defensive Code & Validation
- Avoid pervasive use of `null` without guards.
- Use Terrasoft common extension methods for parameter validation:
    - Use `arg.CheckArgumentNull(nameof(arg))` for objects.
    - Use `arg.CheckArgumentNullOrEmpty(nameof(arg))` for strings/collections.
- If the above are unavailable in a specific context, use the classic pattern:
  `if (arg == null) throw new ArgumentNullException(nameof(arg));`
- Validate enums with `Enum.IsDefined(typeof(MyEnum), value)` for externally sourced data.

## 9. Immutability & Collections
- Favor immutable objects for pure data carriers (e.g. `readonly` fields, private setters).
- Expose collection interfaces (`IReadOnlyCollection<T>`, etc.).
- Do not expose internal mutable lists directly; return copies or read-only wrappers.

## 10. Async & Concurrency
- Do not block on async (`.Result`, `.Wait()`).
- Thread-safe access for shared mutable state (use `lock`, `SemaphoreSlim`, or actors where higher contention expected).
- Accept `CancellationToken` on all public async APIs performing IO or > trivial work; propagate it.
- For CPU-bound parallel loops prefer `Task.WhenAll` over manual thread creation.

## 11. Logging & Diagnostics
- Structured logging templates: `_logger.LogInformation("Processed {Count} items", count);`.
- Avoid logging sensitive values (tokens, secrets, credentials, full PII).
- Single responsibility for logging an error at boundary—avoid duplicate logs up the stack.
- Prefer semantic event naming; avoid cryptic abbreviations.

## 12. Error Handling
- Use domain-specific exception types sparingly; prefer result patterns for expected branch outcomes.
- Wrap low-level exceptions to avoid leaking infrastructure details across layers.
- Preserve stack traces (`throw;` not `throw ex;`).
- Distinguish transient vs permanent errors for potential retry logic.

## 13. Dependency Injection
- Constructor injection primary. Use optional parameters only for optional primitives with defaults—not for service dependencies.
- Avoid service locator patterns or static accessors.
- Keep constructors side-effect free (no IO or heavy allocations).

## 14. Performance Guidelines
- Measure before optimizing. Justify micro-optimizations with profiling evidence.
- Minimize allocations in hot paths; consider pooling or `ValueTask`.
- Avoid reflection per call in tight loops—cache delegates or metadata.
- Prefer `sealed` for non-extensible classes to enable devirtualization.

## 15. Serialization & DTOs
- Separate domain entities from transport contracts.
- Version tolerant changes: additive; avoid breaking field renames without migration handling.
- Date/time: use UTC (`DateTime.UtcNow`) or `DateTimeOffset` for external boundaries.

## 16. Testing (NUnit + FluentAssertions + NSubstitute / AutoFixture)
- Naming: `<Member>_State_Expectation` or `Should_<DoSomething>_When_<Condition>`.
- Deterministic tests: abstract time / randomness.
- Avoid over-mocking; treat value objects as real.
- Ensure at least one failing-path test for each complex branch.
- Use `Received(1)` only for asserting contract interactions, not incidental calls.

## 17. StyleCop / Analyzers
- Do not inline-suppress diagnostics unless justified; include rationale comment.
- Prefer repository-wide suppression (props) only when rule conflicts with accepted practice.

## 18. Internal API Boundaries
- Keep internal visibility except where cross-assembly use is required.
- Restrict `InternalsVisibleTo` to test assemblies.

## 19. Obsolescence & Versioning
- `[Obsolete]` with actionable replacement guidance; mark error only when removal is imminent.
- Provide at least one release grace period for non-critical deprecations.

## 20. Security & Safety
- Input validation at boundaries; fail fast.
- Cryptography: no MD5/SHA1—use modern APIs (e.g., `RNGCryptoServiceProvider` or `RandomNumberGenerator`).
- Clear sensitive buffers (`Array.Clear`) after use when holding credentials.
- Avoid command injection—use parameterized queries / proper escaping.

## 21. Documentation
- XML docs for public surface: describe intent, invariants, side effects.
- Use `<remarks>` for algorithmic or performance considerations.
- Link to design documents (internal) where complexity is non-obvious.
- Do not leave comments inside methods — the code should be self-explanatory.

## 22. Branch & Review Expectations
- PR includes: green build, analyzer compliance, adequate tests.
- Large refactors: add characterization tests first.
- Review checklist: nullability, async correctness, layering, logging, error semantics, test completeness.

## 23. Observability
- Key operations instrumented with structured logs.
- Consider future OpenTelemetry integration; design log scopes to support trace correlation.

## 24. Thread Safety
- Clearly document thread-safe vs non-thread-safe types in XML documentation summary.
- Use immutability or confinement (actors) to avoid coarse locks where possible.

## 25. API Design Guidelines (Summary)
- Favor methods over properties for operations with side effects or expensive computation.
- Do not use exceptions for normal control flow.
- Prefer explicitness over magic: avoid hidden reflection-based registrations without clear contracts.

## 26. Example Ordering Template
```csharp
public sealed class SampleService : ISampleService
{
	// Constants
	public const int DefaultBatchSize = 100;

	// Static readonly
	private static readonly ActivitySource Activity = new("Terrasoft.SampleService");

	// Instance readonly fields
	private readonly IClock _clock;
	private readonly ILogger<SampleService> _logger;

	// Constructors
	public SampleService(IClock clock, ILogger<SampleService> logger)
	{
		clock.CheckArgumentNull(nameof(clock));
		logger.CheckArgumentNull(nameof(logger));
		_clock = clock;
		_logger = logger;
	}

	// Properties
	public DateTime UtcNow => _clock.UtcNow;

	// Public methods
	public async Task<Result<string>> ExecuteAsync(string input, CancellationToken ct)
	{
		input.CheckArgumentNull(nameof(input));
		using (var activity = Activity.StartActivity("Execute")) {
			_logger.LogDebug("Executing with input length {Length}", input.Length);
			await Task.Yield();
			return Result.Success(input.ToUpperInvariant());
		}
	}

	// Private methods
	private static bool IsValid(string value) => !string.IsNullOrWhiteSpace(value);
}
```

### 26.1 Example – Query Object Formatting (Confluence Alignment)
```csharp
// Select with join and top
var select = new Select(userConnection).Top(1)
	.Column("Country", "Name")
	.From("City")
	.InnerJoin("Country").On("Country", "Id").IsEqual("City", "CountryId") as Select;

// Logical grouping with OpenBlock/CloseBlock
var complex = new Select(userConnection)
	.Column("Name")
	.From("Country")
	.Where()
	.OpenBlock()
		.OpenBlock(Column.Parameter("TestValue1")).IsNull()
		.And(Column.Parameter("TestValue2")).IsNull()
		.And(Column.Parameter("TestValue3")).IsNull()
	.CloseBlock()
	.Or("Id").IsNull()
	.CloseBlock() as Select;
```

---
Owned by: Platform Architecture Team
Last updated: 2025-09-24 (Confluence alignment additions)
