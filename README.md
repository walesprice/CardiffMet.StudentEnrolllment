Cardiff Met Student Enrollment website

This is the Cardiff Met Student Enrollment website.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. 

### Prerequisites

To run the Gwent Archives website, you will need to install the following: 

- [Visual Studio 2022](https://visualstudio.microsoft.com/)
- [SQL Server, Developer version](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)


### Using the website locally
1. Open SSMS, log in to your local db account with Windows Auth
2. Create a new database called "CardiffMetEnrollment"
3. Open Visual Studio, find and open the solution file for the project 'CardiffMetroUni.StudentEnrollment.WebAPI'
4. Right click on the solution name and select "Manage User Secrets"
5. Paste the below into the secrets file:
```Json

{
  "ConnectionStrings": {
    "StudentEnrollment": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=CardiffMetroUni.StudentEnrollment;Integrated Security=True;"
  }
}
```

3. Find and open the solution file for the project 'CardiffMetroUni.StudentEnrollment.WebApp'
4. Right click on the solution name and select "Manage User Secrets"
5. Paste the below into the secrets file changing the Url/Port of the API to be correct for your solution:
```Json
{ "WebAPI.BaseUrl": "https://localhost:7011" }
```
6. Load the Package Manager Console.
7. If you are in the src folder.
8. Run the command '..\scripts\update-database.cmd' this will generate the database.
9, Ensure that in Settings, Configure Startup Projects, both the API (selected first) and the Web projects are ticked as they depend on each other.
6. Build and run the project.
7. The Home Page screen should be displayed.


## Live enviroment 

The specific details for releasing to a live environment very much depends on the live environment.

## Testing


In order for the testing project 'CardiffMetroUni.StudentEnrollment.UnitTests' to work correctly, the API project would need to be released to a IIS or Azure platoform
Also the hard coded Test URL would come from a secrets file.

```Json
{ "WebAPI.BaseUrl": "https://localhost:7011" }
```

The API has only been run locally and not released for this demo.
