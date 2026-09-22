# EventDrivenAuditSys

this is my attempt at building a **clean architecture, event-driven audit system** in ASP.NET Core

 a small course-enrollment API where **every write gets audited asynchronously**, through domain events and an in-process queue with a background worker.

---

## Table of contents

1. [flow](#flow)
2. [running it](#running-it)
3. [the API](#the-api)

---

## flow

- Users get enrolled in courses via `POST /api/enrollments`.
- Enrolling a user is a **command** (CQRS). The handler does the business work and saves it.
- The `Enrollment` entity raises a **domain event** (`EnrollmentCreatedDomainEvent`) at the exact moment it's created — the entity itself decides "this thing happened".
- After the database save succeeds, the handler **publishes** that event through MediatR.
- An audit handler picks it up and — instead of writing an audit row right there — it drops an `AuditEvent` onto a **bounded in-process queue** (a `Channel<T>`).
- A **background service** drains that queue in batches and writes the audit rows with its own database scope.
- Net effect: the HTTP request never waits for audit writes. Audit is *eventually* persisted, and the business request is never failed by an audit problem.

There are also plain read endpoints (users, courses, enrollments, audit logs) that go through **queries**. Everything is MediatR, everything is testable, nothing knows about anything it shouldn't.


---

## Running it

**Prereqs:** .NET 8 SDK. That's it. No database server, no Docker, no env vars.

```bash
# from the repo root
dotnet build EventDrivenAuditSys.sln
dotnet run --project EventDrivenAuditSys.API
```

The app starts on **http://localhost:5088**, applies migrations, and (first run only) creates and seeds `EventDrivenAuditSys.API/eventdrivenaudit.db`. Swagger UI opens automatically (all launch profiles use `launchUrl: "swagger"`) — or go to **http://localhost:5088/swagger**.

Launch profiles (in `Properties/launchSettings.json`):

| Profile | URL |
|---|---|
| `http` (default for `dotnet run`) | http://localhost:5088 |
| `https` | https://localhost:7107 (and 5088) |
| IIS Express (from Visual Studio) | http://localhost:47712 |

The database file is git-ignored (`*.db`, `*.db-wal`, `*.db-shm` — WAL sidecar files, more on that later). Delete the `.db` file any time you want a fresh seeded start; the app rebuilds it on boot.

**Seeded data** (IDs are hardcoded, so these work in any fresh copy of the DB):

| Entity | Id | Name |
|---|---|---|
| User | `b3a1c2d3-4e5f-4a6b-8c7d-9e0f1a2b3c4d` | Alice Smith |
| User | `c4b2d3e4-5f6a-4b7c-9d8e-0f1a2b3c4d5e` | Bob Jones |
| Course | `e6d4f5a6-7b8c-4d9e-1f0a-2b3c4d5e6f7a` | C# Fundamentals (49.00) |
| Course | `f7e5a6b7-8c9d-4e0f-2a1b-3c4d5e6f7a8b` | Clean Architecture in .NET (89.00) |
| Course | `a8f6b7c8-9d0e-4f1a-3b2c-4d5e6f7a8b9c` | ASP.NET Core Web API Masterclass (199.00) |

---

## The API

Every controller is deliberately *thin* — parse input, send a MediatR message, shape the HTTP response. If you find business logic in a controller here, file a bug against me.

| Method | Route | Body / Query | Returns | Status codes |
|---|---|---|---|---|
| GET | `/api/users` | — | `UserDto[]` | 200 |
| GET | `/api/users/{id}` | — | `UserDto` | 200, 404 |
| GET | `/api/courses` | — | `CourseDto[]` | 200 |
| GET | `/api/courses/{id}` | — | `CourseDto` | 200, 404 |
| POST | `/api/enrollments` | `EnrollCourseCommand` | `EnrollCourseResponse` | 201, 400, 404, 409 |
| GET | `/api/enrollments?userId=` | `userId` (required) | `EnrollmentDto[]` | 200, 400 |
| GET | `/api/audit-logs?userId=` | `userId` (optional) | `AuditLogDto[]` | 200 |


### Try the whole thing in four calls

```bash
# 1. see who exists
curl http://localhost:5088/api/users

# 2. enroll Alice in the Web API masterclass
curl -X POST http://localhost:5088/api/enrollments \
  -H "Content-Type: application/json" \
  -d '{"userId":"b3a1c2d3-4e5f-4a6b-8c7d-9e0f1a2b3c4d","courseId":"a8f6b7c8-9d0e-4f1a-3b2c-4d5e6f7a8b9c"}'
# → 201, Location: /api/enrollments?userId=b3a1...c4d, body: {"enrollmentId":"..."}

# 3. her enrollments
curl "http://localhost:5088/api/enrollments?userId=b3a1c2d3-4e5f-4a6b-8c7d-9e0f1a2b3c4d"

# 4. the audit trail that the background worker wrote for step 2
curl "http://localhost:5088/api/audit-logs?userId=b3a1c2d3-4e5f-4a6b-8c7d-9e0f1a2b3c4d"
```


Error responses are RFC 7807 `application/problem+json` (details in [error handling](#error-handling-strategy)).

