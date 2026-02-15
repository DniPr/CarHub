# 🚗 CarHub – Car Marketplace Web Application

CarHub is a full-stack ASP.NET Core MVC web application that allows users to create, manage, and explore car advertisements. The platform supports authentication, authorization, favorites management, and full CRUD functionality for car listings.

This project was developed using ASP.NET Core MVC, Entity Framework Core, and ASP.NET Core Identity.

---

## ✨ Features

### 👤 Authentication & Authorization

* User registration and login
* Secure logout functionality
* Only authenticated users can create, edit, delete, and favorite car ads
* Only the owner of a car ad can edit or delete it

### 🚘 Car Ads Management (CRUD)

* View all car advertisements
* View detailed information for each car
* Create new car ads
* Edit existing car ads (owner only)
* Delete car ads (owner only)
* View personal ads ("My Cars")

### ⭐ Favorites System

* Add car ads to favorites
* Remove car ads from favorites
* View personal favorites list
* Prevent duplicate favorites
* Automatic cleanup of favorites when a car ad is deleted

### 🗂 Categories

* Seeded categories in the database
* Dropdown selection when creating/editing ads

### 🧠 Architecture & Best Practices

* Service Layer abstraction (ICarAdService, IFavoriteService)
* Dependency Injection
* Separation of concerns
* ViewModels used for UI interaction
* Entity validation using Data Annotations
* Fluent API configuration for relationships

---

## 🛠 Technologies Used

* ASP.NET Core MVC (.NET 8)
* Entity Framework Core
* ASP.NET Core Identity
* SQL Server
* Bootstrap 5
* Razor Views
* LINQ
* Dependency Injection

---

## 🧱 Database Structure

Main entities:

* Users (ASP.NET Identity)
* CarAds
* Categories
* Favorites

Relationships:

* One User → Many CarAds
* One CarAd → One Category
* Many Users ↔ Many CarAds (via Favorites)

---

## ▶️ How to Run the Project

### 1. Clone the repository

```bash
git clone https://github.com/DniPr/CarHub.git
```

### 2. Open in Visual Studio

Open the solution file:

```
CarHub.sln
```

### 3. Apply database migrations

Open Package Manager Console and run:

```powershell
Update-Database
```

### 4. Run the application

Press:

```
Ctrl + F5
```

or click:

```
Start Debugging
```

---

## 🔐 Test User (Optional)

You can register a new account from the Register page.

---

## 🎯 Key Learning Objectives

This project demonstrates:

* ASP.NET Core MVC architecture
* Entity Framework Core usage
* Identity authentication and authorization
* Service-oriented architecture
* Clean code practices
* Full CRUD implementation
* Secure user-based data access

---

## 📌 Author

Developed by Danail Petrov

This project is for educational purposes.
