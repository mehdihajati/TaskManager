# TaskManager API

A production-grade **Task & Project Management REST API** built with **.NET 10**, following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.

This project was built as a deep dive into designing a real-world backend system from the ground up — with a strict focus on rich domain modeling, aggregate boundaries, and separation of concerns rather than tutorial-style CRUD.

---

## ✨ Highlights

- **Clean Architecture** with a dedicated **Bootstrapper** layer to keep the API project fully decoupled from Infrastructure
- **Rich Domain Models** — entities protect their own state through behavior, not public setters
- **Role-based authorization at the domain level** — `Owner`, `Manager`, `Member`, `Viewer` permissions enforced inside aggregates
- **Soft delete** pattern built into `BaseEntity`, supporting GDPR-style data retention
- **`Guid.CreateVersion7()`** (.NET 10) for sequential, client-generated, non-guessable identifiers
- **DateTimeOffset (UTC)** everywhere — built for a multi-timezone user base from day one
- Designed with extensibility in mind (e.g. fixed roles now, pluggable custom-role/permission system later)

---

## 🏗️ Architecture

```
src/
├── TaskManager.Domain          → Entities, Enums, Repository interfaces. Zero external dependencies.
├── TaskManager.Application     → Use cases, CQRS (MediatR), DTOs, Validators. Depends only on Domain.
├── TaskManager.Infrastructure  → EF Core, repository implementations. Depends on Application + Domain.
├── TaskManager.Bootstrapper    → Wires Application + Infrastructure together for DI.
└── TaskManager.API             → Controllers, Middleware. Depends only on Application + Bootstrapper.

tests/
└── TaskManager.Tests
```

**Dependency rule:**

```
Domain          ← nothing
Application     ← Domain
Infrastructure  ← Application + Domain
Bootstrapper    ← Application + Infrastructure
API             ← Application + Bootstrapper   (never touches Infrastructure directly)
```

Unlike many "Clean Architecture" templates that let the API project reference Infrastructure directly (for DI convenience), this project introduces a **Bootstrapper** project so the API layer has zero knowledge of Infrastructure — a stricter adherence to Uncle Bob's Dependency Rule.

---

## 🧠 Domain Model

| Aggregate / Entity | Responsibility |
|---|---|
| **User** | Account identity, credentials, profile |
| **Project** | Aggregate root — owns membership and enforces role-based rules |
| **ProjectMember** | Entity within `Project` — binds a `User` to a `Project` with a `ProjectRole` |
| **TaskItem** | Independent aggregate — belongs to a `Project`, optionally assigned to a `User` |

**Business rules enforced inside the domain**, e.g.:
- Only one `Owner` per project; the `Owner` role can never be changed or removed
- Only `Owner`/`Manager` can add or remove members, change task priority, or reassign tasks
- A `Member` can only update the status of tasks assigned to them
- Every project automatically gets its creator as `Owner` on creation

---

## 🛠️ Tech Stack

- **.NET 10** (LTS)
- **ASP.NET Core Web API**
- **Entity Framework Core**
- **MediatR** (CQRS)
- **FluentValidation**
- **xUnit**
- **Docker**

---

## 🚀 Getting Started

```bash
git clone https://github.com/mehdihajati/TaskManager.git
cd TaskManager
dotnet restore
dotnet build
dotnet run --project src/TaskManager.API
```

Swagger UI will be available at `https://localhost:{port}/swagger`.

---

## 📌 Project Status

This project is under active development as part of a backend engineering portfolio focused on .NET / Clean Architecture / DDD.

- [x] Domain layer (entities, aggregates, repository contracts)
- [ ] Application layer (CQRS, validation)
- [ ] Infrastructure layer (EF Core, repositories)
- [ ] API layer (Controllers, JWT auth, Swagger)
- [ ] Tests
- [ ] Docker + CI/CD

---

## 📄 License

MIT
