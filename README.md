
# DCIT318 Assignment 4

This repository contains two Windows Forms applications developed in C# for the DCIT318 course assignment 4:

- **MedicalAppointmentSystem**
- **PharmacyManagementSystem**

---

## Table of Contents
- [Overview](#overview)
- [Requirements](#requirements)
- [Setup Instructions](#setup-instructions)
- [Running the Applications](#running-the-applications)
- [Project Structure](#project-structure)
- [Features](#features)
- [Screenshots](#screenshots)
- [Troubleshooting](#troubleshooting)
- [Author](#author)
- [License](#license)

---

## Overview

This solution contains two separate Windows Forms desktop applications:

### 1. MedicalAppointmentSystem
A user-friendly system for booking, viewing, and managing medical appointments, doctors, and patients.

### 2. PharmacyManagementSystem
An application for managing pharmacy inventory, processing sales, and handling medicine stock.

---

## Requirements

- **Operating System:** Windows 10/11
- **.NET SDK:** .NET 9.0 or later
- **IDE:** Visual Studio 2022 or later (with Windows Forms support)
- **Database:** SQL Server (LocalDB or full SQL Server instance)

---

## Setup Instructions

### 1. Clone the Repository

```sh
git clone https://github.com/Kelly-Buabeng/dcit318-assignment4-11180171.git
cd dcit318-assignment4-11180171
```

### 2. Database Setup

Both applications require a SQL Server database. By default, they use LocalDB. You can use SQL Server Express or a full SQL Server instance if preferred.

#### a. Create Databases

Open SQL Server Management Studio (SSMS) or use the command line to create two databases:

- `MedicalDB`
- `PharmacyDB`

#### b. Create Tables and (Optional) Seed Data

You need to create the required tables for each application. Example table structures:

**For MedicalAppointmentSystem:**

```sql
CREATE TABLE Doctors (
        DoctorID INT PRIMARY KEY IDENTITY,
        FullName NVARCHAR(100),
        Specialty NVARCHAR(100),
        Availability BIT
);

CREATE TABLE Patients (
        PatientID INT PRIMARY KEY IDENTITY,
        FullName NVARCHAR(100)
);

CREATE TABLE Appointments (
        AppointmentID INT PRIMARY KEY IDENTITY,
        DoctorID INT FOREIGN KEY REFERENCES Doctors(DoctorID),
        PatientID INT FOREIGN KEY REFERENCES Patients(PatientID),
        AppointmentDate DATETIME,
        Notes NVARCHAR(255)
);
```

**For PharmacyManagementSystem:**

```sql
CREATE TABLE Medicines (
        MedicineID INT PRIMARY KEY IDENTITY,
        Name NVARCHAR(100),
        Category NVARCHAR(100),
        Price DECIMAL(10,2),
        Quantity INT
);

-- You may also want to add tables for sales, etc.
```

#### c. Update Connection Strings (if needed)

The default connection strings use LocalDB. If you use a different SQL Server, update the `app.config` files in each project:

**MedicalAppointmentSystem/app.config**
```xml
<add name="MedicalDBConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MedicalDB;Integrated Security=True;" providerName="System.Data.SqlClient"/>
```

**PharmacyManagementSystem/app.config**
```xml
<add name="PharmacyDBConnection" connectionString="Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PharmacyDB;Integrated Security=True;" providerName="Microsoft.Data.SqlClient"/>
```

If using SQL Server Express or another instance, change `Data Source` accordingly (e.g., `Data Source=localhost;`).

---

## Running the Applications

### 1. Open the Solution

Open `dcit318-assignment4-11180171.sln` in Visual Studio.

### 2. Restore NuGet Packages

Visual Studio should automatically restore required NuGet packages. If not, right-click the solution and select **Restore NuGet Packages**.

### 3. Build the Solution

Build the solution (Ctrl+Shift+B) to ensure all dependencies are installed and the code compiles.

### 4. Set Startup Project

Right-click either `MedicalAppointmentSystem` or `PharmacyManagementSystem` in Solution Explorer and select **Set as Startup Project**.

### 5. Run the Application

Press **F5** or click **Start** to launch the application.

---

## Project Structure

```
dcit318-assignment4-11180171/
│
├── MedicalAppointmentSystem/
│   ├── app.config
│   ├── *.cs (C# source files)
│   ├── *.Designer.cs (UI designer files)
│   └── ...
│
├── PharmacyManagementSystem/
│   ├── app.config
│   ├── *.cs
│   ├── *.Designer.cs
│   └── ...
│
├── dcit318-assignment4-11180171.sln
├── README.md
└── Screenshot*.png
```

---

## Features

### MedicalAppointmentSystem
- View list of doctors and their availability
- Book new appointments (with validation)
- View and manage (update/delete) appointments
- View patient list

### PharmacyManagementSystem
- View all medicines in inventory
- Add new medicines
- Search medicines by name or category
- Update medicine stock
- Record sales and adjust stock levels

---

## Screenshots

Screenshots of the applications are included in the root directory for reference:

- `Screenshot 2025-08-23 000635.png`
- `Screenshot 2025-08-23 001108.png`
- ...

---

## Troubleshooting

- **Database Connection Errors:**
    - Ensure SQL Server (LocalDB or your instance) is running.
    - Check that the connection string in `app.config` matches your SQL Server setup.
    - Make sure the required databases and tables exist.
- **Build Errors:**
    - Ensure you have the correct .NET SDK and Visual Studio version installed.
    - Restore NuGet packages if missing.
- **UI Issues:**
    - If forms do not display correctly, rebuild the solution and ensure all designer files are present.
- **Data Not Displaying:**
    - Check that your database contains data and the connection string is correct.

---

## Author

Kelly Buabeng

---

## License

This project is for educational purposes only. 

