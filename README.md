# Baby Nanny

Baby Nanny is a cross‑platform application built with **.NET MAUI** and **Blazor**. It lets parents track their child's activities such as feeding, sleeping and diaper changes. Data is stored locally in a SQLite database and presented via Telerik Blazor UI components.

## Project structure

```
BabyNanny.sln
└── BabyNanny/
    ├── App.xaml / App.xaml.cs            # Application entry
    ├── MauiProgram.cs                    # Dependency registration
    ├── MainPage.xaml / MainPage.xaml.cs  # Hosts the BlazorWebView
    ├── Components/                       # Razor pages and layout
    ├── Data/                             # SQLite repository and dialog service
    ├── Models/                           # Database entities
    ├── Platforms/                        # Platform-specific code
    ├── Resources/                        # Icons, fonts, images
    └── wwwroot/                          # Blazor static assets
```

### Key concepts

* **MauiProgram.cs** configures the app, registers services and sets up the SQLite-backed `BabyNannyRepository`.
* **BabyNannyRepository** creates tables for `Child` and `BabyAction` and exposes CRUD methods.
* **Models** (`Child` and `BabyAction`) map to SQLite tables using attributes from `sqlite-net` and `SQLiteNetExtensions`.
* **Home.razor** implements the main UI where users log feeding, sleeping and diaper events.

## Requirements

* [.NET 9 SDK](https://dotnet.microsoft.com/) with MAUI workload installed
* Telerik UI for Blazor (refer to your license for setup)

## Building and running

Restore and build the solution from the command line:

```bash
# restore dependencies
 dotnet restore

# build the MAUI app
 dotnet build BabyNanny.sln
```

To run for a specific platform, use `-f` with the appropriate target framework (e.g. `net9.0-android`, `net9.0-ios`, `net9.0-windows10.0.19041.0`).

## Next steps

The application is intentionally simple and serves as a learning project for MAUI + Blazor. Useful areas to explore next are:

1. Adding more features, such as statistics or reminders.
2. Enhancing the UI with additional Telerik components.
3. Synchronizing data to a cloud service for backup.

Licensed under the MIT License. See `LICENSE.txt` for details.
