# Agent Guide

This repository contains a .NET MAUI + Blazor project for tracking baby care activities.
Follow these guidelines when making contributions.

## Development

- **Use .NET 9 SDK**: Ensure the .NET 9 workload is installed and restore dependencies with `dotnet restore`.
- **Run Tests**: Execute `dotnet test BabyNanny.sln` before committing. All tests should pass.
- **Code Style**: Format C# files with `dotnet format` and keep naming consistent with existing code.

## Repository Structure

- `BabyNanny/` contains the main MAUI project.
- `BabyNanny.Tests/` hosts the unit tests for helper classes.
- `README.md` provides an overview of the solution and build instructions.

## Adding Features

- Prefer small, focused commits with clear messages.
- Update or add tests for new functionality in the `BabyNanny.Tests` project.
- Document significant changes in `README.md`.
