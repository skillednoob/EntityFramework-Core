# EntityFramework-Core
A .NET (C#) demo showcasing EF Core with Database-First approach, CRUD operations, stored procedures, and SQL functions.  


🌟 Key Features
 Full CRUD Operations (Create, Read, Update, Delete)  
 
 Stored Procedures (SPs) called from C#  
 
 Table-Valued Functions (TVFs) mapped to LINQ  
 
 Scalar-Valued Functions executed in queries  
 
 Database-First Approach (Scaffolded models)  
 

Setup Guide  

1️⃣ Create the Solution Structure  

1. Create a blank solution  
2. Add a class library project (for models/repositories)
3. Add a console application (as the startup project)
4. Add a reference to the class library in the console app  

2️⃣ Install Required Packages

  Microsoft.EntityFrameworkCore.SqlServer  
  
  Microsoft.EntityFrameworkCore.Design  
  
  ⚠️ Key Notes:  
  
  Only Microsoft.EntityFrameworkCore.Design is needed in the console app (for Package Manager Console commands).  
  
  Avoid redundant packages (e.g., Microsoft.EntityFrameworkCore.Tools is included in .Design).  
  
3️⃣ Database Setup  

  Run the SQL script   
  
4️⃣ Scaffold DbContext & Models  

  In Package Manager Console (PMC) in Visual Studio  
  

📂 Code Structure  

/DBFirstCore.ConsoleApp  

  └── Program.cs                   # Demo executions  
  

/DBFirstCore.DataAccessLayer  

  ├── Models/                      # Scaffolded entity classes and DbContext  
  
  ├── QuickKartRepository.cs       # CRUD + SP + TVF + Scalar Function logic  
  
  └── SQLScript/  
  
       ├── QuickKartDB.txt         # SQL script to create DB and schema  
       
       └── Scaffold.txt            # Example Scaffold command  

Note  
used appsettings.json file to store the connection string

 
