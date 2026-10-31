/*

Browser/Postman
    |
    |
AuthController
    |
    |
User Service
    |
    |
DbContext
    |
    |
SQL Server            

*/

/*

Kiến trúc tổng quan của dự án SmartBA:

SmartBA.API
│
├── Controllers
│     └── AuthController
│
SmartBA.Application
│
├── Interfaces
│     └── IUserService
│
SmartBA.Infrastructure
│
├── Services
│     └── UserService
│
├── Persistence
│     └── ApplicationDbContext

*/


/*

Browser/Postman
        │
        ▼
AuthController
        │
        ▼
IUserService
        │
        ▼
UserService
        │
        ▼
ApplicationDbContext
        │
        ▼
Users Table
        │
        ▼
User Entity  ────── Convert thành DTO
        │
 Mapping
        ▼
UserProfileDto
        │
        ▼
AuthController
        │
        ▼
JSON Response



SmartBA.Application
 └─ DTOs
     └─ Auth
         └─ UserProfileDto.cs

SmartBA.Application
 └── Interfaces
      └── User
           └── IUserService.cs

SmartBA.Infrastructure
 └─ Services
     └─ User
         └─ UserService.cs       

AuthController : var profile = await _userService.GetProfileAsync(userId);
|
|
IUserService : Task<UserProfileDto?> GetProfileAsync(Guid userId);
|
|
UserService : public async Task<UserProfileDto?> GetProfileAsync(Guid userId)
|
|
DbContext : _db.Users
|
|
SQL Server : SELECT * FROM Users WHERE Id = @userId
|
|
User Entity : C# User
|
|
Mapping : C# UserProfileDto
|
|
AuthController : C# return Ok(profile);


*/

/*

SmartBA.API
 ├── SmartBA.Application
 ├── SmartBA.Infrastructure
 └── SmartBA.Domain

SmartBA.Infrastructure
 ├── SmartBA.Application
 └── SmartBA.Domain

SmartBA.Application
 └── SmartBA.Domain

SmartBA.Domain
 └── không reference ai
 
*/