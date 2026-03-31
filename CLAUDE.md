# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

This is an **Umbraco CMS 17.x** web application built on **ASP.NET Core / .NET 10.0**, with custom backoffice extension packages using **TypeScript + Lit web components** and **Vite**.

## Commands

### .NET Backend

```bash
dotnet build src/Protenacity.Cake.sln
dotnet run --project src/Protenacity.Cake.Web.UI/Protenacity.Cake.Web.UI.csproj
dotnet publish --configuration Release
```

### TypeScript Client Code (per package)

Each package under `src/Packages/*/Client/` has its own npm project:

```bash
npm install
npm run build    # Build TypeScript → wwwroot/App_Plugins/[Package]/
npm run watch    # Watch mode for development (auto-refreshes browser)
```

Node LTS 20.17.0+ required. Use nvm/nvm-windows/Volta.

## Architecture

### Solution Structure

- **Protenacity.Cake.Web.UI** — ASP.NET Core entry point; Umbraco config, MVC views, API controllers, middleware
- **Protenacity.Cake.Web.Core** — Business logic, data models, services, and Umbraco content type models (auto-generated in `Constitution/`)
- **Protenacity.Cake.Web.Presentation** — Presentation layer; categories, search, editors, page components, view models
- **Protenacity.Spreadsheet** — CSV/Excel utilities (CsvHelper + EPPlus)
- **src/Packages/** — Umbraco backoffice extension packages (Razor Class Libraries + TypeScript clients)

### Packages

Each package in `src/Packages/` is a self-contained Umbraco extension:

| Package | Purpose |
|---|---|
| `Protenacity.Web.UfmPercent` | Custom percentage input editor |
| `Protenacity.Web.TipTapHeaders` | TipTap rich text editor extension |
| `Protenacity.Web.OpenStreetMap` | OpenStreetMap integration |
| `Protenacity.Web.RollbackPreview` | Content rollback preview |
| `Protenacity.Web.GraphEmail` | Microsoft Graph / Microsoft 365 email |
| `Protenacity.Web.Review` | Review workflow functionality |

TypeScript source in each package's `/Client` folder compiles to `wwwroot/App_Plugins/[Package]/` for Umbraco to load.

### Key Dependencies

**Backend:** Umbraco.Cms v17, uSync v17, FusionCache, Serilog, Microsoft.Graph, Azure.Identity, OpenIddict, Umbraco.AI + Umbraco.AI.Anthropic

**Frontend:** `@umbraco-cms/backoffice`, Lit, Vite, TypeScript 5.6, ESLint 9, `@hey-api/openapi-ts`

**Database:** SQLite (`Umbraco.sqlite.db`)

### Umbraco Specifics

- Content type models are auto-generated — do not hand-edit files in `Constitution/` folders
- Custom editors register themselves as Umbraco App_Plugins via `umbraco-package.json` in each package's `wwwroot/App_Plugins/` directory
- uSync manages content/schema sync across environments
- Configuration: `appsettings.json` / `appsettings.Development.json` / `appsettings.Release.json`
