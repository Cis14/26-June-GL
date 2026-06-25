# AI Development Guidelines

This folder contains development guidelines split into categorized files for better organization and maintainability.

## Required Guidelines
The following documents define mandatory standards for all new or changed backend code (read ALL before implementing changes):
- C# general coding rules: [csharp-general-rules.md](.ai/csharp-general-rules.md)
- TDD Development Pipeline: [tdd-pipeline.md](.ai/tdd-pipeline.md)

**ALWAYS** use the provided [Project structure] when creating new files, folders or modules and
use the provided [Project structure] when reading files, folders or modules.
[Project structure](./.ai/repository/PROJECT_STRUCTURE.md)

**ALWAYS** read the following documents and follow by their links for understanding user request.

## Required Guidelines
The following documents define the architecture of the main functionality:
[Creatio Copilot: Workflow Agents & API Skills Architecture](./.ai/architecture/WORKFLOW_AGENTS_ARCHITECTURE.md)
[Creatio Copilot: Send user message flow](./.ai/architecture/SEND_USER_MESSAGE_FLOW.md)
[Copilot Session Lifecycle](./.ai/architecture/COPILOT_SESSION_LIFECYCLE.md)
[Workflow Confirmation and Clarification Flow](./.ai/architecture/WORKFLOW_CONFIRMATION_FLOW.md)
[SendUserMessage: Workflow Scenario Flow](./.ai/architecture/SEND_USER_MESSAGE_WORKFLOW_FLOW.md)

## Usage
Before performing ANY automated or manual change, you MUST read (or re-skim if already familiar) the required guidelines above. When conflicts arise between code comments and these documents, treat the documents as the source of truth and create a follow-up ticket to reconcile discrepancies.

## Per-Project Agent Documentation
**ALWAYS** read the AGENTS.md file of the project before making changes. It may contain project-specific overrides or
additions to the core guidelines.
