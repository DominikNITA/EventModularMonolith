# EventModularMonolith

## Project Description
Playground project for discovering Domain Driven Design in modular monolith architecture. Project is developed following [Milan Jovanovic's tutorial](https://www.milanjovanovic.tech/modular-monolith-architecture) and augmented by [Kamil Grzybek's ideas](https://github.com/kgrzybek/modular-monolith-with-ddd).
This project is an Event Management System built using C# and React. The system is designed to manage various types of events such as conferences, conventions, concerts, and more. It aims to provide a robust and scalable solution for event organizers to handle event planning, registration, scheduling, and attendee management.

## Technologies Used
- **C#**
- **.NET 8**
- **Entity Framework Core**
- **Dapper** (for read models)
- **MediatR** (for handling commands and queries inside modules)
- **MassTransit** (for exchanging integration events between modules)
- **FluentValidation** (for validation)
- **Redis** (for caching query results)
- **Azure Blob Service** (locally with Azurite for storing images) 
- **Docker** (for containerization and local developement)
- **React 18** (for client-facing web app)
- **Blazor Server** (for administration web app) **WIP**
- **Aspire Dashborad** (WIP)
- **Stripe Integration** (WIP)

## Concepts
- **CQRS** (for now single PostgreSQL database with separate schemas per module)
- **Inbox/Outbox patterns**
- **Redis Caching** (WIP, some problems with serialization)
- **SAGA** (WIP)
- **Client code generation from Swagger definition**

## Architecture
The project follows a Modular Monolith architecture, which means the application is divided into distinct modules that encapsulate specific business functionalities. Each module follows the principles of Domain-Driven Design (DDD) to ensure a clear separation of concerns and maintainability. Currently implemented backend modules:
- **Users** - managing users registration and authorization (TODO: Add concept of event organizer)
- **Events** - managing events with speakers/venue/images (TODO: Add schedule to events)
- **Ticketing** - perfoming tickets sales/refunds (WIP: currently lacking most of the processes +  Stripe integration)
- **Attendance** - checking tickets presented by attendees and counting stats (WIP: not yet created)

The project will also have 3 web applications:
- **Main selling app** - allowing users to search and buy tickets for events
- **Administration app** - for overwatching the main app and the whole
- **Organizers app** - managing events and reading QR codes on the tickets

## Roadmap
- [ ] Architecture tests
- [ ] Unit and integration tests for business logic
- [ ] Complete business logic for exisitng modules and add new Attendance module for managing ongoing events
- [ ] Add Authentication and Authorization with roles and permissions
- [ ] Extend client web app
- [ ] Create admin panel for managing and overwatching all events (with Aspire Dashboard)
