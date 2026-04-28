# 🏆 Nexus Sports Manager - Sports Complex System

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![MySQL](https://img.shields.io/badge/MySQL-005C84?style=for-the-badge&logo=mysql&logoColor=white)
![Bootstrap](https://img.shields.io/badge/Bootstrap-563D7C?style=for-the-badge&logo=bootstrap&logoColor=white)

Welcome to **Nexus Sports Manager**, a complete web application built with **ASP.NET Core 8 MVC**. This system was designed to handle the daily operations of a modern sports complex, allowing administrators to easily manage users, sports spaces (like soccer or basketball courts), and reservations, while ensuring no double-bookings ever happen.

---

## 👤 Student Information
* **Name:** [WRITE YOUR NAME HERE]
* **Student ID / Code:** [WRITE YOUR ID HERE]
* **Course / Module:** [WRITE YOUR COURSE HERE]

---

## ✨ Key Features
* **Complete CRUD:** Full management for Users and Sports Spaces.
* **Smart Reservation Engine:** A rigorous background logic that prevents time-slot overlaps. It stops double-bookings for the same court and prevents a single user from booking different courts at the exact same time.
* **Dynamic Search & Filters:** Easily find specific courts by their type (e.g., filtering all "Soccer" spaces).
* **Premium Glassmorphism UI:** A sleek, dark-themed, responsive user interface designed with modern UX principles to provide a high-end feel.
* **Real Email Notifications:** Fully integrated with **MailKit** and SMTP to automatically send real email confirmations to users upon successful reservations.

---

## 📊 System Diagrams

### 1. Database Architecture (Entity-Relationship)
This diagram shows how our MySQL database is structured. It revolves around three core entities, with the `Reservations` table acting as the bridge linking users to specific sports spaces.

```mermaid
erDiagram
    Users ||--o{ Reservations : "makes"
    SportsSpaces ||--o{ Reservations : "booked for"

    Users {
        int Id PK
        string Document
        string Name
        string Phone
        string Email
    }

    SportsSpaces {
        int Id PK
        string Name
        string SpaceType
        int Capacity
    }

    Reservations {
        int Id PK
        int UserId FK
        int SportsSpaceId FK
        date ReservationDate
        time StartTime
        time EndTime
        string State
    }
```

### 2. Reservation Workflow
Here is how the system processes a new reservation request, including the anti-collision validation and the automated email dispatch.

```mermaid
sequenceDiagram
    actor Admin
    participant Controller as ReservationsController
    participant Service as ReservationService
    participant DB as MySQL Database
    participant Email as MailKit (SMTP)

    Admin->>Controller: Submits Booking (Date, Time, User, Space)
    Controller->>Service: CreateReservation(Reservation)
    
    Service->>DB: Check if Court is already booked at this time
    DB-->>Service: Result (Occupied / Free)
    
    alt is Occupied
        Service-->>Controller: Throws Overlap Exception
        Controller-->>Admin: Displays Error Message (UI Blocked)
    else is Free
        Service->>DB: Check if User has another booking at this time
        DB-->>Service: Result (Occupied / Free)
        
        alt User is busy
            Service-->>Controller: Throws Overlap Exception
            Controller-->>Admin: Displays Error Message (UI Blocked)
        else User is free
            Service->>DB: Save Reservation (State = Active)
            DB-->>Service: Success
            Service->>Email: Send(UserEmail, "Reservation Confirmed")
            Email-->>Service: Email Sent
            Service-->>Controller: Success
            Controller-->>Admin: Redirects to Dashboard
        end
    end
```

---

## 🚀 Setup & Installation Guide

To run this project on your local machine, follow these steps:

### 1. Clone the repository
```bash
git clone https://github.com/monterrosag18/PruebaDesempenoCsharp270326.git
cd PruebaDesempenoCsharp270326
```

### 2. Configure Database & Email Credentials
Open the `appsettings.json` file in the root directory and replace the placeholders with your actual credentials:

```json
{
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "Nexus Sports System",
    "SenderEmail": "your.real.email@gmail.com",
    "Username": "your.real.email@gmail.com",
    "Password": "your-16-character-app-password"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_MYSQL_SERVER;Database=YOUR_DATABASE;User Id=YOUR_USER;Password=YOUR_PASSWORD"
  }
}
```

### 3. Build and Run
Open a terminal in the project folder and run:
```bash
dotnet restore
dotnet build
dotnet run
```
*Alternatively, simply open the solution in Visual Studio and press `F5` (Run).*

---

## 📖 How to Use
1. **Step 1:** Upon launching the app, go to the **Users** section and register at least one client.
2. **Step 2:** Go to the **Spaces** section and register at least one sports court.
3. **Step 3:** Navigate to the **Reservations** section and try booking a slot. The system will automatically validate the availability and send an email confirmation.
