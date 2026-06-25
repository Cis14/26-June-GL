# Creatio Copilot: Workflow Agents & API Skills Architecture

## Document Overview

This document provides a comprehensive analysis of the Creatio Copilot system architecture, focusing on Workflow Agents (agentic workflows), API Skills, Actions, and their execution flows. This is based on analysis of the codebase as of the current state.

---

## Table of Contents

1. [Component Definitions](#1-component-definitions)
2. [Architecture Hierarchy](#2-architecture-hierarchy)
3. [Key Classes and Their Responsibilities](#3-key-classes-and-their-responsibilities)
4. [Execution Flows](#4-execution-flows)
   - [4.1 API Skill Execution Flow](#41-api-skill-execution-flow)
   - [4.2 Workflow Agent Execution Flow](#42-workflow-agent-execution-flow)
   - [4.3 Chat Session with Tool Calls Flow](#43-chat-session-with-tool-calls-flow)
   - [4.4 ExecuteIntentUserTask Flow](#44-executeintentusertask-flow)
   - [4.5 SendUserMessage Detailed Flow (In-Depth)](./SEND_USER_MESSAGE_FLOW.md)
   - [4.6 SendUserMessage: Workflow Scenario Flow](./SEND_USER_MESSAGE_WORKFLOW_FLOW.md)
   - [4.7 Workflow Confirmation & Clarification Flow](./WORKFLOW_CONFIRMATION_FLOW.md)
   - [4.8 Copilot Session Lifecycle (Detailed)](./COPILOT_SESSION_LIFECYCLE.md)
5. [Session and Context Management](#5-session-and-context-management)
6. [Relationships and Integration Points](#6-relationships-and-integration-points)
7. [Summary](#7-summary)

---

## 1. Component Definitions

### 1.1 Intent (CopilotIntentSchema)

**Definition**: An Intent is the fundamental building block of the Copilot system, representing a specific capability or functionality that can be invoked by the LLM or user.

**Types** (CopilotIntentType enum):
- **Skill**: Standard intents that perform specific tasks (e.g., data retrieval, entity operations)
- **System**: Built-in system-level intents
- **Agent**: Container intents that orchestrate multiple skills
- **WorkflowAgent**: Agents that execute business process workflows

**Key Properties**:
```csharp
public class CopilotIntentSchema {
    public Guid UId { get; set; }
    public string Name { get; set; }
    public CopilotIntentType Type { get; set; }
    public CopilotIntentStatus Status { get; set; } // InDevelopment, Active, Deactivated
    public string Prompt { get; set; }
    public CopilotIntentBehavior Behavior { get; set; }
    public CopilotSubIntentMetaItemCollection SubIntents { get; set; }
    public CopilotActionMetaItemCollection Actions { get; set; }
    public CopilotIntentWorkflowMetaItem Workflow { get; set; } // For WorkflowAgent type
    public IntentResponseFormatJsonSchema ResponseFormatJsonSchema { get; set; }
}
```

**Intent Behavior**:
- `SkipForChat`: When true, intent is only available via API (not chat) - this is the API Skill mode
- `UsePageContext`: Whether intent uses current page context
- `UseChatHistory`: Whether intent uses conversation history
- `WorkflowOnly`: Intent only available in workflow context

### 1.2 API Skills / Workflow Skills

**API Skills** are Intents configured for standalone API execution:
- `Type = CopilotIntentType.Skill`
- `Behavior.SkipForChat = true` (API mode)
- Executed via `ICopilotApiSkillExecutor`
- Can have output parameters defined via JSON Schema

**Workflow Skills** refer to skills/sub-intents that can be called within workflow agent processes.

### 1.3 Workflow Agents (Agentic Workflows)

**Definition**: Workflow Agents are specialized intents that execute Creatio business process workflows, enabling complex multi-step orchestrations.

**Structure**:
```csharp
public class CopilotIntentSchema {
    public CopilotIntentType Type { get; set; } // = WorkflowAgent
    public CopilotIntentWorkflowMetaItem Workflow { get; set; }
}

public class CopilotIntentWorkflowMetaItem {
    public Guid WorkflowSchemaUId { get; set; } // References ProcessSchema
}
```

**Key Characteristics**:
- Execute business processes (ProcessSchema instances)
- Can perform complex, multi-step operations
- Support synchronous and asynchronous execution
- Can request user confirmation/clarification mid-execution
- Automatically unassign after completion/failure

### 1.4 Actions

**Definition**: Actions are executable operations associated with Intents, representing specific capabilities like data operations, integrations, or custom logic.

**Structure**:
```csharp
public class CopilotActionMetaItem {
    public string Name { get; set; }
    public LocalizableString Caption { get; set; }
    public LocalizableString Description { get; set; }
    public CopilotActionParameterMetaItemCollection Parameters { get; set; }
    public bool IsConfirmRequired { get; set; }
    public CopilotActionTypeSchema ActionTypeSchema { get; set; }
}

public class CopilotAction {
    protected virtual CopilotActionExecutionResult InternalRun(
        CopilotActionExecutionOptions options);
}
```

**Action Types**:
- **System Actions**: Built-in actions (e.g., RetrieveEntityData, CreateEntity)
- **Custom Actions**: User-defined actions via source code
- **Parametrized Actions**: Actions that accept parameters from the LLM

**Execution Result**:
```csharp
public class CopilotActionExecutionResult {
    public string Response { get; set; }
    public CopilotActionExecutionStatus Status { get; set; } // Completed, Failed, etc.
    public string ErrorMessage { get; set; }
    public CopilotActionResponseOptions ResponseOptions { get; set; }
}
```

### 1.5 ExecuteIntentUserTask (Call Copilot Element)

**Definition**: A special process element that executes Copilot intents from within business process workflows.

**Location**: `CrtCopilot\Schemas\ExecuteIntentUserTask\ExecuteIntentUserTask.cs`

**Purpose**: Bridges business processes and Copilot intents, allowing workflows to invoke AI capabilities.

**Key Features**:
- Executes intents synchronously or asynchronously
- Maps process parameters to intent parameters
- Handles intent results and maps outputs back to process
- Supports file/document passing
- Tracks execution status and errors

---

## 2. Architecture Hierarchy

```
┌─────────────────────────────────────────────────────────────┐
│                     Copilot System                          │
└─────────────────────────────────────────────────────────────┘
                            │
        ┌───────────────────┼───────────────────┐
        │                   │                   │
        ▼                   ▼                   ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│ Chat Session │    │  API Skills  │    │  Workflows   │
│  Executor    │    │  Executor    │    │ (Process)    │
└──────────────┘    └──────────────┘    └──────────────┘
        │                   │                   │
        └───────────────────┼───────────────────┘
                            ▼
                ┌─────────────────────┐
                │  Intent Selection   │
                │  & Tool Resolution  │
                └─────────────────────┘
                            │
        ┌───────────────────┼───────────────────┐
        ▼                   ▼                   ▼
┌──────────────┐    ┌──────────────┐    ┌──────────────┐
│   Agents     │    │   Skills     │    │  Workflow    │
│  (Intents)   │    │  (Intents)   │    │   Agents     │
└──────────────┘    └──────────────┘    └──────────────┘
        │                   │                   │
        │                   ▼                   │
        │           ┌──────────────┐            │
        │           │   Actions    │            │
        │           └──────────────┘            │
        │                                       ▼
        └──────────────────────────────►┌──────────────┐
                                        │  Business    │
                                        │  Process     │
                                        └──────────────┘
```

**Hierarchy Levels**:

1. **Executors** (Top Level)
   - `CopilotChatExecutor` - Handles chat conversations
   - `CopilotApiSkillExecutor` - Handles API skill execution
   - `CopilotEngine` - Orchestrates overall intent execution

2. **Intents** (Mid Level)
   - Agents (container intents)
   - Skills (functional intents)
   - Workflow Agents (process-executing intents)

3. **Executable Units** (Bottom Level)
   - Actions (within skills)
   - Business Processes (within workflow agents)

---

## 3. Key Classes and Their Responsibilities

### 3.1 Executor Classes

#### ICopilotEngine
**File**: `Creatio.Copilot.Abstractions\ICopilotEngine.cs`

**Responsibilities**:
- Primary orchestration interface for intent execution
- Routes execution to appropriate executor (Chat vs API)
- Manages async action completion

**Key Methods**:
```csharp
Task<CopilotIntentCallResult> ExecuteIntentAsync(
    CopilotIntentCall call, CancellationToken token);
CopilotIntentCallResult ExecuteIntent(CopilotIntentCall call);
Task<CopilotIntentCallResult> CompleteExecutingIntentAsync(
    BaseCopilotSession session, CancellationToken token);
void CompleteAction(Guid copilotSessionId, string actionInstanceUId, 
    CopilotActionExecutionResult result);
```

#### CopilotApiSkillExecutor
**File**: `CrtCopilot\Schemas\CopilotApiSkillExecutor\CopilotApiSkillExecutor.cs`

**Responsibilities**:
- Executes API-mode skills (SkipForChat = true)
- Validates permissions and feature flags
- Manages session creation for API calls
- Handles output parameter parsing
- Supports API tool calls (sub-intent/action execution)
- **Handles interaction requests**: Sends confirmation/clarification requests directly to the client via `ICopilotMsgChannelSender`.
- **Session Lifecycle**: Closes child sessions via `_sessionManager.CloseSession` upon completion.

**Execution Flow**:
1. Validate skill for API execution (permissions, status, mode)
2. Create API skill session
3. Generate prompt with parameters
4. Execute LLM completion
5. Parse and validate output parameters
6. Handle tool calls if enabled
7. Return structured result

#### CopilotChatExecutor
**File**: `CrtCopilot\Schemas\CopilotChatExecutor\CopilotChatExecutor.cs`

**Responsibilities**:
- Manages chat-based conversations
- Handles multi-turn interactions
- Coordinates tool execution (agents, skills, actions)
- Processes LLM responses with tool calls

### 3.2 Tool Execution Framework

#### IToolExecutor
**File**: `CrtCopilot\Schemas\IToolExecutor\IToolExecutor.cs`

**Interface Definition**:
```csharp
public interface IToolExecutor {
    bool IsConfirmationRequired { get; }
    List<CopilotMessage> Execute(string toolCallId, 
        Dictionary<string, object> arguments, CopilotSession session);
}
```

**Purpose**: Unified interface for executing different types of tools (intents, actions, workflows)

#### IntentToolExecutor
**File**: `CrtCopilot\Schemas\IntentToolExecutor\IntentToolExecutor.cs`

**Responsibilities**:
- Executes regular intents (Skills and Agents)
- Updates session state (CurrentIntentId, RootIntentId)
- Adds intent-specific documents to session
- Sends progress notifications to client

**Key Operations**:
```csharp
public List<CopilotMessage> Execute(string toolCallId, 
    Dictionary<string, object> arguments, CopilotSession session) {
    
    session.SetCurrentIntent(_instance.UId);
    if (_instance.Type == CopilotIntentType.Agent) {
        session.RootIntentId = _instance.UId;
    }
    
    // Add tool result message
    // Add system message with intent prompt
    // Attach documents
    
    return messages;
}
```

#### WorkflowToolExecutor
**File**: `CrtCopilot\Schemas\WorkflowToolExecutor\WorkflowToolExecutor.cs`

**Responsibilities**:
- Executes workflow agents (Type = WorkflowAgent)
- Starts business process workflows
- Manages session state during workflow execution
- Handles synchronous workflow results

**Key Operations**:
```csharp
public List<CopilotMessage> Execute(string toolCallId, 
    Dictionary<string, object> arguments, CopilotSession session) {
    
    session.SetCurrentIntent(_instance.UId);
    session.RootIntentId = _instance.UId;
    
    // Start workflow process
    var toolCalls = _workflowService.Start(session, 
        _instance.Workflow.WorkflowSchemaUId, customParameters);
    
    messages.AddRange(toolCalls);
    return messages;
}
```

#### CopilotActionToolExecutor
**File**: `CrtCopilot\Schemas\CopilotActionToolExecutor\CopilotActionToolExecutor.cs`

**Responsibilities**:
- Executes actions as tool calls
- Handles action confirmation requirements
- Manages async action execution
- Filters/shortens action responses
- Sends progress notifications

**Key Logic**:
```csharp
public List<CopilotMessage> Execute(string toolCallId, 
    Dictionary<string, object> arguments, CopilotSession session) {
    
    // Send progress notification
    CopilotAction action = _instance.CreateActionInstance(_userConnection);
    CopilotActionExecutionResult result = action.Run(options);
    
    if (result == null) {
        // Async action - return temporary message
        return AsyncFnTemporaryResultMessage;
    } else {
        // Sync action - return actual result
        return result.Response;
    }
}
```

### 3.3 Tool Context Building

#### IIntentToolExecutorFactory
**File**: `CrtCopilot\Schemas\IIntentToolExecutorFactory\IIntentToolExecutorFactory.cs`

**Responsibilities**:
- Creates appropriate tool executor for an intent
- Factory pattern for executor instantiation

**Factory Logic**:
```csharp
public IToolExecutor CreateExecutor(CopilotIntentSchema copilotIntentSchema) {
    return IsWorkflowSchema(copilotIntentSchema) ?
        (IToolExecutor) GetWorkflowExecutor(copilotIntentSchema) :
        GetIntentExecutor(copilotIntentSchema);
}
```

#### CopilotToolContextBuilder
**File**: `CrtCopilot\Schemas\CopilotToolContextBuilder\CopilotToolContextBuilder.cs`

**Responsibilities**:
- Builds tool context for LLM with available tools
- Creates tool definitions (function schemas)
- Maps tool names to executors
- Filters tools based on session state (agent/skill context)
- Handles both chat and API skill contexts

**Tool Context Structure**:
```csharp
public class CopilotToolContext {
    public IList<ToolDefinition> Tools { get; } // LLM function schemas
    public Dictionary<string, IToolExecutor> Mapping { get; } // Name -> Executor
}
```

**Building Logic**:
1. Determine available intents (agents/skills) based on session state
2. Get actions from current agent/skill
3. Create tool definitions from intents and actions
4. Map tool names to executors (IntentToolExecutor, ActionToolExecutor, WorkflowToolExecutor)
5. Return context for LLM

### 3.4 Workflow Services

#### ICopilotWorkflowService
**File**: `CrtCopilot\Schemas\CopilotWorkflowService\CopilotWorkflowService.cs`

**Responsibilities**:
- Starts workflow processes for workflow agents
- Handles workflow confirmation/clarification requests by completing business process elements via `_processExecutor.CompleteProcess`.
- Manages workflow completion hooks (`WorkflowCompletionHook`).
- Coordinates async workflow execution.
- **Session Persistance**: Updates the session state (`_sessionManager.Update`) before resuming business processes.

**Key Methods**:
```csharp
IList<CopilotMessage> Start(CopilotSession session, Guid workflowUId, 
    Dictionary<string, string> arguments);
bool HandleConfirmation(CopilotSession session, string userMessage);
bool HandleClarification(CopilotSession session, string userMessage);
```

**Workflow Completion Hook**:
- Attached to process execution events (Completed, Cancelled, Failed)
- Converts workflow results to Copilot messages
- Unassigns workflow agent from session
- Handles errors gracefully

#### ExecuteIntentUserTask
**File**: `CrtCopilot\Schemas\ExecuteIntentUserTask\ExecuteIntentUserTask.cs`

**Responsibilities**:
- Process element that executes intents from workflows
- Maps process parameters to intent parameters
- Handles intent execution results
- Supports async intent execution
- Manages file/document passing

**Execution Logic**:
```csharp
protected override bool InternalExecute(ProcessExecutingContext context) {
    var intentCall = new CopilotIntentCall {
        IntentName = intentSchema.Name,
        Parameters = ResolveParameters(),
        Documents = ResolveDocuments(),
        ProcessElementId = UId,
        RootSessionId = CopilotRootSessionId,
        RootIntentId = rootIntentId
    };
    
    CopilotIntentCallResult response = copilotEngine.ExecuteIntent(intentCall);
    
    if (response.Status == IntentCallStatus.InProgress) {
        return false; // Async execution
    }
    
    HandleResponse(response);
    FillResponseParameters(response);
    return true;
}
```

---

## 4. Execution Flows

### 4.1 API Skill Execution Flow

```
User/System
    │
    ├─► CopilotEngine.ExecuteIntent(CopilotIntentCall)
    │
    ├─► CopilotApiSkillExecutor.ExecuteAsync()
    │      │
    │      ├─► Validate skill (permissions, status, mode)
    │      ├─► Create API skill session
    │      ├─► Generate prompt with parameters
    │      ├─► Build tool context (if EnableApiToolCalls)
    │      │      ├─► Get sub-intents
    │      │      └─► Get actions
    │      ├─► Call LLM (GenAICompletionService)
    │      ├─► Process LLM response
    │      │      ├─► Handle tool calls (if any)
    │      │      │      ├─► Execute IntentToolExecutor
    │      │      │      └─► Execute ActionToolExecutor
    │      │      └─► Parse output parameters
    │      └─► Return CopilotIntentCallResult
    │
    └─► Response with structured output
```

**Key Points**:
- Standalone execution without chat session
- Single-turn interaction (request → response)
- Output parameters validated against JSON schema
- Tool calls executed synchronously within the call

### 4.2 Workflow Agent Execution Flow

```
Chat Session
    │
    ├─► User selects/LLM calls workflow agent tool
    │
    ├─► WorkflowToolExecutor.Execute()
    │      │
    │      ├─► Update session (RootIntentId = agent UId)
    │      ├─► CopilotWorkflowService.Start()
    │      │      │
    │      │      ├─► Prepare workflow parameters
    │      │      ├─► Attach completion hooks
    │      │      └─► Start process via ICopilotProcessExecutor
    │      │
    │      ├─► Workflow executes
    │      │      │
    │      │      ├─► May use ExecuteIntentUserTask elements
    │      │      │      ├─► Execute nested intents/skills
    │      │      │      └─► Return results to workflow
    │      │      │
    │      │      ├─► May request confirmation/clarification (via ToolProcessor)
    │      │      │      ├─► CopilotApiSkillExecutor adds pending interaction message
    │      │      │      ├─► Update Root and Child sessions (SessionManager.Update)
    │      │      │      ├─► Send interaction request directly to Client
    │      │      │      ├─► Set IntentCallStatus to InProgress
    │      │      │      └─► Wait for user response (Process Pauses)
    │      │      │
    │      │      └─► Complete/Fail/Cancel
    │      │
    │      └─► WorkflowCompletionHook fires
    │             │
    │             ├─► Extract workflow results
    │             ├─► Create completion messages
    │             ├─► Cleanup: Clear RootIntentId and CurrentIntentId
    │             └─► Continue chat conversation
    │
    └─► Chat continues with workflow results
```

**Workflow States**:
- **Synchronous**: Completes immediately, returns result in same turn
- **Asynchronous**: Runs in background, completion hook notifies session
- **Confirmation Required**: Pauses for user confirmation, resumes on response
- **Clarification Required**: Pauses for user input, resumes with input

### 4.3 Chat Session with Tool Calls Flow

```
User Message
    │
    ├─► CopilotChatExecutor.SendMessageAsync()
    │      │
    │      ├─► Validate session and message
    │      ├─► Build tool context
    │      │      │
    │      │      └─► CopilotToolContextBuilder
    │      │             ├─► Get agents (if no agent selected)
    │      │             ├─► Get skills (if agent selected)
    │      │             └─► Get actions (from current agent/skill)
    │      │
    │      ├─► Call LLM with tool context
    │      ├─► Process LLM response
    │      │      │
    │      │      ├─► If tool_calls present:
    │      │      │      │
    │      │      │      ├─► For each tool call:
    │      │      │      │      │
    │      │      │      │      ├─► Resolve IToolExecutor from mapping
    │      │      │      │      │      ├─► IntentToolExecutor (agent/skill)
    │      │      │      │      │      ├─► WorkflowToolExecutor (workflow agent)
    │      │      │      │      │      └─► ActionToolExecutor (action)
    │      │      │      │      │
    │      │      │      │      ├─► Check confirmation requirement
    │      │      │      │      │      ├─► If required: add confirmation message
    │      │      │      │      │      └─► Else: execute tool
    │      │      │      │      │
    │      │      │      │      └─► Add tool result to messages
    │      │      │      │
    │      │      │      └─► Call LLM again with tool results
    │      │      │
    │      │      └─► If text response: return to user
    │      │
    │      └─► Update session and return response
    │
    └─► User receives response
```

### 4.4 ExecuteIntentUserTask (Call Copilot) Flow

```
Business Process Element
    │
    ├─► ExecuteIntentUserTask.InternalExecute()
    │      │
    │      ├─► Resolve intent by UId
    │      ├─► Resolve parameters from process
    │      ├─► Resolve documents from process
    │      ├─► Create CopilotIntentCall
    │      │      ├─► IntentName
    │      │      ├─► Parameters (from process)
    │      │      ├─► Documents
    │      │      ├─► ProcessElementId (this element's UId)
    │      │      ├─► RootSessionId
    │      │      └─► RootIntentId
    │      │
    │      ├─► CopilotEngine.ExecuteIntent(call)
    │      │      │
    │      │      └─► Routes to appropriate executor
    │      │             ├─► CopilotApiSkillExecutor (API skills)
    │      │             └─► CopilotChatExecutor (other)
    │      │
    │      ├─► Receive CopilotIntentCallResult
    │      │      │
    │      │      ├─► If InProgress: return false (async)
    │      │      └─► Else: handle response
    │      │
    │      ├─► Map result parameters to process outputs
    │      └─► Return true (completed)
    │
    └─► Process continues
```

**Async Completion**:
```
Process Element (waiting)
    │
    └─► ExecuteIntentUserTask.CompleteExecuting()
           │
           ├─► Get session by CopilotSessionId
           ├─► CopilotEngine.CompleteExecutingIntentAsync(session)
           ├─► Receive completion result
           ├─► Map result parameters
           └─► Complete process element
```

---

## 5. Session and Context Management

Detailed information about the session lifecycle, storage, and hierarchy can be found in the [Copilot Session Lifecycle](./COPILOT_SESSION_LIFECYCLE.md) documentation.

### 5.1 Session Structure

```csharp
public class CopilotSession : BaseCopilotSession {
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid State { get; set; }
    public Guid? CurrentIntentId { get; set; }  // Currently active skill
    public Guid? RootIntentId { get; set; }     // Currently active agent
    public Guid? BoundedIntentId { get; set; }  // Permanently bound agent
    public Guid? RootSessionId { get; set; }    // Root session for nesting
    public Guid? ProcessElementId { get; set; } // Executing process element
    public List<CopilotMessage> Messages { get; set; }
    public List<CreatioAIDocument> Documents { get; set; }
    public CopilotSessionType Type { get; set; } // Chat, Api, Transient
}
```

### 5.2 Session State Transitions

**Agent/Skill Selection**:
```
Initial State:
  CurrentIntentId = null
  RootIntentId = null

Agent Selected:
  CurrentIntentId = AgentUId
  RootIntentId = AgentUId

Skill Selected (within agent):
  CurrentIntentId = SkillUId
  RootIntentId = AgentUId (unchanged)

Workflow Agent Started:
  CurrentIntentId = WorkflowAgentUId
  RootIntentId = WorkflowAgentUId

Workflow Completed/Cancelled (Cleanup via WorkflowCompletionHook):
  CurrentIntentId = null
  RootIntentId = null
```

### 5.3 ProcessElementId Role

**Purpose**: Tracks which process element is currently executing or waiting for completion.

**Usage Scenarios**:

1. **ExecuteIntentUserTask**:
   - Set to the process element's UId when executing
   - Used to complete async intent execution
   - Stored in CopilotIntentCall.ProcessElementId

2. **Workflow Confirmation/Clarification**:
   - Set in message.ProcessElementId
   - Used to resume process after user response
   - Passed to ICopilotProcessExecutor.CompleteProcess()

3. **Session Tracking**:
   - Links Copilot session to active process element
   - Enables process to retrieve session for completion

**Flow**:
```
ExecuteIntentUserTask
    │
    ├─► Set ProcessElementId in CopilotIntentCall
    │
    ├─► Execute intent (may be async)
    │
    └─► If async:
           │
           ├─► Store session with ProcessElementId
           │
           └─► Later: CompleteExecuting()
                  │
                  └─► Find session by Id
                      └─► Complete process by ProcessElementId
```

### 5.4 RootSessionId and Nested Sessions

**Purpose**: Supports hierarchical session relationships when workflows execute intents.

**Usage**:
- **RootSessionId**: References the parent session when creating nested sessions
- **RootIntentId**: Tracks the top-level intent in nested execution

**Scenario**:
```
Main Chat Session (Id=A)
    │
    ├─► Workflow Agent executes
    │      │
    │      └─► ExecuteIntentUserTask calls API skill
    │             │
    │             └─► New API session (Id=B, RootSessionId=A)
    │
    └─► Results flow back to main session
```

---

## 6. Relationships and Integration Points

### 6.1 Intent → Skill → Action Relationship

```
┌─────────────────────────────────────────────┐
│              Intent (Agent)                  │
│  - Type: Agent                               │
│  - SubIntents: [Skill1, Skill2, Skill3]     │
│  - Actions: [AgentAction1, AgentAction2]    │
└─────────────────────────────────────────────┘
                     │
        ┌────────────┼────────────┐
        ▼            ▼            ▼
┌──────────────┬──────────────┬──────────────┐
│  Skill1      │  Skill2      │  Skill3      │
│ - Actions:   │ - Actions:   │ - Actions:   │
│   [A1, A2]   │   [A3, A4]   │   [A5]       │
└──────────────┴──────────────┴──────────────┘
        │            │            │
        └────────────┼────────────┘
                     ▼
         ┌────────────────────────┐
         │     Actions Execute     │
         │  - Call APIs            │
         │  - Query data           │
         │  - Create records       │
         └────────────────────────┘
```

**Key Points**:
- Agents can have actions directly (agent-level actions)
- Skills inherit/add actions
- Tool context includes actions from both current agent and current skill
- Actions are the atomic units of execution

### 6.2 Workflow Agent → Business Process Integration

```
┌────────────────────────────────────────┐
│       Workflow Agent (Intent)           │
│  - Type: WorkflowAgent                  │
│  - Workflow.WorkflowSchemaUId: X        │
└────────────────────────────────────────┘
                 │
                 ▼
┌────────────────────────────────────────┐
│      Business Process (ProcessSchema)  │
│  - UId: X                               │
│  - Elements: [Start, Task1, Task2, End]│
└────────────────────────────────────────┘
                 │
       ┌─────────┼─────────┐
       ▼         ▼         ▼
┌──────────┬──────────┬──────────────────┐
│  Task1   │  Task2   │ ExecuteIntentTask│
│  (User)  │ (Script) │ (Calls Skill)    │
└──────────┴──────────┴──────────────────┘
                             │
                             ▼
                    ┌─────────────────┐
                    │  API Skill      │
                    │  Execution      │
                    └─────────────────┘
```

**Integration Points**:

1. **Workflow Invocation**:
   - WorkflowToolExecutor.Execute() → ICopilotWorkflowService.Start()
   - Parameters passed from LLM arguments to process parameters

2. **Process → Intent Calls**:
   - ExecuteIntentUserTask elements within process
   - Can call any API skill or other intent
   - Results mapped back to process variables

3. **Workflow Completion**:
   - WorkflowCompletionHook attached to process
   - Fires on Completed/Cancelled/Failed events
   - Results converted to Copilot messages
   - Agent unassigned from session

### 6.3 Tool Call Resolution

**Mapping Structure**:
```csharp
Dictionary<string, IToolExecutor> toolMapping = {
    // Agents/Skills
    ["AgentName_agent"] = IntentToolExecutor(AgentIntent),
    ["SkillName_skill"] = IntentToolExecutor(SkillIntent),
    ["WorkflowAgentName_agent"] = WorkflowToolExecutor(WorkflowIntent),
    
    // Actions
    ["CreateEntity"] = CopilotActionToolExecutor(CreateEntityAction),
    ["RetrieveData"] = CopilotActionToolExecutor(RetrieveDataAction),
    ["CustomAction"] = CopilotActionToolExecutor(CustomAction)
};
```

**Resolution Flow**:
```
LLM Tool Call
    │
    ├─► Extract tool name: "CreateEntity"
    │
    ├─► Lookup in toolMapping
    │
    ├─► Found: CopilotActionToolExecutor
    │
    ├─► Check IsConfirmationRequired
    │      ├─► If true: Add confirmation message, wait for user
    │      └─► Else: Execute immediately
    │
    └─► Execute tool, return messages
```

---

## 7. Summary

### Key Architectural Patterns

1. **Tool Abstraction**: All executable units (intents, actions, workflows) implement IToolExecutor
2. **Factory Pattern**: IntentToolExecutorFactory creates appropriate executors based on intent type
3. **Context Building**: CopilotToolContextBuilder dynamically builds available tools for LLM
4. **Hook Pattern**: Workflows use completion hooks for async result handling
5. **Session Hierarchy**: RootSessionId and ProcessElementId enable nested execution tracking

### Critical Integration Points

- **Chat ↔ API Skills**: Unified through CopilotEngine interface
- **Workflows ↔ Intents**: Bidirectional via ExecuteIntentUserTask and WorkflowToolExecutor
- **LLM ↔ Tools**: Dynamic function schema generation and executor mapping
- **Process ↔ Copilot**: Process elements can invoke and be invoked by Copilot

### Execution Modes

1. **Synchronous API Skills**: Single request-response with structured output
2. **Chat Conversations**: Multi-turn with tool calls and state management
3. **Workflow Agents**: Long-running processes with async completion
4. **Hybrid (ExecuteIntentUserTask)**: Process-driven intent execution

### Current Capabilities & Gaps

**Strengths**:
- Robust chat-based tool execution
- Workflow integration with async support
- Flexible tool context building
- Comprehensive session management

**Gaps for Full Async Action Support**:
- Direct action completion mechanism incomplete
- No action state tracking/persistence
- Limited ProcessElementId usage for actions
- Transient sessions don't support async

---

## References

### Key Files Analyzed

1. **Executors**:
   - `CrtCopilot\Schemas\CopilotApiSkillExecutor\CopilotApiSkillExecutor.cs`
   - `CrtCopilot\Schemas\CopilotChatExecutor\CopilotChatExecutor.cs`
   - `CrtCopilot\Schemas\CopilotEngine\CopilotEngine.cs`

2. **Tool Executors**:
   - `CrtCopilot\Schemas\IntentToolExecutor\IntentToolExecutor.cs`
   - `CrtCopilot\Schemas\WorkflowToolExecutor\WorkflowToolExecutor.cs`
   - `CrtCopilot\Schemas\CopilotActionToolExecutor\CopilotActionToolExecutor.cs`

3. **Infrastructure**:
   - `CrtCopilot\Schemas\IToolExecutor\IToolExecutor.cs`
   - `CrtCopilot\Schemas\IntentToolExecutorFactory\IntentToolExecutorFactory.cs`
   - `CrtCopilot\Schemas\CopilotToolContextBuilder\CopilotToolContextBuilder.cs`

4. **Workflow Integration**:
   - `CrtCopilot\Schemas\CopilotWorkflowService\CopilotWorkflowService.cs`
   - `CrtCopilot\Schemas\ExecuteIntentUserTask\ExecuteIntentUserTask.cs`

5. **Schema Definitions**:
   - `Creatio.Copilot\CopilotIntentSchema.cs`
   - `Creatio.Copilot\Metadata\CopilotIntentWorkflowMetaItem.cs`
   - `Creatio.Copilot.Abstractions\BaseCopilotSession.cs`
   - `Creatio.Copilot.Abstractions\CopilotIntentCall.cs`

---

*Document Generated: Analysis of Creatio Copilot codebase as of current state*
*Feature Flag: UseAgenticProcesses - Enables workflow agent functionality*
