// using MongoDB.Driver;
// using MongoDB.Bson;
// using MongoDB.Bson.Serialization.Attributes;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Authentication.JwtBearer;
// using Microsoft.IdentityModel.Tokens;

// var builder = WebApplication.CreateBuilder(args);

// // Add services to the container.
// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("AllowAll", policy =>
//     {
//         policy.AllowAnyOrigin()
//               .AllowAnyMethod()
//               .AllowAnyHeader()
//               .WithExposedHeaders("Content-Disposition"); // If you're returning files, expose necessary headers
//     });
// });


// builder.Services.AddOpenApi();
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//         options.Authority = "https://your-auth-provider.com"; // Replace with your Auth provider
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateAudience = false
//         };
//     });
// builder.Services.AddAuthorization();


// builder.Services.AddSingleton<IUserRepository, MongoUserRepository>(); // MongoDB-based repository


// var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
// }

// app.UseCors("AllowAll");
// app.UseHttpsRedirection();
// app.UseAuthentication();
// app.UseAuthorization();

// var secretKey = "your_secret_key"; // Replace with a strong secret key

// // Utility functions
// string EncryptPassword(string password)
// {
//     using var sha256 = System.Security.Cryptography.SHA256.Create();
//     var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + secretKey));
//     return Convert.ToBase64String(hashedBytes);
// }

// string GenerateToken() => Guid.NewGuid().ToString();

// string GenerateSecureCode() => new Random().Next(100000, 999999).ToString();

// // Register endpoint
// app.MapPost("/api/auth/register", async (UserModel user, IUserRepository repo, ILogger<Program> logger) =>
// {
//     logger.LogInformation("Registering new user: {username}", user.username);

//     // Check if the username already exists
//     var existingUser = await repo.GetUserByUsernameAsync(user.username);
//     if (existingUser != null)
//     {
//         logger.LogWarning("username {username} already exists {existingUser}", user.username , existingUser);
//         return Results.BadRequest(new { Message = "username already exists"  , error = "failed"});
//     }

//     // Encrypt password and generate token
//     user.password = EncryptPassword(user.password);
//     user.token = GenerateToken();

//     try
//     {
//         await repo.SaveUserAsync(user);
//         logger.LogInformation("User registered successfully: {username}", user.username);
//         return Results.Ok(new { Message = "User registered successfully", token = user.token });
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, "Error during user registration: {username}", user.username);
//         return Results.Ok(new { Message = "User register failed" , error = "failed"});
//     }
// });

// // Generate secure code endpoint
// app.MapPost("/api/auth/secure/code", async (UserModel user, IUserRepository repo, ILogger<Program> logger) =>
// {
//     logger.LogInformation("Request received to generate secure code for user: {username}", user.username);
    
//     var storedUser = await repo.GetUserByUsernameAsync(user.username);
//     if (storedUser == null)
//     {
//         logger.LogWarning("User with username {username} not found , {storedUser}", user.username , storedUser);
//         return Results.Unauthorized();
//     }

//     if (storedUser.securecode == null)
//     {
//         storedUser.securecode = GenerateSecureCode();
//         await repo.UpdateUserAsync(storedUser);
        
//         logger.LogInformation("Secure code generated for user: {username}", user.username);
//         return Results.Ok(new { Message = "Secure code generated", securecode = storedUser.securecode });
//     }

//     logger.LogWarning("Failed to generate code for this user : {username}", user.username);
//       return Results.Ok(new { Message = "Failed to generate code !", error = "failed"});
// });

// // // Login endpoint
// // app.MapPost("/api/auth/login", async (UserModel user, IUserRepository repo, ILogger<Program> logger) =>
// // {
// //     logger.LogInformation("Login attempt for user: {username}", user.username);

// //     var storedUser = await repo.GetUserByUsernameAsync(user.username);
// //     if (storedUser != null && storedUser.password == EncryptPassword(user.password))
// //     {
// //         logger.LogInformation("Login successful for user: {username}", user.username);
// //         return Results.Ok(new { Message = "Login successful", token = storedUser.token });
// //     }

// //     logger.LogWarning("Login failed for user: {username}", user.username);
// //     return Results.Unauthorized();
// // });

// // Login endpoint
// app.MapPost("/api/auth/login", async (UserModel user, IUserRepository repo, HttpContext httpContext, ILogger<Program> logger) =>
// {
//     logger.LogInformation("Login attempt for user: {username}", user.username);

//     // Extract Bearer token from the Authorization header
//     // var authHeader = httpContext.Request.Headers["Authorization"].ToString();
//     // if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
//     // {
//     //     logger.LogWarning("Authorization header is missing or invalid for user: {username}", user.username);
//     //     return Results.Ok(new { Message = "Authorization header is missing or invalid" });
//     // }

//     // var token = authHeader.Substring("Bearer ".Length).Trim();

//     // Find the user in the database
//     var storedUser = await repo.GetUserByUsernameAsync(user.username);
//     if (storedUser == null || storedUser.securecode == null)
//     {
//         logger.LogWarning("User not found in the database: {username}", user.username);
//         return Results.Ok(new { Message = "Invalid username or password" , error = "failed" });
//     }

//     // Validate the password
//     if (storedUser.password != EncryptPassword(user.password) && storedUser.securecode != null)
//     {
//         logger.LogWarning("Password mismatch for user: {username}", user.username);
//         return Results.Ok(new { Message = "Invalid username or password" , error = "failed" });
//     }

//         // // Check if the token matches the one in the database
//         // if (storedUser.token != token)
//         // {
//         //     logger.LogWarning("Bearer token mismatch for user: {username}", user.username);
//         //     return Results.Ok(new { Message = "Invalid token" });
//         // }

//     logger.LogInformation("Login successful for user: {username}", user.username);
//     return Results.Ok(new { Message = "Login successful", token = storedUser.token });
// });


// // Forget password endpoint
// app.MapPost("/api/auth/forget", async (ForgetPasswordModel model, IUserRepository repo, ILogger<Program> logger) =>
// {
//     logger.LogInformation("password reset request for user: {username}", model.username);

//     var storedUser = await repo.GetUserByUsernameAsync(model.username);
//     if (storedUser != null && storedUser.securecode == model.securecode)
//     {
//         storedUser.password = EncryptPassword(model.newpassword);
//         storedUser.securecode = null; // Clear the secure code after use
//         await repo.UpdateUserAsync(storedUser);
        
//         logger.LogInformation("password updated successfully for user: {username}", model.username);
//         return Results.Ok(new { Message = "password updated successfully" , success="true" });
//     }

//     logger.LogWarning("Invalid secure code for user: {username}", model.username);
//     return Results.Ok(new { Message = "Invalid secure code for user" , error = "failed" });
//     // return Results.Unauthorized();
// });

// // Get Username from Token endpoint
// app.MapGet("/api/auth/profile", async (HttpContext httpContext, IUserRepository repo, ILogger<Program> logger) =>
// {
//     // Extract the token from Authorization header
//     var authHeader = httpContext.Request.Headers["Authorization"].ToString();
//     if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
//     {
//         logger.LogWarning("Authorization header is missing or invalid.");
//         return Results.Unauthorized(new { Message = "Authorization header is missing or invalid" });
//     }

//     var token = authHeader.Substring("Bearer ".Length).Trim();

//     try
//     {
//         // Decode the JWT token to extract username
//         var handler = new JwtSecurityTokenHandler();
//         var jsonToken = handler.ReadToken(token) as JwtSecurityToken;
//         if (jsonToken == null)
//         {
//             return Results.Ok(new { Message = "Invalid token", error = "failed" });
//         }

//         // Extract the username from token claims
//         var usernameClaim = jsonToken?.Claims.FirstOrDefault(c => c.Type == "username")?.Value;
//         if (string.IsNullOrEmpty(usernameClaim))
//         {
//             return Results.Ok(new { Message = "Username claim is missing from token" , error = "failed" });
//         }

//         // Get the user by username (you could optionally return more details about the user here)
//         var user = await repo.GetUserByUsernameAsync(usernameClaim);
//         if (user == null)
//         {
//             return Results.Ok(new { Message = "User not found" , error = "failed" });
//         }

//         logger.LogInformation("Username successfully extracted from token: {username}", usernameClaim);
//         return Results.Ok(new { Message = "Username found", username = usernameClaim  });
//     }
//     catch (Exception ex)
//     {
//         logger.LogError(ex, "Error decoding token");
//         return Results.Ok(new { Message = "Invalid token", error = ex.Message , error = "failed" });
//     }
// });


// app.Run();

// // Models and Repository
// public record UserModel
// {
//     [BsonId] // This attribute marks the _id field
//     public ObjectId Id { get; set; } // MongoDB's ObjectId type

//     public string username { get; set; } = string.Empty;
//     public string password { get; set; } = string.Empty;
//     public string? token { get; set; }
//     public string? securecode { get; set; }
// }

// public record ForgetPasswordModel
// {
//     public string username { get; set; } = string.Empty;
//     public string newpassword { get; set; } = string.Empty;
//     public string securecode { get; set; } = string.Empty;
// }

// public interface IUserRepository
// {
//     Task SaveUserAsync(UserModel user);
//     Task<UserModel?> GetUserByUsernameAsync(string username);
//     Task<UserModel?> CheckUserSecurityCode(string securityCode);
//     Task UpdateUserAsync(UserModel user);
// }

// public class MongoUserRepository : IUserRepository
// {
//     private readonly IMongoCollection<UserModel> _users;

//     public MongoUserRepository()
//     {
//         var client = new MongoClient("mongodb://localhost:27017"); // Replace with your MongoDB connection string
//         var database = client.GetDatabase("UserDatabase");
//         _users = database.GetCollection<UserModel>("Users");
//     }

//     public async Task SaveUserAsync(UserModel user)
//     {
//         await _users.InsertOneAsync(user);
//     }

//     public async Task<UserModel?> GetUserByUsernameAsync(string username)
//     {
//         return await _users.Find(u => u.username == username).FirstOrDefaultAsync();
//     }

//     public async Task<UserModel?> CheckUserSecurityCode(string securityCode){
//         return await _users.Find(u => u.securecode == securityCode).FirstOrDefaultAsync();
//     }

//     public async Task UpdateUserAsync(UserModel user)
//     {
//         await _users.ReplaceOneAsync(u => u.username == user.username, user);
//     }
// }



using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .WithExposedHeaders("Content-Disposition"); // If you're returning files, expose necessary headers
    });
});

builder.Services.AddOpenApi();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Authority = "https://your-auth-provider.com"; // Replace with your Auth provider
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
    });
builder.Services.AddAuthorization();

builder.Services.AddSingleton<IUserRepository, MongoUserRepository>(); // MongoDB-based repository

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

var secretKey = "your_secret_key"; // Replace with a strong secret key

// Utility functions
string EncryptPassword(string password)
{
    using var sha256 = System.Security.Cryptography.SHA256.Create();
    var hashedBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password + secretKey));
    return Convert.ToBase64String(hashedBytes);
}

string GenerateToken() => Guid.NewGuid().ToString();

string GenerateSecureCode() => new Random().Next(100000, 999999).ToString();

// Register endpoint
app.MapPost("/api/auth/register", async (UserModel user, IUserRepository repo, ILogger<Program> logger) =>
{
    logger.LogInformation("Registering new user: {username}", user.username);

    // Check if the username already exists
    var existingUser = await repo.GetUserByUsernameAsync(user.username);
    if (existingUser != null)
    {
        logger.LogWarning("username {username} already exists {existingUser}", user.username , existingUser);
        return Results.BadRequest(new { Message = "username already exists"  , error = "failed"});
    }

    // Encrypt password and generate token
    user.password = EncryptPassword(user.password);
    user.token = GenerateToken();

    try
    {
        await repo.SaveUserAsync(user);
        logger.LogInformation("User registered successfully: {username}", user.username);
        return Results.Ok(new { Message = "User registered successfully", token = user.token });
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Error during user registration: {username}", user.username);
        return Results.Ok(new { Message = "User register failed" , error = "failed"});
    }
});

// Generate secure code endpoint
app.MapPost("/api/auth/secure/code", async (UserModel user, IUserRepository repo, ILogger<Program> logger) =>
{
    logger.LogInformation("Request received to generate secure code for user: {username}", user.username);
    
    var storedUser = await repo.GetUserByUsernameAsync(user.username);
    if (storedUser == null)
    {
        logger.LogWarning("User with username {username} not found , {storedUser}", user.username , storedUser);
        return Results.Unauthorized();
    }

    if (storedUser.securecode == null)
    {
        storedUser.securecode = GenerateSecureCode();
        await repo.UpdateUserAsync(storedUser);
        
        logger.LogInformation("Secure code generated for user: {username}", user.username);
        return Results.Ok(new { Message = "Secure code generated", securecode = storedUser.securecode });
    }

    logger.LogWarning("Failed to generate code for this user : {username}", user.username);
    return Results.Ok(new { Message = "Failed to generate code !", error = "failed"});
});

// Login endpoint
app.MapPost("/api/auth/login", async (UserModel user, IUserRepository repo, HttpContext httpContext, ILogger<Program> logger) =>
{
    logger.LogInformation("Login attempt for user: {username}", user.username);

    // Find the user in the database
    var storedUser = await repo.GetUserByUsernameAsync(user.username);
    if (storedUser == null || storedUser.securecode == null)
    {
        logger.LogWarning("User not found in the database: {username}", user.username);
        return Results.Ok(new { Message = "Invalid username or password", error = "failed" });
    }

    // Validate the password
    if (storedUser.password != EncryptPassword(user.password) && storedUser.securecode != null)
    {
        logger.LogWarning("Password mismatch for user: {username}", user.username);
        return Results.Ok(new { Message = "Invalid username or password", error = "failed" });
    }

    logger.LogInformation("Login successful for user: {username}", user.username);
    return Results.Ok(new { Message = "Login successful", token = storedUser.token });
});

// Forget password endpoint
app.MapPost("/api/auth/forget", async (ForgetPasswordModel model, IUserRepository repo, ILogger<Program> logger) =>
{
    logger.LogInformation("password reset request for user: {username}", model.username);

    var storedUser = await repo.GetUserByUsernameAsync(model.username);
    if (storedUser != null && storedUser.securecode == model.securecode)
    {
        storedUser.password = EncryptPassword(model.newpassword);
        await repo.UpdateUserAsync(storedUser);
        
        logger.LogInformation("password updated successfully for user: {username}", model.username);
        return Results.Ok(new { Message = "password updated successfully", success = "true" });
    }

    logger.LogWarning("Invalid secure code for user: {username}", model.username);
    return Results.Ok(new { Message = "Invalid secure code for user", error = "failed" });
});

// Get username by token endpoint
app.MapPost("/api/auth/profile", async (HttpContext httpContext, IUserRepository repo, ILogger<Program> logger) =>
{
    // Extract the Bearer token from the Authorization header
    var authHeader = httpContext.Request.Headers["Authorization"].ToString();
    if (string.IsNullOrWhiteSpace(authHeader) || !authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
    {
        logger.LogWarning("Authorization header is missing or invalid");
        return Results.Ok(new { Message = "Authorization header is missing or invalid" , error = "failed"  });
    }

    var token = authHeader.Substring("Bearer ".Length).Trim();

    // Find the user in the database by token
    var storedUser = await repo.GetUserByTokenAsync(token);
    if (storedUser == null)
    {
        logger.LogWarning("User not found with token: {token}", token);
        return Results.Ok(new { Message = "Invalid token" , error = "failed" });
    }

    // Return the username if the token matches
    logger.LogInformation("Token validated successfully, user found: {username}", storedUser.username);
    return Results.Ok(new { Username = storedUser.username });
});

app.Run();

// Models and Repository
public record UserModel
{
    [BsonId] // This attribute marks the _id field
    public ObjectId Id { get; set; } // MongoDB's ObjectId type

    public string username { get; set; } = string.Empty;
    public string password { get; set; } = string.Empty;
    public string? token { get; set; }
    public string? securecode { get; set; }
}

public record ForgetPasswordModel
{
    public string username { get; set; } = string.Empty;
    public string newpassword { get; set; } = string.Empty;
    public string securecode { get; set; } = string.Empty;
}

public interface IUserRepository
{
    Task SaveUserAsync(UserModel user);
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task<UserModel?> GetUserByTokenAsync(string token); // New method to get user by token
    Task<UserModel?> CheckUserSecurityCode(string securityCode);
    Task UpdateUserAsync(UserModel user);
}

public class MongoUserRepository : IUserRepository
{
    private readonly IMongoCollection<UserModel> _users;

    public MongoUserRepository()
    {
        var client = new MongoClient("mongodb://localhost:27017"); // Replace with your MongoDB connection string
        var database = client.GetDatabase("UserDatabase");
        _users = database.GetCollection<UserModel>("Users");
    }

    public async Task SaveUserAsync(UserModel user)
    {
        await _users.InsertOneAsync(user);
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        return await _users.Find(u => u.username == username).FirstOrDefaultAsync();
    }

    public async Task<UserModel?> GetUserByTokenAsync(string token) // Method to fetch user by token
    {
        return await _users.Find(u => u.token == token).FirstOrDefaultAsync();
    }

    public async Task<UserModel?> CheckUserSecurityCode(string securityCode)
    {
        return await _users.Find(u => u.securecode == securityCode).FirstOrDefaultAsync();
    }

    public async Task UpdateUserAsync(UserModel user)
    {
        await _users.ReplaceOneAsync(u => u.username == user.username, user);
    }
}
