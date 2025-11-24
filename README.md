# Haelya – E-commerce Platform (Work in Progress)
![.NET 8](https://img.shields.io/badge/.NET%208-512BD4?logo=dotnet&logoColor=white)
![Angular 19](https://img.shields.io/badge/Angular%2019-DD0031?logo=angular&logoColor=white)
![SQL%20Server](https://img.shields.io/badge/SQL%20Server-CC2927?logo=microsoftsqlserver&logoColor=white)
![Work%20in%20Progress](https://img.shields.io/badge/Status-WIP-yellow)

Haelya is a personal project designed to explore modern full-stack development using **ASP.NET Core Web API** and **Angular**.  
It follows Clean Architecture principles to ensure scalability, separation of concerns, and maintainability.

Although still in progress, the project already demonstrates backend–frontend communication, authentication, error handling, domain-driven structure, and catalog layout integration.

---

## 🛠 Tech Stack

### Backend (.NET 8)
- ASP.NET Core Web API
- Clean Architecture (Api, Application, Domain, Infrastructure)
- Entity Framework Core + SQL Server
- LINQ, AutoMapper, FluentValidation
- JWT Authentication & Authorization
- Centralized exception handling & logging

### Frontend (Angular 19)
- Standalone components
- Routing, services, API calls
- Product catalog UI based on Booksaw template integration

### Tooling
- Visual Studio 2022 / VS Code
- SQL Server / SSMS
- Git & GitHub
- Node.js + Angular CLI

---

## ✅ Current Features

### Backend
- User registration, login & profile retrieval
- Secure JWT authentication
- Exception-handling middleware
- Product module structure (filters + pagination in progress)
- Clean separation between layers & domain logic

### Frontend
- Angular setup connected to API
- Home page + product catalog layout
- Product card component with pricing and CTA
- Routing structure prepared for future pages

> 🚧 More features planned — project currently on hold but will be resumed later.

---

## 📂 Project Structure

```text
haelya/
 ├─ backend/
 │   ├─ Haelya.Api
 │   ├─ Haelya.Application
 │   ├─ Haelya.Domain
 │   ├─ Haelya.Infrastructure
 │   └─ Haelya.Shared (if present)
 └─ frontend/
     └─ haelya-front (Angular app)
``` 

	 
## ▶️ How to Run the Project


### Backend – ASP.NET Core API

1. Navigate to the API project:
   ```bash
   cd backend/Haelya.Api
   ```
   
2. Configure your SQL Server connection string in : 
	```bash
	appsettings.Development.json
	```
	
3. Apply Entity Framework migrations : 
	```bash
	dotnet ef database update
	```
	
4. Run the API: 
	```bash
	dotnet run
	```
	
Default URLs (may vary depending on configuration):

https://localhost:7200
http://localhost:5200

### Frontend – Angular

1. Navigate to the Angular project:
   ```bash
   cd frontend/haelya-front
	```
	
2.Install dependencies:

```bash
   npm install
```


3. Start the development server:	
	
```bash
    ng serve
```
	
Default URL:

http://localhost:4200


If needed, update the API base URL in:

src/environments/environment.ts


## 📌 Project Status

The project is currently **on hold**, but it already demonstrates:

- Full-stack .NET + Angular development
- Clean Architecture implementation
- Secure JWT authentication
- Centralized exception handling
- Component-based frontend UI
- Maintainable, modular and well-organized codebase

Planned improvements include:
- Completing the Product module (CRUD, filtering, pagination)
- Shopping cart, checkout & order management
- Admin back-office dashboard

---

## 👤 Author

**Maximilien Schaekels**  
Junior Full Stack / .NET Developer  
GitHub — https://github.com/Max-Schaekels  
LinkedIn — [Maximilien Schaekels](https://www.linkedin.com/in/maximilien-schaekels-371738326/)

---

# 🇫🇷 Version Française

## 📌 Statut du Projet

Le projet est actuellement **en pause**, mais il illustre déjà :

- du développement full-stack .NET + Angular
- une implémentation structurée de l’architecture Clean
- une authentification sécurisée via JWT
- une gestion centralisée des erreurs API
- une interface frontend modulaire et réutilisable
- un code maintenable, clair et bien organisé

Évolutions prévues :
- finalisation du module Produit (CRUD, filtres, pagination)
- panier, commande et processus de paiement
- interface d’administration / back-office

---

## 👤 Auteur

**Maximilien Schaekels**  
Développeur Junior Full Stack / .NET  
GitHub — https://github.com/Max-Schaekels  
LinkedIn — [Maximilien Schaekels](https://www.linkedin.com/in/maximilien-schaekels-371738326/)

	
