# 🚗 CarHub – ASP.NET Core MVC Car Marketplace

CarHub is a full-stack ASP.NET Core MVC web application for buying and selling cars.
The project is developed as part of the ASP.NET Advanced course and demonstrates a clean layered architecture, role-based access, and unit testing.

---

## 📌 Project Overview

CarHub allows users to:

* Browse car listings
* Create, edit, and delete their own ads
* Add listings to favorites

The application also includes an **Admin Area** for managing content and categories.

---

## 🧱 Architecture

The project follows a **layered architecture**:

* **CarHub.Web** – Presentation layer (MVC, Controllers, Views, Areas)
* **CarHub.Service.Core** – Business logic layer (Services, Interfaces)
* **CarHub.Data** – Data access layer (DbContext, Configurations)
* **CarHub.Data.Models** – Entity models
* **CarHub.ViewModels** – View models (UI binding models)
* **CarHub.Tests** – Unit tests (NUnit, EF Core InMemory)

---

## ⚙️ Technologies Used

* ASP.NET Core MVC (.NET)
* Entity Framework Core
* SQL Server
* ASP.NET Identity (Authentication & Authorization)
* NUnit (Unit Testing)
* EF Core InMemory (Testing)
* Bootstrap (UI)

---

## 🔐 Authentication & Authorization

* User registration and login via ASP.NET Identity
* Role-based access control:

  * **User** – can create and manage their own car ads
  * **Admin** – has access to the Admin Area

---

## 🛠️ Features

### 👤 User Features

* Create, edit, and delete car ads
* Browse all listings with filtering and pagination
* View detailed information for each car
* Add/remove favorites

### 🛡️ Admin Features

* Dedicated **Admin Area**
* Manage all car ads
* Manage categories
* Full control over platform content

---

## 🧪 Unit Testing

* Implemented using **NUnit**
* Uses **EF Core InMemory database**
* Covers core service logic

---

## 📂 Project Structure

```
CarHub/
 ├── CarHub.Web/
 │    ├── Areas/Admin/
 │    ├── Controllers/
 │    ├── Views/
 │
 ├── CarHub.Service.Core/
 ├── CarHub.Data/
 ├── CarHub.Data.Models/
 ├── CarHub.ViewModels/
 └── CarHub.Tests/
```

---

## 🚀 Getting Started

1. Clone the repository
2. Configure your connection string in `appsettings.json`
3. Run database migrations:

```
dotnet ef database update
```

4. Start the application:

```
dotnet run
```

---

## 👨‍💻 Author

Developed by **Danail Petrov**

---

## 📌 Notes

This project is intended for educational purposes and demonstrates:

* Clean architecture principles
* Separation of concerns
* Testable service layer
* Role-based application design
