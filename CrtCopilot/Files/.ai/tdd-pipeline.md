# TDD Pipeline & Development Guidelines

This document defines the mandatory pipeline for any non-trivial code changes, emphasizing a Test-Driven Development (TDD) approach, clear planning, and rigorous quality gates.

## 1. When this pipeline applies
This pipeline MUST be followed for:
- New feature implementation
- Bug fixing (except for trivial typos or obvious single-line logic errors)
- Refactoring of existing logic
- Architecture changes or new component design
- Integrations (API, external services)
- Data model changes (new entities, fields, relationships)
- API design and implementation

## 2. Step-by-Step TDD Pipeline
The following steps must be executed in order:

### Step 1: Implementation Plan (Business Logic & Architecture)
- **Objective:** Align on the approach and architecture before writing code.
- **Expected outputs/artifacts:** 
  - A structured plan outlining the logic flow.
  - Identification of affected components, classes, and interfaces.
  - Data schema changes (if any).
  - External dependencies or service integrations.
- **Gate / exit criteria:** Plan is reviewed and approved (or documented if working autonomously).

### Step 2: Write Tests First
- **Objective:** Define expectations through executable code.
- **Expected outputs/artifacts:** 
  - Unit or integration tests (using NUnit + FluentAssertions).
  - List of test cases covering happy paths and edge cases.
- **Workflow:**
  - **Check Existing:** Determine if a test file for the target class already exists in the test project.
  - **Update or Create:** 
    - If it exists, append/integrate new test cases into the existing file.
    - If it does not exist, create a new test file in the appropriate location as defined by the [Project structure](./repository/PROJECT_STRUCTURE.md).
- **Gate / exit criteria:** Tests are written and fail as expected (reproducing the bug or missing feature).

### Step 3: Implement Code
- **Objective:** Satisfy the requirements defined by the tests.
- **Expected outputs/artifacts:** 
  - Production code implementing the business logic.
  - Updated/new classes, methods, and configurations.
- **Gate / exit criteria:** Code is complete enough to potentially pass the tests.

### Step 4: Run Tests
- **Objective:** Verify the implementation.
- **Expected outputs/artifacts:** 
  - Test execution report (stdout/stderr).
- **Gate / exit criteria:** All tests in the relevant scope are executed.

### Step 5: Fix Failures & Re-run
- **Objective:** Reach a stable, passing state.
- **Expected outputs/artifacts:** 
  - Refined code and/or tests.
- **Gate / exit criteria:** All tests pass consistently (Green).

### Step 6: Final Review & Best Practices
- **Objective:** Ensure code quality and adherence to standards.
- **Expected outputs/artifacts:** 
  - Refactored code (if needed).
  - Documentation updates.
- **Gate / exit criteria:** Code satisfies "csharp-general-rules.md", no lint issues, and logic is clean.

## 3. Quality Gates
Before final submission, the following must be satisfied:
- [ ] **All tests passing:** Both new and existing regression tests must be green.
- [ ] **No lint/type issues:** Code must be free of compiler warnings and style violations.
- [ ] **No TODOs:** All TODO comments must be resolved or explicitly approved as future tasks.
- [ ] **Edge cases covered:** Logic handles nulls, empty collections, exceptions, and boundary values.
- [ ] **Security + Performance:** Reviewed for sensitive data handling, efficient loops, and memory usage.

## 4. Change Control / ADR (Architectural Decision Records)
If a change impacts architecture, public interfaces, external dependencies, or data schema:
- **Requirement:** Update or create an ADR entry in `docs/adr/` (or the project-specified ADR location).
- **Content:** Include Context, Decision, and Consequences (positive and negative).

## 5. Communication Protocol
- **Plan First:** The agent must present the implementation plan BEFORE starting the coding phase.
- **Change Summary:** Upon completion, summarize WHAT was changed, WHERE it was changed, and WHY.
- **Test Instructions:** Explicitly list the command or method to run the new/affected tests locally.

## 6. Mode Switches
The user can request the agent to operate in specific modes:

1. **[TDD-STRICT]**: Mandatory TDD with zero tolerance for skipping test-first steps. Best for critical core logic.
2. **[FAST-PROTOTYPE]**: Prioritizes speed and implementation over tests initially, with tests added at the end. Use for POCs only.
3. **[REFACTOR-SAFE]**: Focuses heavily on regression testing and ADR documentation to ensure no breaking changes in legacy code.
