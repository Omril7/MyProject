# MyProject
<p align="left">
  <a href="https://docs.microsoft.com/en-us/dotnet/csharp/" target="_blank" rel="noreferrer">
    <img src="https://cdn.worldvectorlogo.com/logos/c--4.svg" width="36" height="36" alt="C#" />
  </a>
  <a href="https://dotnet.microsoft.com/en-us/" target="_blank" rel="noreferrer">
    <img src="https://www.vectorlogo.zone/logos/dotnet/dotnet-icon.svg" width="36" height="36" alt=".NET" />
  </a>
</p>

## Add DB
1. Initialize SQL Server on your Visual Studio (If you dont have one)
2. Add new DataBase to your SQL Server
3. Copy your DataBase ConnectionString (Right click on DB -> Properties -> ConnectionString)
4. Open appsettings.json file in the TelHai.CS.ServerAPI project
5. Paste the ConnectionString in line 10 : "ExamContext": "{ConnectionString}"
6. Open the Package Manager Console (Tools -> NuGet Package Manager -> Package Manager Console)
7. Write Update-Database
