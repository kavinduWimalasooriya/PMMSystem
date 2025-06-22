# Property Maintenance Management System Setup Guide #
This guide provides a concise setup for your .NET 9 Web API backend and Angular 19 frontend.

## Getting Starte : Prerequisites
Ensure you have these installed:

Git: Download Git

.NET 9 SDK: Download .NET 9 SDK

Node.js (LTS) & npm: Download Node.js

Angular CLI

### Step 1: Clone the Repository
Open your terminal.

Navigate to your desired project directory.

Clone the repository:

git clone https://github.com/kavinduWimalasooriya/PMMSystem.git

cd PMMSystem

### Step 2: Backend Setup (.NET 9 Web API)
Navigate:

<code> cd PMMSystem.API </code>

Install & Build:

<code>dotnet restore
dotnet build</code>

Run:

<code>dotnet run</code>

(API typically runs on https://localhost:5001). Access Scaler at /(https://localhost:5001/scalar/v1).

### Step 3: Frontend Setup (Angular 19)
Navigate:

<code>cd ..
cd client</code>

Install & Build:

<code>npm install # or npm i
ng build</code>

Run:

<code>ng serve</code>

(Frontend typically runs on http://localhost:4200).
