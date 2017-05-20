# REST and WCF SOAP service in .NET demo

This solution contains project that hosts SyncProfileRequest service and UserInfoProvider WCF service in one console application (Microsoft.OWIN library is used).
Also it contains TestApi service that can also be invoked: it is being used for test data management in functional tests.

## To launch application:
1) Build `UserRepositoryService.sln` in Visual Studio.
2) Run application `UserRepositoryServiceApp.exe` from `"..UserRepositoryService\UserRepositoryServiceApp\bin\Debug"`

## 1. Services: UserRepositoryServiceApp project
### SyncProfileRequest service:
- Address: derived from `App.config` file;
By default: http://localhost:2828/import.json
- Actions: support POST method. If `SyncProfileRequest` contains existing `UserId`, the corresponding data is being updated; if `UserId` does not exist, new data is being created.
Standard HTTP statuses are being returned;
- Logging: Serilog .NET library. Sink: `FileLogger`. Configuration: `App.config`;
- Communicates with internal repositories through the special manager class. 

### UserInfoProvider WCF service:
- Address, binding and behaviours are in `App.config`
Address by default: http://localhost:2828/UserInfoProviderService
- Actions: retrieves information for specific user by `Id`. Specific DTO is being used;
- Logging: Serilog .NET library. Sink: `FileLogger`. Configuration: `App.config`;
- Communicates with internal repositories through the special manager class.

### TestApi service:
This is helper for managing test data (CRUD methods for user sync requests)
http://localhost:2828/api/TestApi

### Infrastructure:
- Data (converters, DTO, internal entities...);
- Managers to transfer and translate data between services and internals;
- `UserRepositoryFactory` (Factory based on string repositoryType; interface; three possible implementations...)
- Models and Controllers
- Services (WCF interface and implementation) and Faults
- Logging

## 2. Functional Tests: UserRepositoryServiceTests project
- xUnit as base unit test framework. Also VS runner, xUnit additional libraries are being used;
- RestSharp for REST API service testing;
- .NET SVC util is being used for generating proxy for WCF service (Proxy -> CreateServiceProxy.cmd script). `UserRepositoryServiceProxy` contain auto-generated class.
- Utils:
  - `UserRepositoryUtils` - wrapper for TestApi REST calls
  - `TestUtils` - common helpers
  - `TestRunConfiguration` - contains configured entities to use in tests
- Cleanup logic for test data that have been generated across test run (Users in repository). Cleanup is being provided by xUnit fixtures that allow to implement dispose logic.

## Tests structure:

`SyncProfileRequestServiceTests`: negative and positive scenarios
- verification against repository
- verification against HTTP status codes
- verification against logger logic

`UserInfoProviderServiceTests`: negative and positive scenarios
- verification against repository
- verification against logger logic
