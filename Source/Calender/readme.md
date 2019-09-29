# Calender
The fake *domain* of this app is a Calender. Within different branches the starting branch *oop* will slowly be refactored to a functional setup.

## Description of Domain

You can:
* Add an **Event** every hour on every day
* An **Event** constists of:
..* Unique id
..* Title (100 chars max)
..* Description (1000 chars max -- optional)
..* When (datetime)
..* End (datetime)
* The *length* of an **Event** must be exactly one or multiple hours
* An **Event** cannot spread over multiple days (next day starts at midnight)
* An **Event** cannot be added for the past
* **Events** cannot overlap

# Run locally
This is a .NET core Api that uses *ISSExpress* and *localDB* run locally. Dependency Injection is done via *Startup.cs*.

## Sql script

* Connect to your **(localdb)\\MSSQLLocalDB** database instance.
* Run the script in the *scripts* directory. You should now have a **Calender** database with a single table.

## Visual studio

* Build Solution and run in *ISSExpress*.
* The *Swagger* page should now open, if it doesn't browse to http://localhost:55646/swagger.

# Branches

Refactoring will be in this order, the functionality won't change except the error messages send from the Api.

## oop

This is the starting state of the solution and fully (well should fully) handle all functionality within the domain. `CQRS` is used to separate the complexity of writing vs. reading.

The app consists of:
* An Api (using Dependency Injection, Swagger)
* A data layer (using Dapper ORM -- this supports both domain layers)
* A domain layer for *Writing* (or the `Command` part)
* A domain layer for *Reading* (or the `Query` part)

Validation is done with throwing validation exceptions which are caught and translated to a `HTTP 400` (bad request).

## fp-1-data-layer

This introduces the `Connect` function and `ConnectionString` type as a `Higher Order Function` to simplify connecting to the database.

## fp-2-use-option-and-constrained-types

This adds a `Value Objects` project as building blocks for the `Entities` in the Command part. Also it introduces the `Option type` from the `LaYumba.Functional` library to make explicit that some functions (or methods) not always produce data (preferred to dealing with `null`) which can be mapped to an `HTTP 404` (not found) in the Api layer. These also provide a way to make explicit that some data is optional in an `Entity`, such as the `Description` on an `Event` in our domain.
But it leaves our writing part (after the validation in de `AddEventCommandHandler`) ugly since the validation is not yet integrated fully in the `Entities`.

## fp-3-use-validation-workflow

In the final state we use the `Validation type` from the `LaYumba.Functional` library to handle invalid data. We also say goodbye to single member interfaces which are replaced with a `Func` and injected into the endpoints.
The validation has now become *business as usual* and a result of a call with invalid data; this can then be mapped to a desired `HTTP` return type in the Api layer (the validation exception type is removed).
Also `workflows` are introduced where necessary (the delete event usecase doesn't need it); these are given all necessary data from the database (with DI) to make all decisions and produce a valid entity to persist *or* a list of one or more errors (which doesn't need to be persisted).