# EFCoreNetStandard_RTM
Experiments with EFCore RTM in NetStandard Project Plus XUnit Tests

EFCore in a NetStandard 1.6 project.
Using XUnit tests to explore some EF Core behaviors.

Status:
Migrations working  
InMemory Tests passing
Database Dependent Tests passing

Of interest for now is getting the project.json sorted out for the EFCore project and the XUnit project. They are all working together now. Was tricky. In order to do the migrations, you have to point to an executable project (in this solution, that's the test project) and that project needs to have a dependency on EntityFrameworkCore.Tools.

I'm just starting in with the tests using some tests from earlier (in the EF7WhoAreYou repository) I created as my starting point. 
But everything is compiling and the inmemory provider tests are all passing.

Note that for the migrations and db integration tests, I'm targetting SQL Server LocalDb. The connection string is hard-coded into the SamuraContext so you can change it there if needed.


