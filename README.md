# Nutri Facts

App for tracking macronutrients in everyday life.

## About the project

Nutri Facts was created to make food and macro tracking easier in a real daily routine.
With it, you can search foods by text or scan a barcode, log intake throughout the day, and use history to speed up new entries.

This repository represents a functional MVP.
It is not finished yet, but it already allows authenticated users to:

- Sign in to their account
- Add foods consumed during the day
- Update records
- Delete records

## Main features

- Food search by text (Open Food Facts)
- Barcode search (Open Food Facts)
- Food entry logging
- Daily intake tracking
- Entry history to make logging easier
- JWT authentication

## Tech stack

- Backend: ASP.NET Core (net10.0), Entity Framework Core, PostgreSQL
- Frontend: React + TypeScript + Vite
- Testes: xUnit (backend)

## Repository structure

- backend: API, business rules, authentication, persistence, and tests
- frontend/nutri-facts-front: web application

## How to run locally

### 1) Prerequisites

- .NET SDK 10
- Node.js 20+
- PostgreSQL

### 2) Configure environment variables (backend)

Set the variables below in your operating system:

- ConnectionStrings__Default
- Jwt__Key

Example for ConnectionStrings__Default:

Host=localhost;Port=5432;Database=nutrition;Username=YOUR_USERNAME;Password=YOUR_PASSWORD

### 3) Run backend

In the backend directory:

dotnet restore
dotnet run

By default, the backend starts with Swagger enabled in development mode.

### 4) Run frontend

In the frontend/nutri-facts-front directory:

npm install
npm run dev

### 5) Run backend tests

In the backend/NutriFacts.Tests directory:

dotnet test

## Current status

- Functional MVP
- Main authentication and food management flow implemented
- Planned improvements for user experience, nutrition rules, and product refinements

## Next steps
- improve deployment with docker
- improve UI it is not well adapted for mobile/desktop 
- Improve onboarding and frontend user feedback
- Expand integration test coverage
- Add more granular daily macro targets
- Improve history and progress visualizations
