# EFCoreNetStandard_RTM
Experiments with EFCore RTM in NetStandard Project Plus XUnit Tests

EFCore in a NetStandard 1.6 project.
Using XUnit tests to explore some EF Core behaviors.

Status:
Migrations: working  
InMemory Tests: passing  
Database Dependent: Tests passing  

**Of interest for now is getting the project.json sorted out for the EFCore project, migrations and the XUnit project to tests against the EF Core project.** 

They are all working together now. Was tricky. In order to do the migrations, you have to point to an executable project because the EF Tools don't (yet) support class libraries. So in this solution, that's the exe project is the test project and that project needs to have a dependency on EntityFrameworkCore.Tools.

I'm just starting in with the tests using some tests from earlier (in the EF7WhoAreYou repository) I created as my starting point. *Please keep in mind that I have not yet re-addressed the tests with respect to their relevancy to EF Core RTM.*

But everything is compiling and the inmemory provider tests are all passing.

Note that for the migrations and db integration tests, I'm targetting SQL Server LocalDb. The connection string is hard-coded into the SamuraContext so you can change it there if needed. You can do the migrations using the powershell commands in the package manager console. That way I could have the test project as my startup project but target the class library with the DbContext in it. If you want to use the CLI commands (e.g dotnet ef migrations) the team added a startup-project parameter so you can do it in the CLI as well. So for this project you would be sure that the command line is in the EFCoreDbContext project. Then at the command line you can 

```
dotnet ef --startup-project ..\netstandardintegrationtest [command]  
e.g. 
dotnet ef --startup-project ..\netstandardintegrationtest migrations list
```
to get at the commands

