# Currency App

## Project Description

Currency App is a web application that allows downloading and viewing currency exchange rates from the NBP API.

The application consists of three modules:

- Angular Frontend
- ASP.NET Core Web API Backend
- PostgreSQL Database

All modules run in Docker containers and communicate through Docker Compose.

---

## Technologies

### Frontend

- Angular
- TypeScript
- Bootstrap

### Backend

- ASP.NET Core Web API (.NET 9)
- Entity Framework Core

### Database

- PostgreSQL

### Containerization

- Docker
- Docker Compose

### External API

- NBP API (Narodowy Bank Polski)

---

## Features

- Download currency rates from NBP API
- Save currency rates into PostgreSQL database
- Display currency rates
- Search rates by:
  - Day
  - Month
  - Quarter
  - Year

- Sort rates by value

---

## Project Structure

currency-app/

├── CurrencyApp.API/

├── CurrencyApp.Tests/

├── currency-app-ui/

├── docker-compose.yml

└── README.md

---

## Running the Application

### Requirements

- Docker Desktop

### Start application

```bash
docker compose up -d --build
```

### Frontend

```text
http://localhost:4200
```

### Backend API

```text
http://localhost:5262
```

---

## Docker Containers

### currency-ui

Angular frontend served by Nginx.

### currency-api

ASP.NET Core Web API.

### currency-db

PostgreSQL database.

---

## API Endpoints

### Download and save currencies

```http
POST /currencies/fetch
```

### Get currencies

```http
GET /currencies
```

### Search by date

```http
GET /currencies/date/{date}
```

### Search by year

```http
GET /currencies/year/{year}
```

### Search by month

```http
GET /currencies/month/{year}/{month}
```

### Search by quarter

```http
GET /currencies/quarter/{year}/{quarter}
```

---

## Tests

Backend tests:

```bash
dotnet test
```

Frontend tests:

```bash
ng test
```

---

## Author

Artem Vorozhbyt

Advanced Programming Project

2026
