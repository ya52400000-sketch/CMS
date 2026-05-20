Clinic Management System (CMS) 🏥

A secure and modern Clinic Management System built using ASP.NET Core, designed to connect Doctors and Patients through an efficient healthcare platform.

The system allows patients to book appointments with doctors, while doctors can manage consultations and provide medical diagnoses directly through the platform.

The project implements secure authentication and authorization using ASP.NET Identity, JWT Authentication, and Role-Based Authorization.

✨ Features
👨‍⚕️ Doctor Features
View patient appointments
Manage consultations
Add medical diagnosis and notes
Access patient medical history
Secure doctor dashboard
🧑 Patient Features
Register and login securely
Browse available doctors
Book appointments online
View diagnosis and appointment history
Manage personal profile
🔐 Authentication & Authorization
ASP.NET Identity Authentication
JWT Authentication
Role-Based Authorization
Secure password hashing
Protected API endpoints
Supported Roles
Doctor
Patient
Admin (optional)

🛠️ Tech Stack
Backend
ASP.NET Core Web API
C#
Database
SQL Server
Entity Framework Core
Authentication & Security
ASP.NET Identity
JWT (JSON Web Token)
Role-Based Authorization
Tools & Technologies
LINQ
RESTful APIs
Swagger UI
Dependency Injection

🚀 System Workflow
Patient Flow
Register/Login
Browse doctors
Book appointment
View diagnosis and consultation details
Doctor Flow
Login securely
View appointments
Open patient case
Add diagnosis and medical notes

🔐 Security

The system follows modern security practices:

JWT Token Authentication
ASP.NET Identity Integration
Password Hashing
Role-Based Access Control (RBAC)
Protected API Routes
Unauthorized Access Prevention

⚙️ Installation
1️⃣ Clone Repository
git clone https://github.com/ya52400000-sketch/CMS.git
2️⃣ Navigate To Project
cd CMS
3️⃣ Restore Packages
dotnet restore
4️⃣ Configure Database

Update appsettings.json:

"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=ClinicDB;Trusted_Connection=True;TrustServerCertificate=True;"
  
  🗄️ Run Database Migrations
dotnet ef database update

▶️ Run The Project
dotnet run

👨‍💻 Developer

Developed by Youssef Ahmed
