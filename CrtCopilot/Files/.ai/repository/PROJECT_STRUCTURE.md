### Copilot Architecture

The `CopilotEngine` is the central entry point of the Copilot system. It acts as an orchestrator, delegating the actual execution logic to specialized executors depending on the interaction mode (Chat or API).

#### Core Orchestration and Execution

1.  **CopilotEngine**: The main facade. It dispatches calls to `CopilotChatExecutor` for chat-based interactions and `CopilotApiSkillExecutor` for direct API-based skill executions.
2.  **CopilotChatExecutor**: Handles the stateful chat workflow, including message history, tool calls, and real-time client communication via message channels.
3.  **CopilotApiSkillExecutor**: Handles the execution of skills via API, focusing on parameter mapping and returning structured results.

#### Core Abstractions and Services

1.  **ICopilotSessionManager**: Manages the lifecycle of Copilot sessions, including persistence and retrieval from the database.
2.  **IGenAICompletionServiceProxy**: A proxy service for interacting with the Generative AI (LLM) provider.
3.  **ICopilotToolProcessor**: Manages the execution of tools (actions) triggered by the LLM and processes their results.
4.  **ICopilotToolContextBuilder**: Constructs the `CopilotToolContext` containing available actions and intents for the LLM.
5.  **IIntentPromptBuilder**: Generates system prompts based on intent metadata and parameters.
6.  **ICopilotContextBuilder**: Builds contextual information (e.g., page data, global constants) to be included in the LLM request.
7.  **ICopilotDocumentManager**: Manages documents and file attachments associated with a session or intent.
8.  **ICopilotLinkValidator**: Validates, cleans up, and secures hyperlinks in the AI's responses.
9.  **ICopilotMsgChannelSender**: Handles real-time communication with the client UI via WebSocket/Message channels.
10. **ICopilotSessionResponseDispatcher**: Dispatches session-related events and updates asynchronously.
11. **ICopilotIntentsStorage**: Provides access to Copilot intent schemas (agents and skills) and their metadata.
12. **ILlmModelResolver**: Resolves which LLM model should be used for a specific intent or session.
13. **ICopilotRequestLogger**: Logs AI requests, responses, and token usage for auditing and monitoring.
14. **ICopilotHyperlinkUtils**: Utility for marking, validating, and enhancing links within the Copilot context.

#### Workflow

- **Message Processing**: `CopilotEngine` receives user input and routes it to `CopilotChatExecutor.SendUserMessage`.
- **Session Handling**: The executor retrieves the current `CopilotSession` via `ICopilotSessionManager`.
- **Prompt Construction**: The executor assembles the LLM request by combining:
    - Session history (messages).
    - System prompts (from `IIntentPromptBuilder`).
    - Application context (from `ICopilotContextBuilder`).
    - Attached documents (from `ICopilotDocumentManager`).
    - Tool definitions (from `ICopilotToolContextBuilder`).
- **LLM Interaction**: The request is sent to the LLM via `IGenAICompletionServiceProxy`.
- **Response Handling**:
    - If the LLM returns text, it's validated for links (via `ICopilotLinkValidator`) and sent to the client (via `ICopilotMsgChannelSender`).
    - If the LLM requests tool calls, `ICopilotToolProcessor` executes the corresponding actions, and the results are added back to the session for a follow-up LLM call.
- **Completion**: The final state is persisted, and the response is dispatched via `ICopilotSessionResponseDispatcher`.

### Guidelines for Creating New C# Files (Schemas)

When adding new logic to the `Pkg\CrtCopilot` package, follow these steps to ensure consistency and compatibility with the Creatio metadata system.

#### 1. Create a Dedicated Folder
Each new schema (interface or class) must reside in its own folder under `Pkg\CrtCopilot\Schemas\`. The folder name should match the schema name.
- Example: `Pkg\CrtCopilot\Schemas\MyNewService\`

#### 2. Create the C# File
Place the `.cs` file inside the created folder. Ensure the namespace matches the project structure (typically `Creatio.Copilot`). Use appropriate attributes like `[DefaultBinding]` for dependency injection.

#### 3. Create Required JSON Metadata Files
Every schema requires three JSON files in its folder to be recognized by the Creatio environment:
- **descriptor.json**: Contains the UId, Name, and ManagerName (`SourceCodeSchemaManager`).
    - `UId`: Must be a unique GUID. Ensure it's newly generated for each schema.
    - `ModifiedOnUtc`: Must always be the current UTC datetime value (e.g., `\/Date(1763456928000)\/`).
- **metadata.json**: Defines the schema structure and localizable strings.
    - `UId`: Must match the `UId` specified in `descriptor.json`.
- **properties.json**: Defines versioning and optional properties.

You can use existing schemas (e.g., `CopilotLinkValidator`) as templates for these files.

#### 4. Create a Corresponding Test Fixture
All new logic must be covered by unit tests.
- Create a new test class in the `UnitTests\CrtCopilot.UnitTests\` project.
- Use NUnit and NSubstitute (or the project's preferred mocking framework).
- File naming convention: `{SchemaName}Tests.cs`.
- Ensure tests are marked with the `[TestFixture]` attribute and relevant categories (e.g., `[Category("PreCommit")]`).

#### 5. SVN/VCS Integration
Mark all new files (including JSON metadata) as added to the version control system.