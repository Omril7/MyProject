# MyProject
## Add DB
1. Initialize SQL Server on your Visual Studio (If you dont have one)
2. Add new DataBase to your SQL Server
3. Copy your DataBase ConnectionString (Right click on DB -> Properties -> ConnectionString)
4. Open appsettings.json file in the TelHai.CS.ServerAPI project
5. Paste the ConnectionString in line 10 : "ExamContext": "{ConnectionString}"
6. Open the Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
7. Write Add-Migration {migration-name}
8. Write Update-Database
