# Finnet Project Architecture Overview

## 1. Project Structure

```
Chapter-1-initial-architecture/
├─ Docs/                 # Architecture decision records & documentation
├─ Src/
│  ├─ Fitnet.AppHost/    # ASP.NET Aspire host (executable)
│  ├─ Fitnet/            # Core application library
│  │  ├─ Contracts/      # Bounded context – contract domain
│  │  │  ├─ Data/       # Domain entities & EF Core mappings
│  │  │  ├─ PrepareContract/
│  │  │  ├─ SignContract/
│  │  │  └─ ...
│  │  ├─ Offers/        # Bounded context – offers domain
│  │  ├─ Passes/        # Bounded context – passes domain
│  │  ├─ Reports/       # Reporting (read‑only) modules
│  │  ├─ Common/        # Cross‑cutting utilities (events, validation, docs, error handling, etc.)
│  │  ├─ GlobalUsings.cs
│  │  └─ Program.cs
│  └─ Fitnet.slnx
├─ Fitnet.IntegrationTests/
├─ Fitnet.UnitTests/
└─ Fitnet.ArchitectureTests/
```

> **Fitnet.AppHost** is an **ASP.NET Aspire** host that wires together the application, configures dependency injection, registers the in‑memory event bus, and starts the web server.

## 2. Layered Architecture (Clean Architecture)

| Layer | Responsibility | Typical Classes / Packages | Dependencies |
|-------|----------------|---------------------------|--------------|
| **Presentation** | HTTP API, Swagger, request/response models | `ApiPaths.cs`, `ContractsEndpoints.cs`, `OffersEndpoints.cs`, `PassesEndpoints.cs`, `ReportsEndpoints.cs` | Application |
| **Application** | Use‑cases, command/query handling, orchestration | MediatR handlers (`PrepareContractEndpoint`, `SignContractEndpoint`, etc.), validators (`RequestValidationsExtensions.cs`) | Domain, Infrastructure |
| **Domain** | Core business logic, aggregates, value objects, domain events | `Contract`, `Offer`, `Pass`, business rule validators (`ContractCanBePreparedOnlyForAdultRule`, etc.) | Common, Infrastructure (read‑only) |
| **Infrastructure** | Persistence (EF Core), external integrations, event bus | `ContractsPersistence.cs`, `OffersPersistence.cs`, `PassesPersistence.cs`, `EventBusModule.cs`, `InMemoryEventBus.cs` | Domain, Common |
| **Common** | Cross‑cutting concerns (validation, clock, error handling, docs, event bus abstraction) | `BusinessRuleValidator`, `IEventBus`, `GlobalExceptionHandler`, `ClockModule` | — |

> The **Dependency Rule** is enforced: each layer can only depend on the layer directly below it. This is verified by the *Architecture Tests* located in `Fitnet.ArchitectureTests`.

## 3. Bounded Contexts & Modules

| Context | Key Entities | API Endpoints | Integration Events | Persistence |
|---------|--------------|--------------|--------------------|-------------|
| **Contracts** | `Contract` | `/api/contracts/prepare`, `/api/contracts/sign` | `ContractSignedEvent` | PostgreSQL table *Contracts* (single schema, but can be isolated with separate DB schema via decision log `0004-use-separate-database-schemas.adoc`)
| **Offers** | `Offer` | `/api/offers/prepare`, `/api/offers/sign` | `OfferSignedEvent` | PostgreSQL table *Offers*
| **Passes** | `Pass` | `/api/passes/prepare`, `/api/passes/mark-expired`, `/api/passes/get-all` | `PassExpiredEvent`, `PassRegisteredEvent` | PostgreSQL table *Passes*
| **Reports** | Read‑only DTOs | `/api/reports/new-pass-registrations` | — | PostgreSQL read‑only view / aggregated queries |

All contexts share the same **EF Core DbContext** (`ContractsPersistence`, `OffersPersistence`, `PassesPersistence`) but each module has its own DbContext class for clarity and to support the decision to use separate database schemas.

## 4. Domain‑Driven Design Patterns

* **Aggregates** – `Contract`, `Offer`, `Pass` are aggregate roots exposing only public methods for state changes (`Prepare`, `Sign`, `MarkExpired`).
* **Business Rules** – Implemented as *value objects* in `BusinessRulesEngine`. Validation is performed via `BusinessRuleValidator.Validate`. This keeps entities thin and rules reusable.
* **Domain Events** – Entities raise events (`ContractSignedEvent`, `PassExpiredEvent`) which are published through `IEventBus`. In‑memory bus is used for dev/testing; a production implementation could plug in a message broker.
* **Repositories** – EF Core provides repository‑like persistence; each DbContext exposes `DbSet<T>` for aggregates. Tests use the same infrastructure for integration tests.

## 5. Cross‑Cutting Concerns (Common)

- **Validation** – `FluentValidation` integrated with MediatR via `RequestValidationApiFilter`.
- **Error Handling** – Global exception handler (`GlobalExceptionHandler`) standardizes API error responses.
- **Clock** – `ClockModule` provides abstraction for time, enabling deterministic tests.
- **Event Bus** – `IEventBus` abstraction with `InMemoryEventBus` implementation. Extension methods expose `RegisterEventHandlers`.
- **Swagger** – `SwaggerDocumentationExtensions` adds Swagger docs for each module.

## 6. Architecture Decision Records (ADR)

The `Docs/ArchitectureDecisionLog` folder contains the ADRs that guided the design:

- `0001-record-architecture-decisions.adoc` – The foundation for capturing decisions.
- `0002-use-one-project.adoc` – Chose a single monolithic project to simplify deployment.
- `0003-use-modules.adoc` – Implemented modular boundaries inside the same project.
- `0004-use-separate-database-schemas.adoc` – Each context can be isolated in its own DB schema.
- `0005-use-vertical-slices.adoc` – Layered architecture aligns with vertical slices per bounded context.
- `0006-use-docker.adoc` – Docker Compose for local dev.
- `0007-use-in-memory-event-bus.adoc` – In‑memory bus for dev and tests.
- `0010-select-integration-style-between-passes-and-offers-modules.adoc` – Decides on integration event style.

These records should be consulted when extending or refactoring the system.

## 7. Testing Strategy

| Test Type | Purpose | Implementation |
|-----------|---------|----------------|
| **Unit** | Verify domain logic & business rules | `Fitnet.UnitTests` – uses xUnit and Moq.
| **Integration** | Validate persistence, API contracts, and event flow | `Fitnet.IntegrationTests` – uses Testcontainers for PostgreSQL.
| **Architecture** | Ensure layering and dependencies conform to the design | `Fitnet.ArchitectureTests` – uses ArchUnitNET or similar.

Test projects reference the same project structure and can be run via `dotnet test`.

## 8. Deployment & Runtime

- Built on **.NET 10 (preview)** with **ASP.NET Aspire**.
- Docker Compose (`docker-compose.yml` in the root) sets up PostgreSQL and runs the Aspire host.
- `Fitnet.AppHost` configures the in‑memory event bus and starts the web server.
- In production, the Aspire host can be replaced with a container that connects to a managed PostgreSQL and a message broker.

## 9. Future Considerations

- **Microservice Split** – The modular structure supports a future migration to separate microservices.
- **Event Bus Replacement** – Replace `InMemoryEventBus` with a durable broker (e.g., NATS, Kafka) without changing domain code.
- **CQRS Enhancements** – Split read‑side queries further for scalability.
- **Monitoring & Observability** – Integrate OpenTelemetry for tracing and metrics.

---

> **Prepared by**: Evolutionary Architecture Team
> **Date**: 2026-02-16
