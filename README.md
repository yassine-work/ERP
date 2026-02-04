<p align="center">
  <img src="https://img.shields.io/badge/.NET-9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt=".NET 9.0"/>
  <img src="https://img.shields.io/badge/ASP.NET_Core-MVC-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="ASP.NET Core MVC"/>
  <img src="https://img.shields.io/badge/Entity_Framework-Core_9.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white" alt="EF Core"/>
  <img src="https://img.shields.io/badge/SQLite-003B57?style=for-the-badge&logo=sqlite&logoColor=white" alt="SQLite"/>
  <img src="https://img.shields.io/badge/Bootstrap-5.0-7952B3?style=for-the-badge&logo=bootstrap&logoColor=white" alt="Bootstrap"/>
</p>

---

# ERP - Human Resources Management System

A comprehensive Enterprise Resource Planning (ERP) solution focused on Human Resources management, built with modern .NET technologies. This application provides a complete suite of HR modules including employee management, recruitment, payroll, equipment tracking, and compensation management.

<p align="center">
  <a href="https://erp-y94u.onrender.com/">
    <img src="https://img.shields.io/badge/Live_Demo-Visit_Application-success?style=for-the-badge&logo=render&logoColor=white" alt="Live Demo"/>
  </a>
</p>

---

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [Project Structure](#project-structure)
- [Modules](#modules)
- [Deployment](#deployment)
- [License](#license)

---

## Features

| Module | Description |
|--------|-------------|
| **Employee Management** | Complete employee directory with detailed profiles, contract management, and status tracking |
| **Recruitment** | Job offers management, candidate tracking, application processing, and interview scheduling |
| **Payroll (Paie)** | Salary management with configurable allowances, bonuses, and advantages |
| **Equipment Management** | Inventory tracking, equipment assignment to employees, and maintenance history |
| **Compensation Packages** | Flexible compensation structure with multiple benefit types |
| **Position Management** | Organizational structure with job positions and hierarchies |

---

## Technology Stack

| Category | Technology |
|----------|------------|
| **Framework** | .NET 9.0 / ASP.NET Core MVC |
| **ORM** | Entity Framework Core 9.0 |
| **Database** | SQLite |
| **Frontend** | Bootstrap 5, jQuery, Bootstrap Icons |
| **Deployment** | Docker, Render.com |

---

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Git](https://git-scm.com/)

### Installation

1. **Clone the repository**

```bash
git clone https://github.com/your-username/ERP.git
cd ERP
```

2. **Restore dependencies**

```bash
dotnet restore
```

3. **Apply database migrations**

```bash
dotnet ef database update
```

### Running the Application

```bash
dotnet run
```

The application will be available at `https://localhost:5001` or `http://localhost:5000`.

---

## Project Structure

```
ERP/
|-- Controllers/          # MVC Controllers for all modules
|-- Data/                 # Database context and seeders
|-- Middleware/           # Custom middleware components
|-- Migrations/           # Entity Framework migrations
|-- Models/               # Domain models and view models
|-- Properties/           # Launch settings
|-- Services/             # Business logic services
|-- Views/                # Razor views organized by module
|-- wwwroot/              # Static files (CSS, JS, libraries)
|-- Program.cs            # Application entry point
|-- ERP.csproj            # Project configuration
|-- appsettings.json      # Application configuration
```

---

## Modules

### Employee Management (Employes)
Manage the complete employee lifecycle from hiring to departure. Features include:
- Employee profiles with personal and professional information
- Contract management (CDI, CDD, etc.)
- Status tracking (Active, On Leave, Terminated)
- Equipment assignment history
- Compensation package association

### Recruitment (Recrutement)
End-to-end recruitment process management:
- Job offer creation and publication
- Candidate database management
- Application tracking with status workflow
- Interview scheduling and notes

### Payroll (Paie)
Comprehensive payroll management:
- Salary calculation based on position
- Configurable allowances and bonuses
- Advantage types management
- Payslip generation

### Equipment Management
Track and manage company assets:
- Equipment inventory with categories
- Assignment to employees with history
- Status tracking (Available, Assigned, Maintenance, Retired)
- Return and reassignment workflows

### Compensation Packages
Flexible employee compensation:
- Multiple bonus types
- Allowance configurations
- Advantage packages
- Employee-specific customizations

---

## Deployment

The application is containerized with Docker and deployed on Render.com.

### Docker Build

```bash
docker build -t erp-system .
docker run -p 8080:8080 erp-system
```

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment | Production |
| `PORT` | Application port | 8080 |

---

## Live Demo

The application is deployed and accessible at:

**[https://erp-y94u.onrender.com/](https://erp-y94u.onrender.com/)**

---

## License

This project is developed for educational and demonstration purposes.

---

<p align="center">
  Built with <img src="https://raw.githubusercontent.com/dotnet/brand/main/logo/dotnet-logo.svg" width="20" alt=".NET"/> .NET 9.0
</p>
