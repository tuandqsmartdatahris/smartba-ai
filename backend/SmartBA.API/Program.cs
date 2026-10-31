using Microsoft.EntityFrameworkCore;
using SmartBA.Infrastructure.Persistence;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SmartBA.Application.Interfaces.Security;
using SmartBA.Application.Interfaces.Email;
using SmartBA.Infrastructure.Services.Email;
using SmartBA.Infrastructure.Services.Authentication;
using SmartBA.Infrastructure.Services.Security;

using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection");

Console.WriteLine(connectionString);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Enable CORS for Angular app - TUANDQ - 29 Oct 2026

builder.Services.AddCors(options =>
{
    options.AddPolicy("AngularPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//////////////////////////////////////////////////////////////////
///JWT Package - TUANDQ - 29 Oct 2026
/// dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer

var secretKey =
    builder.Configuration["JwtSettings:SecretKey"];

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultScheme =
            CookieAuthenticationDefaults.AuthenticationScheme;

        options.DefaultChallengeScheme =
            GoogleDefaults.AuthenticationScheme;
    })
    .AddCookie()
    .AddGoogle(options =>
    {
        options.ClientId =
            builder.Configuration["Authentication:Google:ClientId"]!;

        options.ClientSecret =
            builder.Configuration["Authentication:Google:ClientSecret"]!;

        options.Events.OnRedirectToAuthorizationEndpoint = context =>
        {
            Console.WriteLine(
                $"Google Redirect: {context.RedirectUri}");

            context.Response.Redirect(
                context.RedirectUri);

            return Task.CompletedTask;
        };        
    })

    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer =
                    builder.Configuration["JwtSettings:Issuer"],

                ValidAudience =
                    builder.Configuration["JwtSettings:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(secretKey!))
            };
    });

    builder.Services.AddAuthorization();
    builder.Services.AddScoped<JwtService>();
    builder.Services.AddHttpContextAccessor();

    builder.Services.AddScoped<
        IEmailService,
        EmailService>();

    builder.Services.AddScoped<
        ISecurityEventService,
        SecurityEventService>();

    var app = builder.Build();
    app.UseHttpsRedirection();
    app.UseCors("AngularPolicy"); // Enable CORS for Angular app - TUANDQ - 29 Oct 2026
    
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
    
      
    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        //app.MapOpenApi();
        // TUANDQ thêm 2 dòng dưới để hiển thị swagger - 28 Oct 2026
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.Run();
        

